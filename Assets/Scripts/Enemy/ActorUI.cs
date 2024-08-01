using UI;
using UnityEngine;

namespace Enemy
{
    public class ActorUI : MonoBehaviour
    {
        public HPBar hpBar;
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
            hpBar.SetValue(_health.current, _health.max);
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.HealthChanged += UpdateHpBar;
            }
        }

        private void OnDisable()
        {
            _health.HealthChanged -= UpdateHpBar;
        }
    }
}