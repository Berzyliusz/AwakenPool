namespace Pool.Inputs
{
    public interface IInputs
    {
        bool RotateRight { get; }
        bool RotateLeft { get; }
        bool IncreaseForce { get; }
        bool DecreaseForce { get; }
        bool ApplyForce { get; }
        bool RestartGame { get; }
        bool AnyInput { get; }
    }
}