using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Api.Models
{
    public class ApiDbContext
    {
        private string datastore;

        public ApiDbContext()
        {
            this.datastore = "./Datastore";
            InitializeFieldValues();
        }

        private void InitializeFieldValues()
        {
            // Get users
            string filename = "/users.json";
            string json = File.ReadAllText(this.datastore + filename);
            this.Users = JsonConvert.DeserializeObject<List<User>>(json);
        }

        public List<User> Users { get; set; }

        public void SaveChanges()
        {
            // Save users
            string json = JsonConvert.SerializeObject(this.Users);
            string filename = "/users.json";
            File.WriteAllText(this.datastore + filename, json);
        }
    }
}
