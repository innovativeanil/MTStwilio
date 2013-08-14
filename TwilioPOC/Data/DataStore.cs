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
        private object lockObject = new object();
        
        public static DataStore Instance { get { return instance ?? (instance = new DataStore()); } }

        public bool Save(Feedback item)
        {
            var path = HttpContext.Current.Server.MapPath(dataPath);
            var data = File.ReadAllText(path);

            var feedbackList = JsonConvert.DeserializeObject<List<Feedback>>(data) ?? new List<Feedback>();
            item.Id = feedbackList.Count + 1;
            feedbackList.Add(item);
            
            var json = JsonConvert.SerializeObject(feedbackList);
            
            File.WriteAllText(path, json);
            
            return true;
        }

        public Feedback[] GetItems()
        {
            var path = HttpContext.Current.Server.MapPath(dataPath);
            var data = File.ReadAllText(path);
            var feedbackList = JsonConvert.DeserializeObject<List<Feedback>>(data) ?? new List<Feedback>();
            return feedbackList.ToArray();
        }
    }
}