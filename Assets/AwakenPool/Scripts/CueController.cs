using AwakenPool.Inputs;
using System;
using UnityEngine;

namespace AwakenPool.Gameplay
{
    /// <summary>
    /// Handles the pool cue, including movement, rotation and force.
    /// </summary>
    public class CueController : MonoBehaviour
    {
        public event Action OnForceApplied;

        [SerializeField] float forceMul = 1.0f;

        IInputs inputs;
        Ball playableBall;
        Cue cue;
        Transform playableBallTransform;
        Vector2 forceMinMax;
        float currentForce;

        public void Initialize(IInputs inputs, GameSetup setup)
        {
            this.inputs = inputs;
            playableBall = setup.PlayableBall;
            cue = setup.Cue;
            forceMinMax = setup.ForceMinMax;
            playableBallTransform = playableBall.transform;
            currentForce = 1.0f;

            enabled = false;
        }

        public void SetCueActive(bool isActive)
        {
            if(isActive)
            {
                enabled = true;
                cue.SetCuePosition(playableBallTransform.position);
                currentForce = 0f;
                AdjustCueForce(1.0f);
                cue.SetCueVisible(true);
            }
            else
            {
                enabled = false;
                cue.SetCueVisible(false);
            }
        }

        void Update()
        {
            var rotationSpeed = 100 * Time.deltaTime;
            var forceIncrease = 0.5f;
            cue.SetCuePosition(playableBallTransform.position);

            if (inputs.RotateLeft)
                cue.RotateCue(rotationSpeed);
            else
            if(inputs.RotateRight)
                cue.RotateCue(-rotationSpeed);

            if(inputs.IncreaseForce)
                AdjustCueForce(forceIncrease);
            else
            if(inputs.DecreaseForce)
                AdjustCueForce(-forceIncrease);

            if (inputs.ApplyForce)
                ApplyForceToBall();
        }

        void AdjustCueForce(float force)
        {
            currentForce += force;
            currentForce = Mathf.Clamp(currentForce, forceMinMax.x, forceMinMax.y);
            cue.AdjustCueVisualObjectPositionAndScale(currentForce, forceMinMax.y);
        }

        void ApplyForceToBall()
        {
            var directionNormalized = CalculateNormalizedDirection();
            var forceToApply = directionNormalized * currentForce * forceMul;
            playableBall.Rigidbody.AddForce(forceToApply, ForceMode.Impulse);

            OnForceApplied?.Invoke();
            SetCueActive(false);
        }

        Vector3 CalculateNormalizedDirection()
        {
            var cuePosWithoutHeight = new Vector3(cue.Position.x, playableBallTransform.position.y, cue.Position.z);
            var directionNormalized = (playableBallTransform.position - cuePosWithoutHeight).normalized;
            return directionNormalized;
        }
    }
}