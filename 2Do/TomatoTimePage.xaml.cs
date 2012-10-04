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
    public partial class TomatoTimePage : PhoneApplicationPage
    {
        private Entities.RemindItem currentRemindItem;

        private System.Windows.Threading.DispatcherTimer UpdateTimer = new System.Windows.Threading.DispatcherTimer();
        private TimeSpan resetTime;

        public TomatoTimePage()
        {
            InitializeComponent();


            App.LastStartTime = DateTime.Now;

            UpdateTimer.Interval = TimeSpan.FromSeconds(1);
            UpdateTimer.Tick += new EventHandler(UpdateTimer_Tick);

            resetTime = TimeSpan.FromMinutes(25);
            Update();
            UpdateTimer.Start();
            this.Loaded += new RoutedEventHandler(TomatoTimePage_Loaded);
        }


        void UpdateTimer_Tick(object sender, EventArgs e)
        {
            resetTime = resetTime - UpdateTimer.Interval;
            Update();
        }

        private void Update()
        {
            if (resetTime.TotalSeconds > 0)
            {
                resetTimeTb.Text = string.Format("{0}:{1}", (int)resetTime.TotalMinutes, (int)(resetTime.TotalSeconds - (int)resetTime.TotalMinutes * 60));

                progressBar.Value = 100 - resetTime.TotalSeconds / TimeSpan.FromMinutes(25).TotalSeconds * 100;
            }
            else
            {
                (new Data.RemindItemManager()).LogItemDone(currentRemindItem);
                NavigationService.GoBack();
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string value = string.Empty;
            IDictionary<string, string> queryString = this.NavigationContext.QueryString;

            currentRemindItem = new Entities.RemindItem()
            {
                Id = Convert.ToInt32(queryString["id"]),
                Title = queryString["content"]
            };

            PageTitle.Text = currentRemindItem.Title;

            if (!App.LastStartTime.Equals(default(DateTime)))
            {
                resetTime = App.LastStartTime - DateTime.Now + TimeSpan.FromMinutes(25);
                Update();
            }
            base.OnNavigatedTo(e);
        }


        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Grid_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            reseTimeGrid.Visibility = reseTimeGrid.Visibility.Equals(Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            mainGrid.Visibility = reseTimeGrid.Visibility.Equals(Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}