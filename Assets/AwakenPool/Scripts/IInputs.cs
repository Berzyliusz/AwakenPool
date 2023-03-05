namespace AwakenPool.Inputs
{
    public interface IInputs
    {
        bool RotateRight { get; }
        bool RotateLeft { get; }
        bool IncreaseForce { get; }
        bool DecreaseForce { get; }
        bool ApplyForce { get; }
    }
}