using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceScript : MonoBehaviour
{
    public RectTransform thisTransform;
    public BacteriaData bacteriaData;
    public HotBarScript hotBarScript;
    public Sprite[] sprites;
    public GameObject bacteria;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp()
    {
        bacteria.SetActive(true);
        bacteria.GetComponent<Image>().sprite = sprites[bacteriaData.sprite];
    }
    public void Selected()
    {
        hotBarScript.currentSpace = id;
        hotBarScript.SetSpaces();
        StartCoroutine(GoUp());
    }

    IEnumerator GoUp()
    {
        thisTransform.anchoredPosition = new Vector2(thisTransform.anchoredPosition.x, -180);
        for (int i = 0; i < 10; i++)
        {
            thisTransform.anchoredPosition += new Vector2(0, 1);
            yield return null;
        }
    }
}
