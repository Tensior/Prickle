namespace Core.Interfaces
{
    public interface IInteractable<in T> where T : class
    {
        void OnInteract(T player);
    }
}