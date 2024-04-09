using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HPBar : MonoBehaviour
    {
        public Image healthBar;
        public Image damagedBar;
    
        [SerializeField] private float lerpSpeed = 0.05f;

        public void SetValue(int current, int max)
        {
            healthBar.fillAmount = (float)current / (float)max;
            damagedBar.DOFillAmount(healthBar.fillAmount, 0.5f).SetEase(Ease.InQuint);
        }
    }
}