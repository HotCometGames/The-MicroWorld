using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FirstBacteriaScript : MonoBehaviour
{
    public BacteriaData bacteriaData;
    public DataManager dataManager;
    public Sprite[] sprites;
    public GameObject bacteria;
    public TextMeshProUGUI thisText;
    // Start is called before the first frame update
    public void SetUp()
    {
        bacteria.GetComponent<Image>().sprite = sprites[bacteriaData.sprite];
        string Text = "";
        Text = "Health: " + bacteriaData.health + "\nDamage: " + bacteriaData.damage +"\nAgility: " + bacteriaData.agility;
        thisText.text = Text;
    }

    public void Selected()
    {
        dataManager.LoadData();
        dataManager.gameData.Bacterias = new List<BacteriaData>();
        dataManager.gameData.Bacterias.Add(bacteriaData);
        dataManager.gameData.started = true;
        dataManager.SaveData();
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
