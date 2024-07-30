using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _current;

        [SerializeField] 
        private int _max;

        public event Action HealthChanged;

        public int current
        {
            get => _current;
            set => _current = value;
        }

        public int max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(int damage)
        {
            if (current <= 0)
            {
                return;
            }
            _current -= damage;
            
            HealthChanged?.Invoke();
        }
    }
}