using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chucksoft.Silverlight.Controls.Network
{
    internal sealed class SocketClient: IDisposable
    {
        private const int Receive = 1;
        private const int Send = 0;

        private bool isConnected = false;

        private Socket socket;
        private DnsEndPoint endPoint;

        private static AutoResetEvent autoEvent = new AutoResetEvent(false);
        private static AutoResetEvent[] autoSendReceiveEvents = new AutoResetEvent[]
                {
                        new AutoResetEvent(false),
                        new AutoResetEvent(false)
                };

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketClient"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        internal SocketClient(string host, int port)
        {
            endPoint = new DnsEndPoint(host, port);
            socket = new Socket(AddressFamily.InterNetwork 
                        /* hostEndPoint.AddressFamily */, 
                        SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        internal void Connect()
        {
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();

            args.UserToken = socket;
            args.RemoteEndPoint = endPoint;
            args.Completed += new EventHandler<SocketAsyncEventArgs>(OnConnect);

            socket.ConnectAsync(args);
            autoEvent.WaitOne();

            if (args.SocketError != SocketError.Success)
                throw new SocketException((int)args.SocketError);
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        internal void Disconnect()
        {
            socket.Close();
        }

        #region Events

        /// <summary>
        /// Called when [connect].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.Sockets.SocketAsyncEventArgs"/> instance containing the event data.</param>
        private void OnConnect(object sender, SocketAsyncEventArgs e)
        {
            autoEvent.Set();
            isConnected = (e.SocketError == SocketError.Success);
        }

        /// <summary>
        /// Called when [receive].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.Sockets.SocketAsyncEventArgs"/> instance containing the event data.</param>
        private void OnReceive(object sender, SocketAsyncEventArgs e)
        {
            autoSendReceiveEvents[Send].Set();
        }

        /// <summary>
        /// Called when [send].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.Sockets.SocketAsyncEventArgs"/> instance containing the event data.</param>
        private void OnSend(object sender, SocketAsyncEventArgs e)
        {
            autoSendReceiveEvents[Receive].Set();

            if (e.SocketError == SocketError.Success)
            {
                if (e.LastOperation == SocketAsyncOperation.Send)
                {
                    // Prepare receiving.
                    Socket s = e.UserToken as Socket;

                    byte[] response = new byte[255];
                    e.SetBuffer(response, 0, response.Length);
                    e.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceive);
                    s.ReceiveAsync(e);
                }
            }
            else
            {
                ProcessError(e);
            }
        }

        #endregion

        /// <summary>
        /// Processes the error.
        /// </summary>
        /// <param name="e">The <see cref="System.Net.Sockets.SocketAsyncEventArgs"/> instance containing the event data.</param>
        private void ProcessError(SocketAsyncEventArgs e)
        {
            Socket s = e.UserToken as Socket;
            if (s.Connected)
            {
                try
                {
                    s.Shutdown(SocketShutdown.Both);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (s.Connected)
                        s.Close();
                }
            }

            throw new SocketException((int)e.SocketError);
        }

        /// <summary>
        /// Sends the receive.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        internal String SendReceive(string message)
        {
            if (isConnected)
            {
                Byte[] bytes = Encoding.UTF8.GetBytes(message);

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.SetBuffer(bytes, 0, bytes.Length);
                args.UserToken = socket;
                args.RemoteEndPoint = endPoint;
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnSend);

                socket.SendAsync(args);

                AutoResetEvent.WaitAll(autoSendReceiveEvents);

                return Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
            }
            else
                throw new SocketException((int)SocketError.NotConnected);
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            autoEvent.Close();
            autoSendReceiveEvents[Send].Close();
            autoSendReceiveEvents[Receive].Close();
            if (socket.Connected)
                socket.Close();
        }

        #endregion
    }
}
