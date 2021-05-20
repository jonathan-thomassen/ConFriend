
using System.Collections.Generic;
using System.Threading.Tasks;

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
        void Init_Composite(ModelTypes DataTypeA, ModelTypes DataTypeB, ModelTypes TrueDataType);
        Task<bool> Create(T item);
        Task<T> GetFromField(string customField);
        void ClearItemData();
        Task<List<T>> GetAll();
        Task<T> GetFromId(int id, int id2 = 0);
        Task<bool> Delete(int id, int id2 = 0);
        Task<bool> Update(T item);
        Task<List<T>> GetFiltered(int filterId, ModelTypes joinId, ModelTypes myId = ModelTypes.none);
        int GetLastId { get; }
     }
}