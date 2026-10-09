using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BacteriaSpawnerScript : MonoBehaviour
{
    public GameObject bacteriaPrefab;
    public GameObject bacteriaMenu;
    public GameObject lidAndShadow;
    public GameObject testButton;
    public GameObject matchGame;
    public BacteriaMenuScript bacteriaMenuScript;
    public TestButtonScript testButtonScript;
    public MatchGameScript matchGameScript;
    public DataManager dataManager;
    public List<BacteriaData> bacteriaList;
    public GameObject[] allBacteria;
    public int bacteriaAmount;
    public int selectedBacteria;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadData();
        bacteriaAmount = dataManager.gameData.temporaryBacteria.Count;
        bacteriaList = dataManager.gameData.temporaryBacteria;
        allBacteria = new GameObject[bacteriaAmount];


        for (int i = 0; i < bacteriaAmount; i++)
        {
            GameObject Bacteria = Instantiate(bacteriaPrefab, PickRandomPos(), Quaternion.identity);

            Bacteria.GetComponent<BacteriaScript>().bacteriaMenu = bacteriaMenu;
            Bacteria.GetComponent<BacteriaScript>().bacteriaMenuScript = bacteriaMenuScript;
            Bacteria.GetComponent<BacteriaScript>().bacteriaSpawnerScript = this;
            Bacteria.GetComponent<BacteriaScript>().testButton = testButton;
            Bacteria.GetComponent<BacteriaScript>().testButtonScript = testButtonScript;
            Bacteria.GetComponent<BacteriaScript>().bacteriaData = bacteriaList[i];
            Bacteria.GetComponent<BacteriaScript>().bacteriaData.id = i;

            allBacteria[i] = Bacteria;
        }
    }

    Vector3 PickRandomPos()
    {
        int randomX = Random.Range(-10, 10);
        int randomY = Random.Range(-4, 4);
        return new Vector3(randomX, randomY, 0);
    }

    public void PauseAll()
    {
        for (int i =0; i < bacteriaAmount; i++)
        {
            allBacteria[i].GetComponent<BacteriaScript>().Pause();
        }
    }

    public void PauseAllButSelected()
    {
        for (int i =0; i < bacteriaAmount; i++)
        {
            if (i != selectedBacteria)
            {
                allBacteria[i].GetComponent<BacteriaScript>().Pause();
            } else {
                allBacteria[i].GetComponent<BacteriaScript>().shouldMove = true;
            }
        }
    }

    public void UnPauseAll()
    {
        for (int i =0; i < bacteriaAmount; i++)
        {
            allBacteria[i].GetComponent<BacteriaScript>().shouldMove = true;
            allBacteria[i].GetComponent<BacteriaScript>().allowPress = true;
        }
    }

    public void CatchBacteria()
    {
        PauseAllButSelected();
        for (int i = 0; i < bacteriaAmount; i++)
        {
            if (i != selectedBacteria)
            {
                allBacteria[i].SetActive(false);
            } else {
                allBacteria[i].GetComponent<BacteriaScript>().allowPress = false;
            }
        }
        bacteriaMenu.SetActive(false);
        lidAndShadow.SetActive(true);
    }

    public void SaveBacteriaData()
    {
        dataManager.gameData.Bacterias.Add(allBacteria[selectedBacteria].GetComponent<BacteriaScript>().bacteriaData);
        dataManager.SaveData();
    }

    public void BacteriaCaught()
    {
        allBacteria[selectedBacteria].GetComponent<BacteriaScript>().bacteriaData.id = dataManager.gameData.Bacterias.Count;
        SaveBacteriaData();
        allBacteria[selectedBacteria].SetActive(false);
        lidAndShadow.GetComponent<LidAndShadowScript>().MoveToBag();
    }

    public void StartMatchGame()
    {
        PauseAll();
        matchGame.SetActive(true);
        matchGameScript.StartMatch();
    }
    
    public void StopMatchGame()
    {
        UnPauseAll();
    }

    public void ShowBacteriaStats()
    {
        allBacteria[selectedBacteria].GetComponent<BacteriaScript>().setBacteria();
        bacteriaMenu.SetActive(true);
    }

    public void GetReadyForShowCase()
    {
        allBacteria[selectedBacteria].SetActive(true);
        allBacteria[selectedBacteria].GetComponent<BacteriaScript>().Pause();
        allBacteria[selectedBacteria].transform.position = new Vector3(0, 0, -2);
    }

}
