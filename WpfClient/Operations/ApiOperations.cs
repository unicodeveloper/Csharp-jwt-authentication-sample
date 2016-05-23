using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using WpfClient.Models;
using Newtonsoft.Json;

namespace WpfClient.Operations
{
    class ApiOperations
    {
        /**
         * Base Url @string
         */
        private string baseUrl;

        public ApiOperations()
        {
            this.baseUrl = "http://localhost:5000/api";
        }

        /**
         * Authenticate user with Web Api Endpoint
         * @param string username
         * @param string password
         */
        public User AuthenticateUser(string username, string password)
        {
            string endpoint = this.baseUrl + "/users/login";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /**
         * Get User Details from Web Api
         * @param  User Model
         */
        public User GetUserDetails(User user)
        {
            string endpoint = this.baseUrl + "/users/" + user.Id;
            string access_token = user.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = access_token;
            try
            {
                string response = wc.DownloadString(endpoint);
                user = JsonConvert.DeserializeObject<User>(response);
                user.access_token = access_token;
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /**
         * Register User
         * @param  string username
         * @param  string password
         * @param  string firstname
         * @param  string lastname
         * @param  string middlename
         * @param  int    age
         */
        public User RegisterUser(string username, string password, string firstname,
            string lastname, string middlename, int age)
        {
            string endpoint = this.baseUrl + "/users";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password,
                firstname = firstname,
                lastname = lastname,
                middlename = middlename,
                age = age
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
