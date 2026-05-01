using System;
using System.Collections.Generic;
using Loot;
using UnityEngine;

namespace Character
{
    public class CharacterInventoryComponent : MonoBehaviour
    {
        private Dictionary<LootType, float> _loot;
        [Min(0)] public float maxOreLootCount;
        [Min(0)] public float maxRareOreLootCount;
        [Min(0)] public float maxWoodLootCount;
        [Min(0)] public float maxArtifactLootCount;

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
            _loot.Add(type, amount);
        }
    }
}