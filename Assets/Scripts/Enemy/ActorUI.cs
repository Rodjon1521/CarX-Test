using TowerDefence;
using UI;
using UnityEngine;

namespace Enemy
{
    public class ActorUI : MonoBehaviour
    {
        public HPBar HpBar;
        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }
        
        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
            {
                Construct(health);
            }
        }
        
        private void UpdateHpBar()
        {
            HpBar.SetValue(_health.current, _health.max);
        }
    }
}