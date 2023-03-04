using UnityEngine;

namespace AwakenPool.Inputs
{
    public interface IInputs
    {
        bool RotateRight { get; }
        bool RotateLeft { get; }
        bool IncreaseStrength { get; }
        bool DecreaseStrength { get; }
        bool ApplyForce { get; }
    }

    public class InputWrapper : IInputs
    {
        public bool RotateRight => Input.GetKey(KeyCode.D);

        public bool RotateLeft => Input.GetKey(KeyCode.A);

        public bool IncreaseStrength => Input.GetKeyDown(KeyCode.W);

        public bool DecreaseStrength => Input.GetKeyDown(KeyCode.S);

        public bool ApplyForce => Input.GetKeyDown(KeyCode.Space);
    }
}