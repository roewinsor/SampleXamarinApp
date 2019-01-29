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

namespace bcapp.Model
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Key { get; set; }

        public UserInfo()
        {

            this.Id = 0;
            this.Name = " ";
            this.Address = " ";
            this.Key = " ";

        }


    }

   
}