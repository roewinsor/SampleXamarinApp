using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Z1POC.Service;
using Z1POC.Model;

namespace Z1POC.Core
{
    public class ClientService
    {
        public static async Task<UserInfo> GetUserInfo(string method, UserInfo user, string queryString)
        {
           // string queryString = "http://192.168.254.104:56251/api/userinfo/" + id ;

            try
            {
                dynamic test = await DataService.getDataFromService(queryString, method, user).ConfigureAwait(false);

                UserInfo userInfo = new UserInfo();

                if (test != null && method == "POST")
                {
                   

                    userInfo.Id = Convert.ToInt64(test[0]["id"]);
                    userInfo.Name = (string)test[0]["name"];
                    userInfo.Address = (string)test[0]["address"];
                    userInfo.Key = (string)test[0]["key"];

                    return userInfo;

                }else if (test != null && method == "GET")
                {

                    userInfo.Id = Convert.ToInt64(test["id"]);
                    userInfo.Name = (string)test["name"];
                    userInfo.Address = (string)test["address"];
                    userInfo.Key = (string)test["key"];

                    return userInfo;
                }
                else
                {
                    return null;
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}