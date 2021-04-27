namespace ConFriend.Interfaces
{
    public interface ISQLService<T>
    {
        string QueryBuilder();
        bool SqlCommand();
        void OpenDB();
        void CloseDB();
        void OnRead();
    }
}
