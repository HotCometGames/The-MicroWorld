using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotBarScript : MonoBehaviour
{
    public DataManager dataManager;
    public List<GameObject> spaces = new List<GameObject>();
    public TextMeshProUGUI tvText;
    public TextMeshProUGUI tvOverlay;
    public int currentSpace = 0;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadData();
        for (int i = 0; i < dataManager.gameData.Bacterias.Count; i++)
        {
            GameObject thisSpace = spaces[i];
            thisSpace.GetComponent<SpaceScript>().bacteriaData = dataManager.gameData.Bacterias[i];
            thisSpace.GetComponent<SpaceScript>().SetUp();
            thisSpace.GetComponent<SpaceScript>().id = i;
        }
    }

    public void SetSpaces()
    {
        for (int i = 0; i < spaces.Count; i ++)
        {
            if (i != currentSpace)
            {
                SpaceScript spaceScript = spaces[i].GetComponent<SpaceScript>();
                RectTransform spacePos = spaceScript.thisTransform;
                spaceScript.thisTransform.anchoredPosition = new Vector2(spacePos.anchoredPosition.x, -180);
            }
        }

        setText();
    }

    public void setText()
    {
        SpaceScript spaceScript = spaces[currentSpace].GetComponent<SpaceScript>();
        string Text = "";
        int level = 0;
        if (spaceScript.bacteriaData.exp < 10)
        {
            level = 1;
        } else if (spaceScript.bacteriaData.exp < 30)
        {
            level = 2;
        } else if (spaceScript.bacteriaData.exp < 60)
        {
            level = 3;
        } else if (spaceScript.bacteriaData.exp < 100)
        {
            level = 4;
        } else if (spaceScript.bacteriaData.exp < 150)
        {
            level = 5;
        } else if (spaceScript.bacteriaData.exp < 210)
        {
            level = 6;
        } else if (spaceScript.bacteriaData.exp < 280)
        {
            level = 7;
        } else if (spaceScript.bacteriaData.exp < 360)
        {
            level = 8;
        } else if (spaceScript.bacteriaData.exp < 450)
        {
            level = 9;
        } else if (spaceScript.bacteriaData.exp < 550)
        {
            level = 10;
        }
        Text = "Health: " + (spaceScript.bacteriaData.health + spaceScript.bacteriaData.addedHealth) + "\nDamage: " + (spaceScript.bacteriaData.damage + spaceScript.bacteriaData.addedDamage) +"\nAgility: " + (spaceScript.bacteriaData.agility + spaceScript.bacteriaData.addedAgility) + "\nLevel: " + level;
        tvText.text = Text;
        tvOverlay.text = Text;
    }
}
