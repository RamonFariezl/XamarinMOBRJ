using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Xamarin.Forms;
using XamarinMOBRJ.Droid.Database;
using XamarinMOBRJ.Helpers;

[assembly: Dependency(typeof(DatabaseAccess))]
namespace XamarinMOBRJ.Droid.Database
{
    public class DatabaseAccess :IDatabaseAccess
    {
        public DatabaseAccess() { }

        public SQLiteAsyncConnection GetConnection()
        {
            var teste = GetData();

            return new SQLiteAsyncConnection(teste);
        }

        public static string GetData()
        {
            var sqlDbFileName = "DBEstados.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqlDbFileName);

            //var connection = new SQLiteAsyncConnection(path);

            return path;

        }

    }
}