using AwakenPool.Inputs;
using System;
using UnityEngine;

namespace AwakenPool
{
    /// <summary>
    /// Handles the pool cue.
    /// </summary>
    public class CueController : MonoBehaviour
    {
        public event Action OnForceApplied;

        IInputs inputs;

        Ball playableBall;
        Transform playableBallTransform;
        Transform cueRotator;
        Transform cueVisual;

        [SerializeField] float forceMul = 1.0f;

        public void Initialize(IInputs inputs, Ball playableBall, Transform cueRotator, Transform cueVisual)
        {
            this.inputs = inputs;
            this.playableBall = playableBall;
            this.cueRotator = cueRotator;
            this.cueVisual = cueVisual;
            playableBallTransform = playableBall.transform;

            enabled = false;
        }

        public void SetCueActive(bool isActive)
        {
            if(isActive)
            {
                enabled = true;
                UpdateCuePosition();
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
            UpdateCuePosition();

            if (inputs.RotateLeft)
                RotateCue(rotationSpeed);
            else
            if(inputs.RotateRight)
                RotateCue(-rotationSpeed);

            AdjustCueForce();

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

        void AdjustCueForce()
        {
            // Store force up or down
            // Clamp force from params
            // Adjust cue length to the given force
        }

        void ApplyForceToBall()
        {
            var directionNormalized = CalculateNormalizedDirection();
            playableBall.Rigidbody.AddForce(directionNormalized * forceMul, ForceMode.Impulse);

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