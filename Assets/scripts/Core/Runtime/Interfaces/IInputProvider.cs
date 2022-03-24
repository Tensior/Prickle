namespace Core.Interfaces
{
    public interface IInputProvider
    {
        bool IsMoveLeft { get; }
        bool IsMoveRight { get; }
        bool IsFire { get; }
        bool IsJump { get; }
        bool IsPause { get; }
        void SetGameplayActive(bool isActive);
    }
}