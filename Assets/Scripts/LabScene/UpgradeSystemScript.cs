using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeSystemScript : MonoBehaviour
{
    public GameObject upgradeSystemUI;
    public Camera mainCamera;
    public DataManager dataManager;
    public Sprite[] sprites;
    public GameObject bacteriaImage;
    public TextMeshProUGUI baseStatsText;
    public TextMeshProUGUI addedStatsText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI adderText;
    public HotBarScript hotBarScript;
    public BarScript expScript;
    public bool on = true;
    public int health;
    public int damage;
    public float agility;
    public int addedHealth;
    public int addedDamage;
    public int addedAgility;
    public int availablePoints;
    private BacteriaData selectedBacteria;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsHovering() && on)
        {
            setUp();
            refreshUpUpgradeUI();
        }
    }

    void setUp()
    {
        upgradeSystemUI.SetActive(true);
        selectedBacteria = hotBarScript.spaces[hotBarScript.currentSpace].GetComponent<SpaceScript>().bacteriaData;
        bacteriaImage.GetComponent<Image>().sprite = sprites[selectedBacteria.sprite];
    }

    void refreshUpUpgradeUI()
    {
        int level = 0;
        int maxExp = 0;
        int subtractExp = 0;
        if (selectedBacteria.exp < 10)
        {
            level = 1;
            maxExp = 10;
            subtractExp = 0;
        } else if (selectedBacteria.exp < 30)
        {
            level = 2;
            maxExp = 20;
            subtractExp = 10;
        } else if (selectedBacteria.exp < 60)
        {
            level = 3;
            maxExp = 30;
            subtractExp = 30;
        } else if (selectedBacteria.exp < 100)
        {
            level = 4;
            maxExp = 40;
            subtractExp = 60;
        } else if (selectedBacteria.exp < 150)
        {
            level = 5;
            maxExp = 50;
            subtractExp = 100;
        } else if (selectedBacteria.exp < 210)
        {
            level = 6;
            maxExp = 60;
            subtractExp = 150;
        } else if (selectedBacteria.exp < 280)
        {
            level = 7;
            maxExp = 70;
            subtractExp = 210;
        } else if (selectedBacteria.exp < 360)
        {
            level = 8;
            maxExp = 80;
            subtractExp = 280;
        } else if (selectedBacteria.exp < 450)
        {
            level = 9;
            maxExp = 90;
            subtractExp = 360;
        } else if (selectedBacteria.exp < 550)
        {
            level = 10;
            maxExp = 100;
            subtractExp = 450;
        }
        expScript.SetMax(maxExp);
        int startExp = selectedBacteria.exp - subtractExp;
        expScript.SetHealth(startExp);

        health = selectedBacteria.health;
        damage = selectedBacteria.damage;
        agility = selectedBacteria.agility;
        addedHealth = selectedBacteria.addedHealth;
        addedDamage = selectedBacteria.addedDamage;
        addedAgility = selectedBacteria.addedAgility;


        levelText.text = "Level " + level;

        baseStatsText.text = "Base Health: " + health + "\nBase Damage: " + damage + "\nBase Agilty: " + agility;

        int usedPoints = addedHealth + addedDamage + (addedAgility / 2);

        addedStatsText.text = "Added Health: " + addedHealth + "\nAdded Damage: " + addedDamage + "\nAdded Agility: " + addedAgility + "\nUsed Points: " + usedPoints;

        availablePoints = (level - usedPoints) - 1;

        adderText.text = "Health\nDamage\nAgility\nAvailable Points: " + availablePoints;
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

    public void Add(string stat)
    {
        if (stat == "health")
        {
            if (availablePoints > 0)
            {
                addedHealth++;
                selectedBacteria.addedHealth = addedHealth;
                saveBacteria();
                refreshUpUpgradeUI();
            }
        } else if (stat == "damage")
        {
            if (availablePoints > 0)
            {
                addedDamage++;
                selectedBacteria.addedDamage = addedDamage;
                saveBacteria();
                refreshUpUpgradeUI();
            }
        } else if (stat == "agility")
        {
            if (availablePoints > 0)
            {
                addedAgility += 2;
                selectedBacteria.addedAgility = addedAgility;
                saveBacteria();
                refreshUpUpgradeUI();
            }
        }
    }

    public void Subtract(string stat)
    {
        if (stat == "health")
        {
            if (addedHealth > 0)
            {
                addedHealth--;
                selectedBacteria.addedHealth = addedHealth;
                saveBacteria();
                refreshUpUpgradeUI();
            }
        } else if (stat == "damage")
        {
            if (addedDamage > 0)
            {
                addedDamage--;
                selectedBacteria.addedDamage = addedDamage;
                saveBacteria();
                refreshUpUpgradeUI();
            }
        } else if (stat == "agility")
        {
            if (addedAgility > 0)
            {
                addedAgility -= 2;
                selectedBacteria.addedAgility = addedAgility;
                saveBacteria();
                refreshUpUpgradeUI();
            }
        }
    }

    public void saveBacteria()
    {
        dataManager.gameData.Bacterias[selectedBacteria.id] = selectedBacteria;
        dataManager.SaveData();
        hotBarScript.spaces[selectedBacteria.id].GetComponent<SpaceScript>().bacteriaData = selectedBacteria;
    }

    public void exit()
    {
        upgradeSystemUI.SetActive(false);
    }

}
