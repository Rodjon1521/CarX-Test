using System;

namespace Enemy
{
    public interface IHealth
    {
        event Action HealthChanged;
        int current { get; set; }
        int max { get; set; }
        void TakeDamage(int damage);
        public void RefreshHealth();
    }
}