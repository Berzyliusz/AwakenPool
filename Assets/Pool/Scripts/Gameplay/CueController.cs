using Pool.Inputs;
using System;
using UnityEngine;

namespace Pool.Gameplay
{
    public class CueController
    {
        public event Action OnForceApplied;

        readonly float forceMultiplier;
        readonly float rotationSpeed;
        readonly float forceIncreaseStep;

        IInputs inputs;
        Ball playableBall;
        Cue cue;
        Transform playableBallTransform;
        Vector2 forceMinMax;
        float currentForce;

        public CueController(IInputs inputs, GameSetup setup)
        {
            this.inputs = inputs;
            playableBall = setup.PlayableBall;
            cue = setup.Cue;
            forceMinMax = setup.ForceMinMax;
            playableBallTransform = playableBall.transform;
            currentForce = 1.0f;
            forceMultiplier = setup.ForceMultiplier;
            rotationSpeed = setup.RotationSpeed;
            forceIncreaseStep = setup.ForceIncreaseStep;
        }

        public void SetCueActive(bool isActive)
        {
            if(isActive)
            {
                cue.SetCuePosition(playableBallTransform.position);
                currentForce = 0f;
                AdjustCueForce(1.0f);
                cue.SetCueVisible(true);
            }
            else
            {
                cue.SetCueVisible(false);
            }
        }

        public void Update()
        {
            cue.SetCuePosition(playableBallTransform.position);

            if (inputs.RotateLeft)
                cue.RotateCue(rotationSpeed * Time.deltaTime);
            else
            if(inputs.RotateRight)
                cue.RotateCue(-rotationSpeed * Time.deltaTime);

            if(inputs.IncreaseForce)
                AdjustCueForce(forceIncreaseStep);
            else
            if(inputs.DecreaseForce)
                AdjustCueForce(-forceIncreaseStep);

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
            var forceToApply = directionNormalized * currentForce * forceMultiplier;
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