using Amezquita.ControlTiempos.Mobile.Infrastructure;
using SQLite;
using System;
using System.IO;

namespace Amezquita.ControlTiempos.Mobile.Droid.Data
{
    public class SQLiteDroid : ISQLiteLocator
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "AmezCT.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}