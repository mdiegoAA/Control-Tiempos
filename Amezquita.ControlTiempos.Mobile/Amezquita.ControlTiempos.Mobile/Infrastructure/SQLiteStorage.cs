// ----------------------------------------------------------------------------------------------
// <copyright file="SQLiteStorage.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace Amezquita.ControlTiempos.Mobile.Infrastructure
{
    public interface ISQLiteLocator
    {
        #region Instance Methods

        SQLiteConnection GetConnection();

        #endregion
    }

    public class SQLiteStorage : IStorage
    {
        #region Readonly & Static Fields

        private readonly SQLiteConnection _connection;
        private static readonly object _locker = new object();

        #endregion

        #region C'tors

        public SQLiteStorage(ISQLiteLocator manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            _connection = manager.GetConnection();
        }

        #endregion

        #region Instance Methods

        public void Delete<T>(T item) where T : new()
        {
            lock (_locker)
            {
                _connection.Delete(item);
            }
        }

        public IEnumerable<T> Get<T>() where T : new()
        {
            lock (_locker)
            {
                _connection.CreateTable<T>();

                return _connection.Table<T>()
                                  .ToList();
            }
        }

        public T GetFirst<T>() where T : new()
        {
            lock (_locker)
            {
                //_connection.CreateTable<T>();

                var a = _connection.Table<T>()
                                  .FirstOrDefault();
                return a;
            }
        }

        public void Save<T>(T item) where T : new()
        {
            lock (_locker)
            {
                _connection.CreateTable<T>();
                _connection.InsertOrReplace(item);
            }
        }

        public void SaveAll<T>(IEnumerable<T> list)
        {
            lock (_locker)
            {
                var type = list.GetType()
                               .GenericTypeArguments
                               .Single();

                _connection.CreateTable(type);
                _connection.InsertAll(list, type);
            }
        }

        #endregion
    }
}
