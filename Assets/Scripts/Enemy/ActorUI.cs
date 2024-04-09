using TowerDefence;
using UI;
using UnityEngine;

namespace Enemy
{
    public class ActorUI : MonoBehaviour
    {
        public HPBar HpBar;

        private TowerDefence.Enemy enemy;

        private void Start()
        {
            enemy = GetComponent<TowerDefence.Enemy>();
            enemy.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            HpBar.SetValue(enemy.CurrentHp, enemy.MaxHp);
        }
    }
}