using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenuScript : MonoBehaviour
{
    public Text atmosphere;
    public string[] elements;
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText()
    {
        text = "";
        for (int i = 0; i < elements.Length; i++)
        {
            text = text + "\n" + elements[i];
        }


        atmosphere.text = text;
    }
}
