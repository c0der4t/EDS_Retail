﻿using mainModules.Models;
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
using databaseAPI.Models;

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
            LoadSettings();
        }

        private void LoadSettings()
        {

            using (var _contextSettings = new SettingsContext())
            {
                var settingList = _contextSettings.Settings.ToArray();

                foreach (var setting in settingList)
                {
                    switch (setting.varType)
                    {
                        case "string":
                            TextBox newEdt = new TextBox();
                            newEdt.Text = setting.Value;
                            newEdt.Uid = setting.ID.ToString() ;
                            newEdt.Name = $"s{setting.Name}";
                            newEdt.Tag = setting.Description;
                            newEdt.MouseEnter += new MouseEventHandler(setting_OnHover);
                            stckpnlGeneral.Children.Add(newEdt);
                            break;
                        case "float":
                            TextBox newFloatEdt = new TextBox();
                            newFloatEdt.Text = setting.Value;
                            newFloatEdt.Uid = setting.ID.ToString();
                            newFloatEdt.Name = $"f{setting.Name}";
                            newFloatEdt.Tag = setting.Description;
                            newFloatEdt.MouseEnter += new MouseEventHandler(setting_OnHover);
                            stckpnlGeneral.Children.Add(newFloatEdt);
                            break;
                        case "bool":
                            CheckBox newChckbx = new CheckBox();
                            newChckbx.Content = setting.Name;
                            newChckbx.Uid = setting.ID.ToString();
                            newChckbx.IsChecked = setting.Value.ToLower() == "true";
                            newChckbx.Name = $"b{setting.Name}";
                            newChckbx.Tag = setting.Description;
                            newChckbx.MouseEnter += new MouseEventHandler(setting_OnHover);
                            stckpnlGeneral.Children.Add(newChckbx);
                            break;
                        default:
                            break;
                    }

                }
            }


        }

        private void SaveSettings()
        {
            //Build a list of all components found in the settings panel
            //Loop through each component
            //Update each entry in the DB using ID

            IDictionary<string,string> settingComponentValues= new Dictionary<string,string>();

            foreach (Control singleComponent in stckpnlGeneral.Children)
            {
                switch (singleComponent.Name.ToLower()[0])
                {
                    case 's':
                        TextBox txtbx = singleComponent as TextBox;
                        settingComponentValues.Add(txtbx.Name.Substring(1),txtbx.Text);
                        Debug.WriteLine(txtbx.Name.Substring(1));
                        break;

                    case 'f':
                        TextBox ftxtbx = singleComponent as TextBox;
                        settingComponentValues.Add(ftxtbx.Name.Substring(1), ftxtbx.Text);
                        break;

                    case 'b':
                        CheckBox ChckBx = singleComponent as CheckBox;
                        settingComponentValues.Add(ChckBx.Name.Substring(1), ChckBx.IsChecked ?? false ? "true" : "false");
                        break;

                    default:
                        break;
                }
            }

            SettingsContext _contextSettings = new SettingsContext();

            foreach (var settingInDB in _contextSettings.Settings)
            {
                string CurrVal;
                settingComponentValues.TryGetValue(settingInDB.Name, out CurrVal);
                settingInDB.Value = CurrVal;

            }

            _contextSettings.SaveChanges();

        }

        private void setting_OnHover(object sender, MouseEventArgs e)
        {
            var obj = (Control)sender;
            redtSettingDescr.Document.Blocks.Clear();
            redtSettingDescr.AppendText(obj.Tag.ToString());
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

        private void btnGnrlSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }
    }
}
