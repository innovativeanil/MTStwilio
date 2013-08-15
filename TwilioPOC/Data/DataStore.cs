using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TwilioPOC.Models;

namespace TwilioPOC.Data
{
    public class DataStore
    {
        private const string dataPath = @"\Data\feedbackItems.txt";

        private static DataStore instance;
        private readonly object lockObject = new object();
        private readonly List<Feedback> dataStore;

        public static DataStore Instance { get { return instance ?? (instance = new DataStore()); } }
        
        public DataStore()
        {
            dataStore = new List<Feedback>();
        }

        public int Create(Feedback item)
        {
            lock (lockObject)
            {
                item.Id = dataStore.Count + 1;
                item.Status = "Open";
                dataStore.Add(item);
                return item.Id;
            }
        }

        public bool ChangeStatus(int id, string status)
        {
            lock (lockObject)
            {
                // find our item
                var match = dataStore.FirstOrDefault(feedback => feedback.Id.Equals(id));

                if (match == null)
                {
                    return false;
                }

                // update the item
                match.Status = status;

                // Notify submitter via text
                TwilioHelper.SendText("507-304-1050", 
                    string.Format("The status of MTS Feedback Item #{0} has been updated to {1}.", id, status));
                
                return true;
            }
        }

        public Feedback[] GetItems()
        {
            lock (lockObject)
            {
                return dataStore.ToArray();
            }
        }

        public Feedback GetItem(int id)
        {
            if (id > dataStore.Count)
                return null;

            return dataStore[id - 1];
        }

        public Feedback GetItem(string id)
        {
            int intId;
            var success = Int32.TryParse(id, out intId);

            if (!success)
                return null;

            return GetItem(intId);
        }

        //public bool Create(Feedback item)
        //{
        //    lock(lockObject)
        //    {
        //        var path = HttpContext.Current.Server.MapPath(dataPath);
        //        var data = File.ReadAllText(path);

        //        var feedbackList = JsonConvert.DeserializeObject<List<Feedback>>(data) ?? new List<Feedback>();
        //        item.Id = feedbackList.Count + 1;
        //        item.Status = "Open";
        //        feedbackList.Add(item);
            
        //        var json = JsonConvert.SerializeObject(feedbackList);
        //        File.WriteAllText(path, json);
        //        return true;
        //    }
        //}

        //public bool ChangeStatus(int id, string status)
        //{
        //    lock (lockObject)
        //    {
        //        var path = HttpContext.Current.Server.MapPath(dataPath);
        //        var data = File.ReadAllText(path);

        //        // find our item
        //        var feedbackList = JsonConvert.DeserializeObject<List<Feedback>>(data) ?? new List<Feedback>();
        //        var match = feedbackList.FirstOrDefault(feedback => feedback.Id.Equals(id));
                
        //        if (match == null)
        //        {
        //            return false;
        //        }

        //        // update the item
        //        match.Status = status;
        //        var json = JsonConvert.SerializeObject(feedbackList);
        //        File.WriteAllText(path, json);

        //        // TODO: Notify submitter via text

        //        return true;
        //    }
        //}

        //public Feedback[] GetItems()
        //{
        //    lock (lockObject)
        //    {
        //        var path = HttpContext.Current.Server.MapPath(dataPath);
        //        var data = File.ReadAllText(path);
        //        var feedbackList = JsonConvert.DeserializeObject<List<Feedback>>(data) ?? new List<Feedback>();
        //        return feedbackList.ToArray();
        //    }
        //}
    }
}