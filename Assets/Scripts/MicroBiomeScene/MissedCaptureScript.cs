using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissedCaptureScript : MonoBehaviour
{
    public DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void goBack()
    {
        SwitchScene("Lab");
        dataManager.gameData.temporaryBacteria = new List<BacteriaData>();
    }
}
