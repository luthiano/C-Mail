﻿using System;
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
using System.IO;
using System.Diagnostics;
using System.Net.Mail;
using S22.Imap;

namespace C_Mail_2._0
{
    /// <summary>
    /// Interaction logic for InboxWindow.xaml
    /// </summary>
    public partial class InboxWindow : Window
    {
        public InboxWindow()
        {
            InitializeComponent();
        }

        // Button _Click methods

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of MainWindow class
            MainWindow SendMail = new MainWindow();
            
            // Show it
            SendMail.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Opens a new tab in your default browser where you can report your issue when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportAnIssueButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Tijndagamer/C-Mail/issues/new");
        }

        /// <summary>
        /// Shows the newest email in your inbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrEmpty(Program.FromAddress)))
            {
                // Create a new list to store all the messages in
                List<MailMessage> Messages = new List<MailMessage>();

                // Get all the unseen messages
                Messages = Program.GetAllMessages(Program.FromAddress, Program.FromPass);

                // Get the messagecount
                int MessageCount = Messages.Count;

                try
                {
                    // Show the sender of the last received email
                    FromTextBox.Text = Messages[MessageCount - 1].From.ToString();

                    // Show the subject of the last received email
                    SubjectTextBox.Text = Messages[MessageCount - 1].Subject.ToString();

                    // Show the body of the last received email
                    EmailBodyTextBox.Text = Messages[MessageCount - 1].Body.ToString();
                }
                catch (Exception exception)
                {
                    // Create the error message
                    string ErrorMessage = "ERROR 70001" + "\n" + exception.ToString();

                    // Show the error message
                    Program.ErrorPopupCall(ErrorMessage);

                    // Stop executing this method
                    return;
                }
            }
            else
            {
                Program.ErrorPopupCall("Be sure to log in!");
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // If the credentials are saved, open those, else open the login popup
            if (File.Exists("Credentials") == true)
            {
                // Ask the Encryption password using the EncryptionPasswordPopup
                EncryptionPasswordPopup popup = new EncryptionPasswordPopup();

                //Show the popup
                popup.Show();
            }
            else
            {
                // Create a new instance of the LoginPopup class
                LoginPopup loginPopup = new LoginPopup();

                // Show the popup
                loginPopup.Show();
            }
        }
    }
}
