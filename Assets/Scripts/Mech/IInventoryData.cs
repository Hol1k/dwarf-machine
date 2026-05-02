using System.Collections.Generic;
using Loot;

namespace Mech
{
    public interface IInventoryData
    {
        IReadOnlyDictionary<LootType, float> Loot { get; }
    }
}