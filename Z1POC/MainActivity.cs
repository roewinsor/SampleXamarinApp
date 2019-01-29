using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Graphics;

using System;
using System.IO;
using Android.Content;
using System.Collections.Generic;
using Z1POC.Service;
using Z1POC.Model;

namespace Z1POC
{
    [Activity(Label = "Z1POC", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button GoButton = FindViewById<Button>(Resource.Id.button1);
            GoButton.Visibility = ViewStates.Invisible;
            GoButton.Click += Button_Click;

            TextView clickView = FindViewById<TextView>(Resource.Id.textView1);
            clickView.Click += Link_Click;
            

            EditText keyEntry = FindViewById<EditText>(Resource.Id.editText1);
            keyEntry.Click += KeyEdit_Click;

            TextView nameView = FindViewById<TextView>(Resource.Id.textView2);
            TextView addressView = FindViewById<TextView>(Resource.Id.textView4);

            keyEntry.Visibility = ViewStates.Invisible;
            nameView.Visibility = ViewStates.Invisible;
            addressView.Visibility = ViewStates.Invisible;



        }

        private void KeyEdit_Click(object sender, EventArgs e)
        {
            EditText keyEntry =  FindViewById<EditText>(Resource.Id.editText1);
            keyEntry.Text = "";
        }

        private void Link_Click(object sender, EventArgs e)
        {
            Button GoButton = FindViewById<Button>(Resource.Id.button1);
            TextView clickView1 = FindViewById<TextView>(Resource.Id.textView1);
            EditText keyEntry = FindViewById<EditText>(Resource.Id.editText1);

            keyEntry.Visibility = ViewStates.Visible;
            GoButton.Visibility = ViewStates.Visible;
            keyEntry.Text = "Please enter key here and click Go";
            clickView1.SetTextColor(Color.Green);
       
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            try
            {
               

                EditText keyEntry = FindViewById<EditText>(Resource.Id.editText1);
                TextView nameView = FindViewById<TextView>(Resource.Id.textView2);
                TextView addressView = FindViewById<TextView>(Resource.Id.textView4);
                string apiURL = "http://192.168.254.104:56251/api/userinfo/" + keyEntry.Text;


                UserInfo getUser = await Core.ClientService.GetUserInfo("GET", null, apiURL);

                if (getUser != null)
                {
                    nameView.Visibility = ViewStates.Visible;
                    addressView.Visibility = ViewStates.Visible;
                    nameView.Text = "Name: " + getUser.Name;
                    addressView.Text = "Address: " + getUser.Address;
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }



 }

