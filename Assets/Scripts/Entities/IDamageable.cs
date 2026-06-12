using System;
using System.Threading.Tasks;

namespace Entities
{
    public interface IDamageable
    {
        public float MaxHealth { get; set; }
        public float Health { get; set; }
        event Action<float> OnTakeDamage;
        event Action OnDeath;
        
        public void TakeDamage(float damage);
    }
}