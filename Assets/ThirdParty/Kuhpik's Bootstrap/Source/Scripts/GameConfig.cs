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
        [SerializeField] [Range(0.05f, 2f)] private float fireRate;
        [SerializeField] private BulletComponent bulletPrefab;

        public float PlayerMoveSpeed => playerMoveSpeed;
        public int ResourceRespawnTime => resourceRespawnTime;
        public float FireRate => fireRate;
        public BulletComponent BulletPrefab => bulletPrefab;
    }
}