using Microsoft.EntityFrameworkCore;
using securityAPI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for frmUserLogin.xaml
    /// </summary>
    public partial class frmUserLogin : Window
    {
        MainWindow parentForm;

        int FailedLoginCounter = 0;

        public frmUserLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overrides default show/initialize component action.
        /// Override occurs when Show is called with parameter.
        /// </summary>
        /// <param name="callingForm">The form initializing the .Show call. Usually 'this' will suffice</param>
        public frmUserLogin(MainWindow callingForm)
        {
            InitializeComponent();
            parentForm = callingForm;

        }




        #region Custom Methods



        private void VerifyLogin()
        {
            using (var _localcontextUsers = new UserContext())
            {
                try
                {
                    var userItem = _localcontextUsers.Users
                                               .Single(x => x.Username == edtLoginUsername.Text);

                    if (securityAPI.Decryption.VerifyStringAgainstHash(edtLoginPassword.Password, userItem.Password))
                    {
                        authToken.AuthorizeUser(userItem.ID, userItem.Username, userItem.FirstName);
                        Close();
                    }
                    else
                    {
                        throw new Exception("Invalid Password");
                    }
                }
                catch (Exception e)
                {
                    FailedLoginCounter += 1;
                    authToken.DeauthorizeCurrentUser();


                    if (FailedLoginCounter >= 3)
                    {
                        MessageBox.Show("Multiple failed logins detected. Closing application");
                        Environment.Exit(0);
                    }
                    else
                    {
                        if (e.ToString().Contains("Invalid Password"))
                        {
                            MessageBox.Show($"Login failed - Invalid Password");
                        }

                        if (e.ToString().Contains("Sequence contains no elements"))
                        {
                            MessageBox.Show($"Login failed - User does not exist");
                        }
                        
                    }

                }

            }


        }


        #endregion

        #region UI Events

        private void edtLoginUsername_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                edtLoginPassword.Focus();
            }
        }

        private void edtLoginPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                VerifyLogin();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            VerifyLogin();
        }



        #endregion

        private void edtLoginPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Toggled) == KeyStates.Toggled)
            {
                lblCapsLockWarning.Visibility = Visibility.Visible;
            }
            else
            {
                lblCapsLockWarning.Visibility = Visibility.Hidden;
            }

        }
    }
}
