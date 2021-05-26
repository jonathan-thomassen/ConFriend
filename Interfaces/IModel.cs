namespace ConFriend.Interfaces
{
    public interface IModel
    {
        string ToSQL();

        string Identity();

        // static ModelTypes somting { get; set;}
        // ModelTypes DataType { get; }

        //please also implement:
        //  string IdentitySQL { get; }
    }
}
