using AwakenPool.Inputs;
using System;
using UnityEngine;

namespace AwakenPool.Gameplay
{
    public class CueController : MonoBehaviour
    {
        public event Action OnForceApplied;

        [Tooltip("Global multiplier for force, so we can operate on sane numbers. " +
            "Larger value means harder hits on ball.")]
        [SerializeField] float forceMul = 1.0f;
        [Tooltip("How fast is the cue rotating around playable ball. " +
            "Larger value means faster rotation.")]
        [SerializeField] float rotationSpeed = 100;
        [Tooltip("How much we increase the cue force with each tap button." +
            "Larger value means bigger increase, thus fewer steps to regulate the force.")]
        [SerializeField] float forceIncreaseStep = 0.5f;

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