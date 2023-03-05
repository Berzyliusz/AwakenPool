using AwakenPool.Inputs;
using System;
using UnityEngine;

namespace AwakenPool
{
    /// <summary>
    /// Handles the pool cue, including movement, rotation and force.
    /// </summary>
    public class CueController : MonoBehaviour
    {
        public event Action OnForceApplied;

        [SerializeField] float forceMul = 1.0f;
        const float minimalCueOffset = -0.3f;

        IInputs inputs;
        Ball playableBall;
        Transform playableBallTransform;
        Transform cueRotator;
        Transform cueVisual;
        Vector2 forceMinMax;
        Vector3 originalCueScale;
        float originalCueOffset;

        float currentForce;

        public void Initialize(IInputs inputs, GameSetup setup)
        {
            this.inputs = inputs;
            playableBall = setup.PlayableBall;
            cueRotator = setup.CueParent;
            cueVisual = setup.CueObject;
            forceMinMax = setup.ForceMinMax;
            playableBallTransform = playableBall.transform;
            originalCueScale = setup.CueObject.localScale;
            originalCueOffset = setup.CueObject.localPosition.z;
            currentForce = 1.0f;

            enabled = false;
        }

        public void SetCueActive(bool isActive)
        {
            if(isActive)
            {
                enabled = true;
                UpdateCuePosition();
                currentForce = 0f;
                AdjustCueForce(1.0f);
                cueVisual.gameObject.SetActive(true);
            }
            else
            {
                enabled = false;
                cueVisual.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            var rotationSpeed = 100 * Time.deltaTime;
            var forceIncrease = 0.5f;
            UpdateCuePosition();

            if (inputs.RotateLeft)
                RotateCue(rotationSpeed);
            else
            if(inputs.RotateRight)
                RotateCue(-rotationSpeed);

            if(inputs.IncreaseForce)
                AdjustCueForce(forceIncrease);
            else
            if(inputs.DecreaseForce)
                AdjustCueForce(-forceIncrease);

            if (inputs.ApplyForce)
                ApplyForceToBall();
        }

        void UpdateCuePosition()
        {
            cueRotator.position = playableBallTransform.position;
        }

        void RotateCue(float rotation)
        {
            cueRotator.Rotate(0, rotation, 0);
        }

        void AdjustCueForce(float force)
        {
            currentForce += force;
            currentForce = Mathf.Clamp(currentForce, forceMinMax.x, forceMinMax.y);
            AdjustCueVisualObjectPositionAndScale();
        }

        private void AdjustCueVisualObjectPositionAndScale()
        {
            cueVisual.localScale = originalCueScale * currentForce;
            cueVisual.localPosition = new Vector3(
                cueVisual.localPosition.x,
                cueVisual.localPosition.y,
                Mathf.Lerp(minimalCueOffset, originalCueOffset, currentForce / forceMinMax.y));
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
            var cuePosWithoutHeight = new Vector3(cueVisual.position.x, playableBallTransform.position.y, cueVisual.position.z);
            var directionNormalized = (playableBallTransform.position - cuePosWithoutHeight).normalized;
            return directionNormalized;
        }
    }
}