
using DemoLibrary.Models;

namespace DemoLibrary.Utilities
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T>(string sql);

        void SaveData<T>(T person, string sql);
    }
}
