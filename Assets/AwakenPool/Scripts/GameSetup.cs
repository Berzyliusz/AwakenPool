using AwakenPool.Gameplay;
using UnityEngine;

namespace AwakenPool
{
    public class GameSetup : MonoBehaviour
    {
        [field: SerializeField] public Vector2 ForceMinMax { get; private set; }
        [field: SerializeField] public int MaxMoves { get; private set; }
        [field: SerializeField] public int ScoreToWin { get; private set; }

        [field: SerializeField] public Ball PlayableBall { get; private set; }
        [field: SerializeField] public Cue Cue { get; private set; }
        [field: SerializeField] public Ball[] Balls { get; private set; }
        [field: SerializeField] public Transform[] Posts { get; private set; }
    }
}