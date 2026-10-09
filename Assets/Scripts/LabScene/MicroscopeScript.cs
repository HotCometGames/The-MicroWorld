using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MicroscopeScript : MonoBehaviour
{
    public Camera mainCamera;
    public DataManager dataManager;
    public HotBarScript hotBarScript;
    public bool on = true;
    public int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadData();
        currentLevel = dataManager.gameData.level;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsHovering() && on)
        {
            setLevel();
            getBacteria();
            SwitchScene("BattleScene");
        }
    }
    public void setLevel()
    {
        switch (currentLevel)
        {
            case 1:
                PlayerPrefs.SetInt("numberOfSections", 1);
                PlayerPrefs.SetInt("enemiesHealth", 1);
                PlayerPrefs.SetInt("enemiesDamage", 1);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 3f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 6f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1.3f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 3.7f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 2:
                PlayerPrefs.SetInt("numberOfSections", 2);
                PlayerPrefs.SetInt("enemiesHealth", 1);
                PlayerPrefs.SetInt("enemiesDamage", 1);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 3f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 6f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1.2f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 3.7f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 3:
                PlayerPrefs.SetInt("numberOfSections", 2);
                PlayerPrefs.SetInt("enemiesHealth", 2);
                PlayerPrefs.SetInt("enemiesDamage", 1);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 4f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 8f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1.1f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 3.5f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 4:
                PlayerPrefs.SetInt("numberOfSections", 3);
                PlayerPrefs.SetInt("enemiesHealth", 2);
                PlayerPrefs.SetInt("enemiesDamage", 1);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 4f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 8f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 3.5f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 5:
                PlayerPrefs.SetInt("numberOfSections", 3);
                PlayerPrefs.SetInt("enemiesHealth", 3);
                PlayerPrefs.SetInt("enemiesDamage", 1);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 4.5f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 9f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 3.2f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 6:
                PlayerPrefs.SetInt("numberOfSections", 3);
                PlayerPrefs.SetInt("enemiesHealth", 3);
                PlayerPrefs.SetInt("enemiesDamage", 1);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 4.5f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 9f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 2f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 7:
                PlayerPrefs.SetInt("numberOfSections", 3);
                PlayerPrefs.SetInt("enemiesHealth", 3);
                PlayerPrefs.SetInt("enemiesDamage", 2);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 4.5f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 9f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 2.8f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 8:
                PlayerPrefs.SetInt("numberOfSections", 3);
                PlayerPrefs.SetInt("enemiesHealth", 4);
                PlayerPrefs.SetInt("enemiesDamage", 2);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 5f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 10f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 0.9f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 2.6f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 9:
                PlayerPrefs.SetInt("numberOfSections", 3);
                PlayerPrefs.SetInt("enemiesHealth", 4);
                PlayerPrefs.SetInt("enemiesDamage", 2);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 4.5f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 12f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 0.9f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 6.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 12.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 2.5f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
            case 10:
                PlayerPrefs.SetInt("numberOfSections", 5);
                PlayerPrefs.SetInt("enemiesHealth", 3);
                PlayerPrefs.SetInt("enemiesDamage", 2);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 5f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 9.5f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1.1f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 2.5f);
                PlayerPrefs.SetInt("launchers", 0);
                PlayerPrefs.SetInt("bossFight", 1);
                break;
            case 11:
                PlayerPrefs.SetInt("numberOfSections", 3);
                PlayerPrefs.SetInt("enemiesHealth", 3);
                PlayerPrefs.SetInt("enemiesDamage", 2);
                PlayerPrefs.SetFloat("enemiesMoveSpeed", 5f);
                PlayerPrefs.SetFloat("enemiesMaxVelocity", 9.5f);
                PlayerPrefs.SetFloat("enemiesReloadSpeed", 1.1f);
                PlayerPrefs.SetFloat("enemiesStartAggroDistance", 7.5f);
                PlayerPrefs.SetFloat("enemiesStopAggroDistance", 9.5f);
                PlayerPrefs.SetFloat("enemiesDistanceToStop", 2.5f);
                PlayerPrefs.SetInt("launchers", 1);
                PlayerPrefs.SetInt("bossFight", 0);
                break;
        }
    }

    public void getBacteria()
    {
        SpaceScript selectedBacteria = hotBarScript.spaces[hotBarScript.currentSpace].GetComponent<SpaceScript>();
        PlayerPrefs.SetInt("playerHealth", selectedBacteria.bacteriaData.health + selectedBacteria.bacteriaData.addedHealth);
        PlayerPrefs.SetInt("playerDamage", selectedBacteria.bacteriaData.damage + selectedBacteria.bacteriaData.addedDamage);
        PlayerPrefs.SetFloat("playerSpeed", selectedBacteria.bacteriaData.agility + selectedBacteria.bacteriaData.addedAgility);
        PlayerPrefs.SetInt("playerExp", selectedBacteria.bacteriaData.exp);
        PlayerPrefs.SetInt("playerId", selectedBacteria.bacteriaData.id);
        PlayerPrefs.SetInt("playerSprite", selectedBacteria.bacteriaData.sprite);
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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


    //delete under here when done editing, because this UI is for easier use
    public void levelUp()
    {
        if (currentLevel < 11)
        {
            dataManager.LoadData();
            dataManager.gameData.level++;
            dataManager.SaveData();
            currentLevel = dataManager.gameData.level;
        }
    }

    public void levelDown()
    {
        if (currentLevel > 1)
        {
            dataManager.LoadData();
            dataManager.gameData.level--;
            dataManager.SaveData();
            currentLevel = dataManager.gameData.level;
        }
    }

    public void levelReset()
    {
        dataManager.LoadData();
        dataManager.gameData.level = 1;
        dataManager.SaveData();
        currentLevel = dataManager.gameData.level;
    }

    public void startRest()
    {
        dataManager.LoadData();
        dataManager.gameData.started = false;
        dataManager.SaveData();
    }
}
