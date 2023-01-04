using mainModules.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using securityAPI;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for frmsettings_main.xaml
    /// </summary>
    public partial class frmsettings_main : Window
    {

        private UserContext _contextUser =
          new UserContext();

        private CollectionViewSource userViewSource;



        public frmsettings_main()
        {
            InitializeComponent();

            userViewSource =
               (CollectionViewSource)FindResource(nameof(userViewSource));


            InitDB();
        }


        private void InitDB()
        {
            _contextUser.Users.Load();

            // bind to the source
            userViewSource.Source =
                _contextUser.Users.Local.ToObservableCollection();
        }


        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            frmUser user = new frmUser();
            user.ShowDialog();
            RefreshUsers();
        }

        private void RefreshUsers()
        {
            _contextUser.Dispose();
            _contextUser = new UserContext();
            InitDB();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if ((dbgUsers.SelectedIndex >= 0) && (dbgUsers.Items.Count > 1))
            {

                databaseAPI.Models.User userEntry = dbgUsers.SelectedItem as databaseAPI.Models.User;

                MessageBoxResult mrConfirmDelete = MessageBox.Show($"Are you sure you'd like to stage this item for deletion?\nUsername = {userEntry.Username}",
                    "WARNING: Confirm User Deletion", MessageBoxButton.YesNo);

                if (mrConfirmDelete == MessageBoxResult.Yes)
                {
                    _contextUser.Users.Remove(userEntry);
                    _contextUser.SaveChanges();

                }

            }
            else
            {
                MessageBox.Show("Select a user before clicking [DELETE] (Note, you must have at least 1 user at all times)", "Unable to delete", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void dbgUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dbgUsers.SelectedIndex >= 0)
            {
                databaseAPI.Models.User selectedUser = dbgUsers.SelectedItem as databaseAPI.Models.User;
                frmUser user = new frmUser(selectedUser);
                user.ShowDialog();
                RefreshUsers();
            }
        }
    }
}
