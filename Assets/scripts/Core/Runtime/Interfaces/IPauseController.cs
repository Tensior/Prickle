namespace Core.Interfaces
{
    public interface IPauseController
    {
        bool IsPaused { get; }
        void TogglePause();
    }
}