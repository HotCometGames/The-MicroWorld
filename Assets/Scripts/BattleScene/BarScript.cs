using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Slider slider;


    public void SetMax(int max)
    {
        slider.maxValue = max;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
