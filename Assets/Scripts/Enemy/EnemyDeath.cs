using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        private EnemyHealth _health;
        
        public event Action Happened;

        private void Awake()
        {
            _health = GetComponent<EnemyHealth>();
        }

        private void Start()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (_health.current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Happened?.Invoke();
        }
    }
}