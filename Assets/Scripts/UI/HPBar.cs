using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image ImageCurrent;

    public void SetValue(int current, int max)
    {
        ImageCurrent.fillAmount = (float)current / (float)max;
    }
}