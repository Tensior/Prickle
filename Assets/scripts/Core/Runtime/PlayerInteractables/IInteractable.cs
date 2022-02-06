namespace Core
{
    public interface IInteractable<in T> where T : class
    {
        void OnInteract(T player);
    }
}