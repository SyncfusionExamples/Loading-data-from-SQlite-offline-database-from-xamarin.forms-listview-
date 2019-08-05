using SampleDemo;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using SQLitePCL;
using SQLite;

[assembly: Dependency(typeof(SQLite_UWP))]
namespace SampleDemo
{
    class SQLite_UWP : ISQLite
    {
        public SQLite_UWP()
        {

        }


        #region ISQLite implementation
        public SQLiteConnection GetConnection()
        {
            var databasename = "SampleSQLites.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, databasename);
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection 
            return conn;
        }
        #endregion
    }
}

