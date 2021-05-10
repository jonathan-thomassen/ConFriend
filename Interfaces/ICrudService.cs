
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
        void Init(ModelTypes DataType);
        bool Create(T item);
        List<T> GetAll();
        T GetFromId(int id, int id2 = 0);
        bool Delete(int id, int id2 = 0);
        bool Update(T item);
        List<T> GetFiltered(string filter, FilterType filterType);
       
    }
}