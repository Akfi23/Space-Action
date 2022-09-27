using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    [CreateAssetMenu(menuName = "Config/PlayerConfig")]

    public class PlayerValuesConfig : ScriptableObject
    {
        [SerializeField] private float attackRadiusBase;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float moveSpeedBase;
        [SerializeField] private float damageBase;
        [SerializeField] private float armorBase;
        [SerializeField] private float healthRegenBase;
        [SerializeField] private float maxHealthBase;

        public float AttackRadiusBase => attackRadiusBase;
        public float AttackSpeed => attackSpeed;
        public float MoveSpeedBase => moveSpeedBase;
        public float DamageBase => damageBase;
        public float ArmorBase => armorBase;
        public float HealthRegenBase => healthRegenBase;
        public float MaxHealthBase => maxHealthBase;
    }
}
