using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace LoginModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string myPassword;

        public MainWindow()
        {
            InitializeComponent();
            myPassword = getHashFromString("test#test");

        }

        // TODO:Connect to DB
        // TODO: Call by username, get hash

        private bool ComparePasswordandHash(string passwordToCompare, string hashToCompareTo)
        {
            Debug.WriteLine($"Input {getHashFromString(passwordToCompare)}");
            Debug.WriteLine($"DB {hashToCompareTo}");
            return getHashFromString(passwordToCompare) == hashToCompareTo ? true : false;
        }

        private string getHashFromString(string rawString)
        {
            SHA256 sHA256 = SHA256.Create();

            byte[] hashedStringasBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(rawString));

            return string.Join("", hashedStringasBytes.Select(x => $"{x:X2}"));

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(edtUsername.Text)) && (!String.IsNullOrEmpty(edtPassword.Password)))
            {
                Debug.Write(ComparePasswordandHash($"{edtUsername.Text}#{edtPassword.Password}", myPassword) ? "Login Success" : "Login Failed");
            }
        }
    }


}
