namespace Entities
{
    public interface IDamageable
    {
        public float MaxHealth { get; set; }
        public float Health { get; set; }
        
        public void TakeDamage(float damage);
    }
}