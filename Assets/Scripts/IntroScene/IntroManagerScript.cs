using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManagerScript : MonoBehaviour
{
    public DataManager dataManager;
    public FirstBacteriaScript[] firstBacteriaScript;
    public GameObject chooseFirstBacteria;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadData();
        if (dataManager.gameData.started == false)
        {
            Begin();
        } else {
            Countinue();
        }
    }

    void Begin()
    {
        chooseFirstBacteria.SetActive(true);
        for (int i = 0; i < firstBacteriaScript.Length; i++)
        {
            firstBacteriaScript[i].SetUp();
        }
    }

    void Countinue()
    {
        SwitchScene("Lab");
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
