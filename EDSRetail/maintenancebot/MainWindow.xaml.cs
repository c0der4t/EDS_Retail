using mainModules.Models;
using databaseAPI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace maintenancebot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private _dbContext _mainContext =
            new _dbContext();

       private IDictionary<string, string> taskList = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
            InitDB();
            TaskInfoBox.Text = "Click a button to start the task. Hover over a button to find out more about that task";
            GenerateTaskList();
        }

        private void btnDevTest_Click(object sender, RoutedEventArgs e)
        {
          
        }



        private void InitDB()
        {
            //Ref: https://learn.microsoft.com/en-us/ef/core/get-started/wpf#add-code-that-handles-data-interaction

            string dbLocation = databaseAPI.utilities.GetDatabasePath();

            if ((!Directory.Exists(dbLocation)) || (Directory.GetFiles(dbLocation).Length < 1))
            {
                Directory.CreateDirectory(dbLocation);
                //The database does not exists, so create it
                _mainContext.Database.EnsureCreated();
            }

        }


        private void RunDBTask(string SQLQuery, string TaskName = "MaintenanceTask")
        {
            Mouse.SetCursor(Cursors.Wait);

            try
            {
                SQLQuery = Encoding.UTF8.GetString(Convert.FromBase64String(SQLQuery));
                var rowsAffected = _mainContext.Database.ExecuteSqlRaw(SQLQuery);
                WritetoOutput($"{TaskName} Completed. Rows affected : {rowsAffected}");
            }
            catch (Exception e)
            {

                string ExMessage = e.Message;
                switch (ExMessage)
                {
                    case var s when ExMessage.Contains("duplicate column name"):
                        WritetoOutput($"{TaskName} Completed. No changes required");
                        break;
                    default:
                        break;
                }
            }
            Mouse.SetCursor(Cursors.Arrow);
        }

        private void GenerateTaskList()
        {
            //This method assumes the task list exists in the current directory and is called tasklibrary (no extension)

            string libraryPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "tasklibrary"); ;
            var library = File.ReadAllLines(libraryPath);

            taskList.Clear();

            int RowCounter = 1;
            

            foreach (var line in library)
            {
                if (RowCounter > 1)
                {
                    var columns = line.Split(',').ToList();

                    if (!taskList.ContainsKey(columns[0]))
                    {
                        taskList.Add(columns[0], columns[3]);

                        Button newTask = new Button();
                        newTask.Content = columns[0];
                        newTask.ToolTip = columns[2];
                        newTask.MouseEnter += new MouseEventHandler(TaskInfo_OnHover);
                        newTask.MouseLeave += new MouseEventHandler(TaskInfo_Exit);
                        newTask.Click += InitializeTask_Click;
                        spnlListofTasks.Children.Add(newTask);
                    }
                }

                RowCounter++;

            }

        }


        private void WritetoOutput(string OutputString)
        {
            lblOutput.Text = OutputString;
        }

        private void TaskInfo_OnHover(object sender, MouseEventArgs e)
        {
            string description = (sender as Button).ToolTip.ToString();
            TaskInfoBox.Text = description;

        }

        private void TaskInfo_Exit(object sender, MouseEventArgs e)
        {
            TaskInfoBox.Text = "Click a button to start the task. Hover over a button to find out more about that task";

        }

        private void InitializeTask_Click(object sender, RoutedEventArgs e)
        {
            string taskName = (sender as Button).Content.ToString();
            string command;

            taskList.TryGetValue(taskName, out command);

            RunDBTask(command,taskName);

        }
    }
}
