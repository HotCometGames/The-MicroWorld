using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class MatchGameScript : MonoBehaviour
{
    public Camera mainCamera;
    public BacteriaSpawnerScript bacteriaSpawnerScript;
    public List<GameObject> AllDNA;
    public GameObject DNA1;
    public GameObject DNA2;
    public GameObject DNA3;
    public int Slot1 = 0;
    public int Slot2 = 0;
    public List<int> avaiblePositions1 = new List<int> {1, 2, 3};
    public List<int> avaiblePositions2 = new List<int> {4, 5, 6};
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void StartMatch()
    {
        Slot1 = 0;
        Slot2 = 0;
        avaiblePositions1 = new List<int> {1, 2, 3};
        avaiblePositions2 = new List<int> {4, 5, 6};

        int DNA1RandomNumber = Random.Range(0, avaiblePositions1.Count);
        int random1 = avaiblePositions1[DNA1RandomNumber];
        avaiblePositions1.RemoveAt(DNA1RandomNumber);

        GameObject DNA1GameObject = Instantiate(DNA1, FindRightPosition(random1), Quaternion.identity);
        DNA1GameObject.GetComponent<DNAScript>().id = 1;
        DNA1GameObject.GetComponent<DNAScript>().matchGameScript = this;
        DNA1GameObject.GetComponent<DNAScript>().mainCamera = mainCamera;

        AllDNA.Add(DNA1GameObject);


        int DNA1RandomNumber2 = Random.Range(0, avaiblePositions2.Count);
        int random12 = avaiblePositions2[DNA1RandomNumber2];
        avaiblePositions2.RemoveAt(DNA1RandomNumber2);

        GameObject DNA1GameObject2 = Instantiate(DNA1, FindRightPosition(random12), Quaternion.identity);
        DNA1GameObject2.GetComponent<DNAScript>().id = 1;
        DNA1GameObject2.GetComponent<DNAScript>().matchGameScript = this;
        DNA1GameObject2.GetComponent<DNAScript>().mainCamera = mainCamera;

        AllDNA.Add(DNA1GameObject2);




        int DNA2RandomNumber = Random.Range(0, avaiblePositions1.Count);
        int random2 = avaiblePositions1[DNA2RandomNumber];
        avaiblePositions1.RemoveAt(DNA2RandomNumber);

        GameObject DNA2GameObject = Instantiate(DNA2, FindRightPosition(random2), Quaternion.identity);
        DNA2GameObject.GetComponent<DNAScript>().id = 2;
        DNA2GameObject.GetComponent<DNAScript>().matchGameScript = this;
        DNA2GameObject.GetComponent<DNAScript>().mainCamera = mainCamera;

        AllDNA.Add(DNA2GameObject);


        int DNA2RandomNumber2 = Random.Range(0, avaiblePositions2.Count);
        int random22 = avaiblePositions2[DNA2RandomNumber2];
        avaiblePositions2.RemoveAt(DNA2RandomNumber2);

        GameObject DNA2GameObject2 = Instantiate(DNA2, FindRightPosition(random22), Quaternion.identity);
        DNA2GameObject2.GetComponent<DNAScript>().id = 2;
        DNA2GameObject2.GetComponent<DNAScript>().matchGameScript = this;
        DNA2GameObject2.GetComponent<DNAScript>().mainCamera = mainCamera;

        AllDNA.Add(DNA2GameObject2);






        int DNA3RandomNumber = Random.Range(0, avaiblePositions1.Count);
        int random3 = avaiblePositions1[DNA3RandomNumber];
        avaiblePositions1.RemoveAt(DNA3RandomNumber);

        GameObject DNA3GameObject = Instantiate(DNA3, FindRightPosition(random3), Quaternion.identity);
        DNA3GameObject.GetComponent<DNAScript>().id = 3;
        DNA3GameObject.GetComponent<DNAScript>().matchGameScript = this;
        DNA3GameObject.GetComponent<DNAScript>().mainCamera = mainCamera;

        AllDNA.Add(DNA3GameObject);


        int DNA3RandomNumber2 = Random.Range(0, avaiblePositions2.Count);
        int random32 = avaiblePositions2[DNA3RandomNumber2];
        avaiblePositions2.RemoveAt(DNA3RandomNumber2);

        GameObject DNA3GameObject2 = Instantiate(DNA3, FindRightPosition(random32), Quaternion.identity);
        DNA3GameObject2.GetComponent<DNAScript>().id = 3;
        DNA3GameObject2.GetComponent<DNAScript>().matchGameScript = this;
        DNA3GameObject2.GetComponent<DNAScript>().mainCamera = mainCamera;

        AllDNA.Add(DNA3GameObject2);
    }

    public void DeleteAllDNA()
    {
        for (int i = 0; i < AllDNA.Count; i++)
        {
            GameObject CurrentDNA = AllDNA[i];
            Destroy(CurrentDNA);
        }

        AllDNA.Clear();
    }

    public void CheckWin()
    {
        if (Slot1 == Slot2)
        {
            DeleteAllDNA();
            bacteriaSpawnerScript.ShowBacteriaStats();
            gameObject.SetActive(false);
        } else {
            DeleteAllDNA();
            bacteriaSpawnerScript.UnPauseAll();
            gameObject.SetActive(false);
        }
    }

    Vector3 FindRightPosition(int number)
    {
        Vector3 choosenPos = new Vector3(0, 0, 0);
        switch(number)
        {
            case 1:
                choosenPos = new Vector3(-3, 3, -1);
                break;
            case 2:
                choosenPos = new Vector3(-3, 0, -1);
                break;
            case 3:
                choosenPos = new Vector3(-3, -3, -1);
                break;
            case 4:
                choosenPos = new Vector3(3, 3, -1);
                break;
            case 5:
                choosenPos = new Vector3(3, 0, -1);
                break;
            case 6:
                choosenPos = new Vector3(3, -3, -1);
                break;
            default:
                choosenPos = new Vector3(0, 0, 0);
                break;
                
        }

        return choosenPos;
    }
}
