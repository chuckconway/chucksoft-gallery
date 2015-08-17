using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Chucksoft.Entities
{
    [Serializable]
    public abstract class EntityBase 
    {
        /// <summary>
        /// Returns the state saved internally by calling SaveState.
        /// </summary>
        [XmlIgnore]
        private string _internalState;      

        #region IDirty Members

        /// <summary>
        /// Determines whether this instance is dirty.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is dirty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDirty()
        {
            string currentState = CurrentState();
            bool isDirty = ((_internalState == null) || (currentState != _internalState));

            return isDirty;
        }

        /// <summary>
        /// Save (serialize) the state of the current object internally.
        /// </summary>
        public void SaveState()
        {
            if (_internalState == string.Empty)
            {
                string currentState = CurrentState();
                _internalState = currentState;
            }
        }

        #endregion

        /// <summary>
        /// Currents the state.
        /// </summary>
        /// <returns></returns>
        private string CurrentState()
        {
            return SerializeObject(GetType(), this);
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string SerializeObject(Type type, object value)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(type);

            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.ASCII);
            xs.Serialize(xmlTextWriter, value);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

            ASCIIEncoding ASCII = new ASCIIEncoding();

            string serialized = ASCII.GetString(memoryStream.ToArray());

            return serialized;
        }


    }
}
