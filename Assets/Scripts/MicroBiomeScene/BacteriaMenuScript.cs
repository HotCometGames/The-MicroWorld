using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BacteriaMenuScript : MonoBehaviour
{
    public Text info;
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void setText(int health, int damage, float agility)
    {

        text = "Battle stats:\n" + "Health: " + health + "\nDamage: " + damage + "\nAgility: " + agility;

        info.text = text;
    }
}
