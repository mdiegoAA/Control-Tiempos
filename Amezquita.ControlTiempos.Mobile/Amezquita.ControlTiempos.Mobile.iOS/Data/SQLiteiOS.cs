using Amezquita.ControlTiempos.Mobile.Infrastructure;
using SQLite;
using System;
using System.IO;

namespace Amezquita.ControlTiempos.Mobile.iOS.Data
{
    public class SQLiteiOS : ISQLiteLocator
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "AmezCT.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);
            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}