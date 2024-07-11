namespace CustomersDAL;

public interface ISystemContext : IDisposable
{
    bool CommitChanges();
}
