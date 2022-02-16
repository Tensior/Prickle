namespace Core.Interfaces
{
    public interface ITypedAmount
    {
        EntityType Type { get; } //used to distinguish enemies from allies
        int Amount { get; } //damage it deals
        bool IsFullAmount { get; } //does it kill immediately
    }
}