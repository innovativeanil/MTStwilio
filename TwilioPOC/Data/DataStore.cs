﻿using System;
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
        
        public static DataStore Instance { get { return instance ?? (instance = new DataStore()); } }

        public bool Create(Feedback item)
        {
            lock(lockObject)
            {
                var path = HttpContext.Current.Server.MapPath(dataPath);
                var data = File.ReadAllText(path);

                var feedbackList = JsonConvert.DeserializeObject<List<Feedback>>(data) ?? new List<Feedback>();
                item.Id = feedbackList.Count + 1;
                item.Status = "Open";
                feedbackList.Add(item);
            
                var json = JsonConvert.SerializeObject(feedbackList);
                File.WriteAllText(path, json);
                return true;
            }
        }

        public bool ChangeStatus(int id, string status)
        {
            lock (lockObject)
            {
                var path = HttpContext.Current.Server.MapPath(dataPath);
                var data = File.ReadAllText(path);

                // find our item
                var feedbackList = JsonConvert.DeserializeObject<List<Feedback>>(data) ?? new List<Feedback>();
                var match = feedbackList.FirstOrDefault(feedback => feedback.Id.Equals(id));
                
                if (match == null)
                {
                    return false;
                }

                // update the item
                match.Status = status;
                var json = JsonConvert.SerializeObject(feedbackList);
                File.WriteAllText(path, json);

                // TODO: Notify submitter via text

                return true;
            }
        }

        public Feedback[] GetItems()
        {
            lock (lockObject)
            {
                var path = HttpContext.Current.Server.MapPath(dataPath);
                var data = File.ReadAllText(path);
                var feedbackList = JsonConvert.DeserializeObject<List<Feedback>>(data) ?? new List<Feedback>();
                return feedbackList.ToArray();
            }
        }
    }
}