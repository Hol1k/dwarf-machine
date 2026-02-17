using System;
using UnityEngine;

namespace Entities
{
    public abstract class StatsComponent : MonoBehaviour
    {
        public abstract bool IsDied { get; protected set; }
        public abstract event Action OnDeath; 
    }
}