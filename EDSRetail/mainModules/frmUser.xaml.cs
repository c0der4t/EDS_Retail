using Microsoft.EntityFrameworkCore;
using securityAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for frmUser.xaml
    /// </summary>
    public partial class frmUser : Window
    {
        private int userToEdit_ID = -1;
        private string CurrPassHash = string.Empty;

        public frmUser()
        {
            InitializeComponent();
        }

        public frmUser(databaseAPI.Models.User userToEdit)
        {
            InitializeComponent();

            edtFirstName.Text = userToEdit.FirstName;
            edtLastName.Text = userToEdit.LastName;
            edtEmail.Text = userToEdit.Email;
            edtUsername.Text = userToEdit.Username;
            edtUserPassword.Password = userToEdit.Password;
            chckAdmin.IsChecked = userToEdit.IsAdmin;
            chckCanLogin.IsChecked = userToEdit.CanLogin;
            chckChangePassw.IsChecked = userToEdit.MustChangePassword;

            userToEdit_ID = userToEdit.ID;
            CurrPassHash = userToEdit.Password;
            
        }

        private void btnPostUserSetup_Click(object sender, RoutedEventArgs e)
        {
            if (edtUserPassword.Password != string.Empty)
            {

                databaseAPI.Models.User systemUser = new databaseAPI.Models.User();

                systemUser.FirstName = edtFirstName.Text;
                systemUser.LastName = edtLastName.Text;
                systemUser.Email = edtEmail.Text;

                systemUser.Username = edtUsername.Text;
                systemUser.Password = edtUserPassword.Password == CurrPassHash ? CurrPassHash : Encryption.GenerateHashFromString(edtUserPassword.Password);
                systemUser.IsAdmin = (bool)chckAdmin.IsChecked;
                systemUser.CanLogin = (bool)chckCanLogin.IsChecked;
                systemUser.MustChangePassword = (bool)chckCanLogin.IsChecked;

                if (userToEdit_ID > -1)
                {
                    //We are in Edit mode. Delete user entry first, then add as new entry
                    systemUser.ID = userToEdit_ID;



                    using (UserContext _contextUser = new UserContext())
                    {
                        _contextUser.Users.Update(systemUser);

                        _contextUser.SaveChanges();
                    }
                }
                else
                {
                    //Not in edit mode, just add to DB
                    using (UserContext _contextUser = new UserContext())
                    {
                        _contextUser.Users.Add(systemUser);

                        _contextUser.SaveChanges();
                    }
                }

                this.Close();

            }
            else
            {
                MessageBox.Show("Password cannot be empty", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            

        }
    }
}
