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
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;

namespace _2Do.Data
{
    public class RemindItemManager
    {
        static IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        const string FINISH = "FINISH";
        const string LASTID = "LASTID";

        public RemindItemManager()
        {
            InitIfEmpty();
        }

        public List<Entities.RemindItem> GetByListID(string Id)
        {
            throw new NotImplementedException();
        }

        public int lastID
        {
            get
            {
                int result;
                if (settings.TryGetValue<int>(LASTID, out result))
                    return result;
                else
                    return -1;
            }
            set
            {
                settings[LASTID] = value;
            }
        }

        static public void InitIfEmpty()
        {
            if (!settings.Contains(FINISH))
                settings.Add(FINISH, string.Empty);
            if (!settings.Contains(LASTID))
                settings.Add(LASTID, -1);
        }

        public void Insert(string content)
        {
            lastID = lastID + 1;
            settings.Add(lastID.ToString(), content);
        }

        public void LogItemDone(Entities.RemindItem item)
        {
            settings[FINISH] += item.Id.ToString() + "|";
        }

        public List<Entities.RemindItem> GetDoneItem()
        {
            var result = new List<Entities.RemindItem>();

            foreach (var item in GetFinishedItemId())
            {
                string title;
                if (settings.TryGetValue(item.ToString(), out title))
                    result.Add(new Entities.RemindItem() { Id = Convert.ToInt32(item), Title = title });
            }

            return result;
        }

        public List<int> GetFinishedItemId()
        {
            string resultId;
            settings.TryGetValue<string>(FINISH, out resultId);

            var result = new List<int>();
            foreach (var item in resultId.Split('|'))
                try
                {
                    result.Add(Convert.ToInt32(item));
                }
                catch
                {
                }
            return result;
        }

        public List<Entities.RemindItem> GetUnFinishItems()
        {
            var result = new List<Entities.RemindItem>();
            var doneIds = GetFinishedItemId();
            for (int i = -1; i <= lastID; i++)
            {
                if (!doneIds.Contains(i))
                {
                    string title;
                    if (settings.TryGetValue(i.ToString(), out title))
                        result.Add(new Entities.RemindItem() { Id = i, Title = title });
                }
            }

            return result;
        }

        public void Delete(Entities.RemindItem item)
        {
            var finishedIds = GetFinishedItemId();

            if (finishedIds.Contains(item.Id))
            {
                finishedIds.Remove(item.Id);
                foreach (var id in finishedIds)
                    settings[FINISH] += id.ToString() + "|";
            }

            settings.Remove(item.Id.ToString());
        }

        public void Reset()
        {
            settings.Clear();
            InitIfEmpty();
        }
    }
}
