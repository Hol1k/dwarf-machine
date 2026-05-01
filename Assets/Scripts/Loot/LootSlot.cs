using System;

namespace Loot
{
    [Serializable]
    public struct LootSlot
    {
        public LootType type;
        public float amount;
    }
}