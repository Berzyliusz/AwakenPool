using AwakenPool.Gameplay;
using UnityEngine;

namespace AwakenPool
{
    public class GameSetup : MonoBehaviour
    {
        [Tooltip("Global multiplier for force, so we can operate on sane numbers. " +
            "Larger value means harder hits on ball.")]
        [field: SerializeField] public float ForceMultiplier { get; private set; } = 1.0f;
        [Tooltip("How fast is the cue rotating around playable ball. " +
            "Larger value means faster rotation.")]
        [field: SerializeField] public float RotationSpeed { get; private set; } = 100;
        [Tooltip("How much we increase the cue force with each tap button." +
            "Larger value means bigger increase, thus fewer steps to regulate the force.")]
        [field: SerializeField] public float ForceIncreaseStep { get; private set; } = 0.5f;

        [Tooltip("Minimum and maximum force values for cue. Default value is 1." +
            "Larger value means we can set harder force.")]
        [field: SerializeField] public Vector2 ForceMinMax { get; private set; } = new Vector2(0.2f, 2f);
        [Tooltip("How many moves player can make before loosing.")]
        [field: SerializeField] public int MaxMoves { get; private set; } = 10;
        [Tooltip("How many points player needs to win.")]
        [field: SerializeField] public int ScoreToWin { get; private set; } = 30;

        [field: SerializeField] public Ball PlayableBall { get; private set; }
        [field: SerializeField] public Cue Cue { get; private set; }
        [field: SerializeField] public Ball[] Balls { get; private set; }
        [field: SerializeField] public Transform[] Posts { get; private set; }
    }
}