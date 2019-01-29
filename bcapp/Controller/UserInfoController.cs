using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcapp.Model;
using bcapp.Service;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace bcapp.Controller
{
   public class UserInfoController
    {
        public static async Task<UserInfo> GetInfo(string Key)
        {
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip=appid=" + Key;

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);


            if (results["weather"] != null)
            {
                UserInfo userinfo = new UserInfo();
                userinfo.Name = (string)results["name"];
     
               
                return userinfo;
            }
            else
            {
                return null;
            }

        }
}