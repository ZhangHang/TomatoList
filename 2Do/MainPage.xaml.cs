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
    public partial class MainPage : PhoneApplicationPage
    {

        private ViewModels.MainViewModel viewModel = new ViewModels.MainViewModel();
        // Constructor
        public MainPage()
        {
            //reset LastStartTime
            App.LastStartTime = default(DateTime);

            InitializeComponent();

            ToDoListBox.ItemsSource = viewModel.ToDos;
            DoneListBox.ItemsSource = viewModel.Dones;

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.GetData();
        }

        private void NewTaskButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddTaskPage.xaml", UriKind.Relative));
        }

        private void ToDoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem is Entities.RemindItem)
            {
                var item = (Entities.RemindItem)(sender as ListBox).SelectedItem;
                NavigationService.Navigate(new Uri(string.Format("/TomatoTimePage.xaml?content={0}&id={1}", item.Title, item.Id), UriKind.Relative));
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!((sender as Control).DataContext is Entities.RemindItem)) return;

            viewModel.Delete((Entities.RemindItem)(sender as Control).DataContext);

            viewModel.GetData();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutUs.xaml", UriKind.Relative));
        }

        private void TomatoButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/IntroductionPage.xaml", UriKind.Relative));
        }
    }
}