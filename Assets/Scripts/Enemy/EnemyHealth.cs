using System;
using UnityEngine;

namespace TowerDefence
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private int _current = 100;

        [SerializeField] 
        private int _max = 100;

        public event Action HealthChanged;

        public int Current
        {
            get => _current;
            set => _current = value;
        }

        public int Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(int damage)
        {
            if (Current <= 0)
            {
                gameObject.SetActive(false);
                return;
            }
            _current -= damage;
            
            HealthChanged?.Invoke();
        }

        public void Reset()
        {
            _current = _max;
            HealthChanged?.Invoke();
        }
    }
}