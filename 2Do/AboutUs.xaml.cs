using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace _2Do
{
    public partial class AboutUs : PhoneApplicationPage
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask Myemail_Composetask = new EmailComposeTask();
            Myemail_Composetask.To = "zh199111@gmail.com";
            Myemail_Composetask.Subject = "Hello There";
            Myemail_Composetask.Show();
        }
    }
}