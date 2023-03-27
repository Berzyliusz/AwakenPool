using UnityEngine;

namespace Pool.Inputs
{
    public class InputWrapper : IInputs
    {
        public bool RotateRight => Input.GetKey(KeyCode.D);

        public bool RotateLeft => Input.GetKey(KeyCode.A);

        public bool IncreaseForce => Input.GetKeyDown(KeyCode.W);

        public bool DecreaseForce => Input.GetKeyDown(KeyCode.S);

        public bool ApplyForce => Input.GetKeyDown(KeyCode.Space);

        public bool RestartGame => Input.GetKeyDown(KeyCode.R);

        public bool AnyInput => Input.anyKeyDown;
    }
}