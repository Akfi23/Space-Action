using UnityEngine;
using NaughtyAttributes;

namespace Kuhpik
{
    [CreateAssetMenu(menuName = "Config/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        // Example
        // [SerializeField] [BoxGroup("Moving")] private float moveSpeed;
        // public float MoveSpeed => moveSpeed;

        [SerializeField] private float playerMoveSpeed;
        [SerializeField] [Range(3,30)] private int resourceRespawnTime;
        public float PlayerMoveSpeed => playerMoveSpeed;
        public int ResourceRespawnTime => resourceRespawnTime;
    }
}