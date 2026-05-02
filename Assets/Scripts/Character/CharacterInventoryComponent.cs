using System;
using System.Collections.Generic;
using Loot;
using Mech;
using UnityEngine;

namespace Character
{
    public class CharacterInventoryComponent : MonoBehaviour, IInventoryData
    {
        public IReadOnlyDictionary<LootType, float> Loot { get; }
        
        private Dictionary<LootType, float> _loot;
        
        [SerializeField] [Min(0)] private float maxOreLootCount;
        [SerializeField] [Min(0)] private float maxRareOreLootCount;
        [SerializeField] [Min(0)] private float maxWoodLootCount;
        [SerializeField] [Min(0)] private float maxArtifactLootCount;

        private void Awake()
        {
            _loot = new Dictionary<LootType, float>();
            
            foreach (LootType lootType in Enum.GetValues(typeof(LootType)))
            {
                _loot.Add(lootType, 0);
            }
        }

        public void AddLoot(LootType type, float amount)
        {
            _loot[type] += amount;
        }

        public float GetMaxLootCount(LootType type)
        {
            switch (type)
            {
                case LootType.Ore:
                    return maxOreLootCount;
                case LootType.RareOre:
                    return maxRareOreLootCount;
                case LootType.Wood:
                    return maxWoodLootCount;
                case LootType.Artifact:
                    return maxArtifactLootCount;
                default:
                    return 0;
            }
        }

        public float GetRemainingCapacity(LootType type)
        {
            return GetMaxLootCount(type) - _loot[type];
        }
    }
}