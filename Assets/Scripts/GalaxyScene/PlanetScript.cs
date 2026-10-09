using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    public GameObject HomePlanet;
    public GameObject PlanetMenu;
    public PlanetMenuScript planetMenuScript;
    public Camera mainCamera;
    public float rotationSpeed = 0.1f;
    public bool isLight = true;
    public bool oxygen = true;
    public int avgTemp = 20;
    public float pHLevel = 8f;
    public int healthMax = 3;
    public int healthMin = 1;
    public int damageMax = 3;
    public int damageMin = 1;
    public int agilityMax = 3;
    public int agilityMin = 3;
    // Start is called before the first frame update
    void Start()
    {
        setMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHovering() && Input.GetMouseButtonDown(0))
        {
            PlanetMenu.SetActive(!PlanetMenu.activeSelf);
            if (PlanetMenu.activeSelf)
            {
                PlayerPrefs.SetInt("isLight", isLight ? 1 : 0);
                PlayerPrefs.SetInt("oxygen", oxygen ? 1 : 0);
                PlayerPrefs.SetInt("avgTemp", avgTemp);
                PlayerPrefs.SetFloat("pHLevel", pHLevel);
                PlayerPrefs.SetInt("healthMax", healthMax);
                PlayerPrefs.SetInt("healthMin", healthMin);
                PlayerPrefs.SetInt("damageMax", damageMax);
                PlayerPrefs.SetInt("damageMin", damageMin);
                PlayerPrefs.SetInt("agilityMax", agilityMax);
                PlayerPrefs.SetInt("agilityMin", agilityMin);
                PlayerPrefs.Save();
            }
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed);

        if (IsHovering() && HomePlanet.transform.localScale.x < 0.6)
        {
            HomePlanet.transform.localScale = new Vector3(HomePlanet.transform.localScale.x + 0.02f, HomePlanet.transform.localScale.y + 0.02f, HomePlanet.transform.localScale.z);
        }
        if (!IsHovering() && HomePlanet.transform.localScale.x > 0.5)
        {
            HomePlanet.transform.localScale = new Vector3(HomePlanet.transform.localScale.x - 0.02f, HomePlanet.transform.localScale.y - 0.02f, HomePlanet.transform.localScale.z);
        }
    }


    bool IsHovering()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen coordinates to world coordinates
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -mainCamera.transform.position.z));

        // Perform a 2D physics raycast at the converted world position
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        // Check if the raycast hits this GameObject's Collider
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    public void setMenu()
    {
        string lightText = "";
        string oxygenText = "";

        if (isLight)
        {
            lightText = "Light reaches surface";
        } else {
            lightText = "Light dosen't reach surface";
        }

        if (oxygen)
        {
            oxygenText = "Oxygen Present";
        } else {
            oxygenText = "Oxygen not Present";
        }

        string tempText = "Average temp: " + avgTemp + "C";
        string pHText = "Ocean pH level: " + pHLevel;
        
        planetMenuScript.elements = new string[]{lightText, oxygenText, tempText, pHText};
        planetMenuScript.setText();
    }
}