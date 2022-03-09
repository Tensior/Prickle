namespace Core.Interfaces
{
    public interface IEntity
    {
        EntityType Type { get; } //used to distinguish enemies from allies
    }
}