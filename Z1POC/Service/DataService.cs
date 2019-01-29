using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Z1POC.Model;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace Z1POC.Service
{
    public class DataService
    {
        public static async Task<dynamic> getDataFromService(string queryString, string method, UserInfo userInfo)
        {
            dynamic data = null;

            try
            {
                HttpClient client = new HttpClient();
               
                if (method == "GET")
                {
                    var response = await client.GetAsync(queryString);

                    if (response != null)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        data = JsonConvert.DeserializeObject(json);
                    }
                }

                else if (method == "POST")
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(queryString, stringContent);

                    if (response != null)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        data = JsonConvert.DeserializeObject(json);
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        
            return data;
            
        }
    }   
}