using System;
using UnityEngine;

namespace TowerDefence
{
    public class ActorUI : MonoBehaviour
    {
        public HPBar HpBar;

        private EnemyHealth _health;

        private void Start()
        {
            _health = GetComponent<EnemyHealth>();
            _health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            HpBar.SetValue(_health.Current, _health.Max);
        }
    }
}