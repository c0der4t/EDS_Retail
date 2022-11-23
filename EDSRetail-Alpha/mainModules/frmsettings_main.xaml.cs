﻿using mainModules.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
