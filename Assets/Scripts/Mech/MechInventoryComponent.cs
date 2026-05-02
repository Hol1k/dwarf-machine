using System;
using System.Collections.Generic;
using Loot;
using UnityEngine;

namespace Mech
{
    public class MechInventoryComponent : MonoBehaviour, IInventoryData
    {
        public IReadOnlyDictionary<LootType, float> Loot => _loot;
        
        private Dictionary<LootType, float> _loot;
        
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
    }
}