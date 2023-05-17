using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demant.UserAuthentication
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Page
    {
        public LoginScreen()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            if (ValidateLoginError(username))
            {
                UserContext.Instance.InitializeRandomData(username);
                Login.NavigationService.Navigate(new UserScreen());
            }

        }

        /// <summary>
        /// To handle login name and password validation
        /// </summary>
        /// <param name="username"></param>
        /// <returns>return TRUE if login is success</returns>
        private bool ValidateLoginError(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username cannot be empty", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            string password = txtPassword.Password;

            var expectedPasswordPattern = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[#?!@$%^&*-]).{8,}$");

            var isValidPassword = expectedPasswordPattern.IsMatch(password);

            if (!isValidPassword)
            {
                MessageBox.Show("Password must at least need to have at minimum 8 characters and one special type character (like @,#,$ etc.)," +
                    "one upper case and one lower case", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
