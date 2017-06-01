using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Mobile.Infrastructure
{
    public interface IStorage
    {
        T GetFirst<T>() where T : new();
        IEnumerable<T> Get<T>() where T : new();
        void Save<T>(T item) where T : new();
        void SaveAll<T>(IEnumerable<T> list);

        void Delete<T>(T item) where T : new();
    }
}