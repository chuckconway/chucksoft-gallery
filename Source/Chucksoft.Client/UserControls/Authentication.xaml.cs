using System;
using System.Windows;
using Chucksoft.Client.UserService;

namespace Chucksoft.Client.UserControls
{
    /// <summary>
    /// Interaction logic for Authentication.xaml
    /// </summary>
    public partial class Authentication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authentication"/> class.
        /// </summary>
        public Authentication()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserServiceSoapClient client = new UserServiceSoapClient();

                string token = client.GetTokenByUsernameAndPassword(usernameTextBox.Text, passwordTextBox.Password);
                
                if (!string.IsNullOrEmpty(token))
                {
                    Client.Default.username = usernameTextBox.Text;
                    Client.Default.pasword = passwordTextBox.Password;

                    Client.Default.token = token;
                    Client.Default.Save();
                    Close();
                }

                messageLabel.Content = "Your username or password did not match our records.";
                
            }
            catch(Exception ex)
            {
                //TODO:Logging add logging paradigmn

                messageLabel.Content = ex.Message;
            }

            
        }

    }
}
