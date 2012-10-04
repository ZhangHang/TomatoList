using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace _2Do.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Entities.RemindItem> ToDos = new ObservableCollection<Entities.RemindItem>();
        public ObservableCollection<Entities.RemindItem> Dones = new ObservableCollection<Entities.RemindItem>();

        private Data.RemindItemManager remindItemManager = new Data.RemindItemManager();
        public void GetData()
        {
            Dones.Clear();
            foreach (var item in remindItemManager.GetDoneItem())
                Dones.Add(item);

            ToDos.Clear();
            foreach (var item in remindItemManager.GetUnFinishItems())
                ToDos.Add(item);
        }

        public void done(Entities.RemindItem item)
        {
            remindItemManager.LogItemDone(item);
        }

        public void Delete(Entities.RemindItem item)
        {
            remindItemManager.Delete(item);
        }
    }
}
