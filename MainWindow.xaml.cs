using System;
using System.Windows;

namespace PassBruteForce
{
    public partial class MainAppWindow : Window
    {
        public MainAppWindow()
        {
            InitializeComponent();
        }

        private async void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string inputPassword = PasswordInputTextBox.Text;
            if (!string.IsNullOrEmpty(inputPassword))
            {
                string hashedPassword = EncryptInputPassword(inputPassword);
                EncryptedPasswordLabel.Content = "Encrypted Password: " + hashedPassword;
                MessageBox.Show("Password encrypted successfully.");
            }
            else
            {
                MessageBox.Show("Please enter a password.");
            }
        }

        private string EncryptInputPassword(string password)
        {
            return "Encrypted: " + password; // Placeholder logic for password encryption
        }

        private async void Crack_Click(object sender, RoutedEventArgs e)
        {
            int maxThreads = 1;
            if (int.TryParse(ThreadsInputTextBox.Text, out int parsedResult))
            {
                maxThreads = parsedResult;
            }

            ResultsOutputTextBox.Text = "Cracking Password..."; // Initial message

            PasswordHandler passwordHandler = new PasswordHandler();
            passwordHandler.ComputeHash(PasswordInputTextBox.Text);
            string hashedPassword = passwordHandler.EncodedPassword; // Get the hashed password

            PasswordCracker passwordCracker = new PasswordCracker();
            passwordCracker.StartBruteForce(maxThreads, hashedPassword); // Initiate the password cracking

            ResultsOutputTextBox.Text = passwordCracker.ResultOutput; // Show the results
        }
    }
}
