using System.Collections.Generic;

namespace ConFriend.Interfaces
{
    public interface ICrudService<T>
    {
        enum FilterType
        {
            Name,
            Type,
            StartTime,
            EndTime,
            Status,
            Room,
            Theme
        }

        bool Create(T item);
        List<T> GetAll();
        T GetFromId(int id);
        bool Delete(int id);
        bool Update(T item);
        List<T> GetFiltered(string filter, FilterType filterType);
    }
}