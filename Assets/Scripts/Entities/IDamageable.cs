using System;
using System.Threading.Tasks;

namespace Entities
{
    public interface IDamageable
    {
        public float MaxHealth { get; set; }
        public float Health { get; set; }
        event Action<float> OnTakeDamage;
        
        public void TakeDamage(float damage);
    }
}