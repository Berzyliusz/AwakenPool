using UnityEngine;

namespace AwakenPool.Gameplay
{
    public class Cue : MonoBehaviour
    {
        [field: SerializeField] public Transform CueRotationObject { get; private set; }
        [field: SerializeField] public Transform CueVisualObject { get; private set; }

        const float minimalCueOffset = -0.3f;
        Vector3 originalCueScale;
        float originalCueOffset;

        public Vector3 Position => CueVisualObject.position;

        public void SetCueVisible(bool isVisible)
        {
            CueVisualObject.gameObject.SetActive(isVisible);
        }

        public void SetCuePosition(Vector3 newPosition)
        {
            CueRotationObject.position = newPosition;
        }

        public void RotateCue(float rotationAmount)
        {
            CueRotationObject.Rotate(0, rotationAmount, 0);
        }

        public void AdjustCueVisualObjectPositionAndScale(float currentForce, float maxForce)
        {
            CueVisualObject.localScale = new Vector3(
                originalCueScale.x,
                originalCueScale.y * currentForce,
                originalCueScale.z
                );
                
            CueVisualObject.localPosition = new Vector3(
                CueVisualObject.localPosition.x,
                CueVisualObject.localPosition.y,
                Mathf.Lerp(minimalCueOffset, originalCueOffset, currentForce / maxForce));
        }

        void Awake()
        {
            originalCueScale = CueVisualObject.localScale;
            originalCueOffset = CueVisualObject.localPosition.z;
        }
    }
}