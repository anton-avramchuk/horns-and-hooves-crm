namespace HornsAndHoovesCrm.Domain;

public interface IEntity
{
        
}

public interface IEntity<out TKey> : IEntity where TKey : struct
{
    /// <summary>
    /// Unique identifier for this entity.
    /// </summary>
    TKey Id { get; }
}