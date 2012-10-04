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

namespace _2Do
{
    public partial class AddTaskPage : PhoneApplicationPage
    {

        public AddTaskPage()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(AddTaskPage_Loaded);
        }

        void AddTaskPage_Loaded(object sender, RoutedEventArgs e)
        {
            ContentTextBox.Focus();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            (new Data.RemindItemManager()).Insert(ContentTextBox.Text);
            NavigationService.GoBack();
        }
    }
}