namespace CustomersDAL;

public interface IRepository<EntityType, EntityKey> : IDisposable
{
    IEnumerable<EntityType> GetEntities();
    EntityType GetEntityByKey(EntityKey entityKey);
    bool AddEntity(EntityType entityObject);
}
