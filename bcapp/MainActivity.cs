using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.IO;
using Android.Content;
using System.Collections.Generic;
using bcapp.Model;
using bcapp.Core;

namespace bcapp
{
    [Activity(Label = "Z0POC", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public  List<string> users = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.button1);
            Button viewinfoButton = FindViewById<Button>(Resource.Id.button2);

            button.Click += Button_Click;

            viewinfoButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(userInforAct));
   
                intent.PutStringArrayListExtra("info_list", users);

                StartActivity(intent);
            };
        }


       private async void Button_Click(object sender, EventArgs e)
       {
            EditText nameEntry = FindViewById<EditText>(Resource.Id.editText1);
            EditText addressEntry = FindViewById<EditText>(Resource.Id.editText2);
            EditText idEntry = FindViewById<EditText>(Resource.Id.editText3);

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string keyID = Guid.NewGuid().ToString().Substring(0, 8);
            string fileNameID = keyID;
            string filename = Path.Combine(path, fileNameID + ".txt");

            //Creating text file
            using (var streamWriter = new StreamWriter(filename, true))
            {
               await  streamWriter.WriteLineAsync("Name: " + nameEntry.Text);
               await  streamWriter.WriteLineAsync("Address: " + addressEntry.Text);
            }


            //adding to DB
            UserInfo addUser = new UserInfo();
            addUser.Name = nameEntry.Text;
            addUser.Address = addressEntry.Text;
            addUser.Key = keyID;
           
            //Call API Adding user
            UserInfo allUser = await Core.ClientService.GetUserInfo("POST", addUser);

           //check if success or not
            if (allUser != null)
            {

                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("File Saved");
                alert.SetMessage("Save Successfully, Please remember your ID: " + keyID);
                alert.SetButton("OK", (c, ev) =>
                {
                    nameEntry.Text = " ";
                    addressEntry.Text = " ";
                    idEntry.Text = fileNameID;

                });
                alert.Show();


            }
            else
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Error Saving File");
                alert.SetMessage("Error saving information, Please try again.");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
            }

            //read the file text and send to view window
            using (var streamReader = new StreamReader(filename))
            {
                users = new List<string>();
                string content = await streamReader.ReadToEndAsync();
                users.Add(content);
                System.Diagnostics.Debug.WriteLine(content);
            }

        }
    }
}

