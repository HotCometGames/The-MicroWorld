using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOperatorScript : MonoBehaviour
{
    public DataManager dataManager;
    public GameObject player;
    public List<GameObject> sections;
    public List<GameObject> endSections;
    public List<GameObject> bossSections;
    public List<Sprite> sprites;
    public GameObject particleSystemPos;
    public GameObject winScreen;
    public GameObject catchObject;
    public GameObject bossBar;
    public TextMeshProUGUI bossText;
    public BarScript bossHealthBar;
    public ParticleSystem particlesSystem;
    public ParticleSystem poisonParticles;
    public ParticleSystem poisonShootParticles;
    public GameObject poisonParticlesPos;
    public GameObject poisonShootPartilesPos;
    public BarScript ExpBarScript;
    public GameObject expBar;
    public Image bacteriaImageExp;
    public GameObject Coin;
    public TextMeshProUGUI goldText;
    public int numberOfSections;
    public int enemiesHealth = 1;
    public int enemiesDamage = 1;
    public float enemiesMoveSpeed = 5f;
    public float enemiesMaxVelocity = 7f;
    public float enemiesReloadSpeed = 1f;
    public float enemiesStartAggroDistance = 5f;
    public float enemisStopAggroDistance = 7f;
    public float enemiesDistanceToStop = 2f;
    public int enemiesExp = 2;
    public int expAddedEnd = 0;
    public int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadData();
        goldText.text = dataManager.gameData.coins.ToString();
        dataManager.gameData.temporaryBacteria = new List<BacteriaData>();
        numberOfSections = PlayerPrefs.GetInt("numberOfSections");
        enemiesHealth = PlayerPrefs.GetInt("enemiesHealth");
        enemiesDamage = PlayerPrefs.GetInt("enemiesDamage");
        enemiesMoveSpeed = PlayerPrefs.GetFloat("enemiesMoveSpeed");
        enemiesMaxVelocity = PlayerPrefs.GetFloat("enemiesMaxVelocity");
        enemiesReloadSpeed = PlayerPrefs.GetFloat("enemiesReloadSpeed");
        enemiesStartAggroDistance = PlayerPrefs.GetFloat("enemiesStartAggroDistance");
        enemisStopAggroDistance = PlayerPrefs.GetFloat("enemiesStopAggroDistance");
        enemiesDistanceToStop = PlayerPrefs.GetFloat("enemiesDistanceToStop"); 
        coins = dataManager.gameData.coins;


        LoadSections();
    }


    public void LoadSections()
    {
        for(int i = 0; i < numberOfSections; i++)
        {
            int randomInt = Random.Range(0, sections.Count);
            GameObject section = Instantiate(sections[randomInt], new Vector3((i*72) + 25, 0, 0), Quaternion.identity);
            section.GetComponent<EnvironmentSectionScript>().dataManager = dataManager;
            section.GetComponent<EnvironmentSectionScript>().player = player;
            section.GetComponent<EnvironmentSectionScript>().particleSystemPos = particleSystemPos;
            section.GetComponent<EnvironmentSectionScript>().particlesSystem = particlesSystem;
            section.GetComponent<EnvironmentSectionScript>().gameOperatorScript = this;
            section.GetComponent<EnvironmentSectionScript>().coin = Coin;
            section.GetComponent<EnvironmentSectionScript>().enemiesHealth = enemiesHealth;
            section.GetComponent<EnvironmentSectionScript>().enemiesDamage = enemiesDamage;
            section.GetComponent<EnvironmentSectionScript>().enemiesMoveSpeed = enemiesMoveSpeed;
            section.GetComponent<EnvironmentSectionScript>().enemiesMaxVelocity = enemiesMaxVelocity;
            section.GetComponent<EnvironmentSectionScript>().enemiesReloadSpeed = enemiesReloadSpeed;
            section.GetComponent<EnvironmentSectionScript>().enemiesStartAggroDistance = enemiesStartAggroDistance;
            section.GetComponent<EnvironmentSectionScript>().enemisStopAggroDistance = enemisStopAggroDistance;
            section.GetComponent<EnvironmentSectionScript>().enemiesDistanceToStop = enemiesDistanceToStop;
            section.GetComponent<EnvironmentSectionScript>().enemiesExp = enemiesExp;
        }
        if (PlayerPrefs.GetInt("bossFight") == 0)
        {
            int randomEnd = Random.Range(0, endSections.Count);
            GameObject end = Instantiate(endSections[randomEnd], new Vector3((numberOfSections * 72) + 1, 0, 0), Quaternion.identity);
            end.GetComponent<EndScript>().player = player;
            end.GetComponent<EndScript>().gameOperatorScript = this;
        }
        else if (PlayerPrefs.GetInt("bossFight") == 1)
        {
            int randomBoss = Random.Range(0, bossSections.Count);
            GameObject boss = Instantiate(bossSections[randomBoss], new Vector3((numberOfSections * 72) + 5, 0, 0), Quaternion.identity);
            boss.GetComponent<BossSectionScript>().player = player;
            boss.GetComponent<BossSectionScript>().gameOperatorScript = this;
            boss.GetComponent<BossSectionScript>().bossBar = bossBar;
            boss.GetComponent<BossSectionScript>().bossText = bossText;
            boss.GetComponent<BossSectionScript>().bossHealthBar = bossHealthBar;
            boss.GetComponent<BossSectionScript>().poisonParticles = poisonParticles;
            boss.GetComponent<BossSectionScript>().poisonShootParticles = poisonShootParticles;
            boss.GetComponent<BossSectionScript>().poisonParticlesPos = poisonParticlesPos;
            boss.GetComponent<BossSectionScript>().poisonShootPartilesPos = poisonShootPartilesPos;
            boss.GetComponent<BossSectionScript>().boss = 1;
        }
    }
    public void won()
    {
        winScreen.SetActive(true);
        if (PlayerPrefs.GetInt("bossFight") != 0)
        {
            catchObject.SetActive(false);
        }
        startExpAnimation();
        player.GetComponent<PlayerBattleScript>().won = true;
        dataManager.gameData.level++;
        dataManager.SaveData();
    }

    public void startExpAnimation()
    {
        expBar.SetActive(true);
        StartCoroutine(expAnimation());
    }


    public IEnumerator expAnimation()
    {
        int maxExp = 0;
        int subtractExp = 0;
        int playerExp = player.GetComponent<PlayerBattleScript>().exp;
        int id = player.GetComponent<PlayerBattleScript>().id;
        dataManager.gameData.Bacterias[id].exp = playerExp + expAddedEnd;
        dataManager.SaveData();
        if (playerExp < 10)
        {
            maxExp = 10;
            subtractExp = 0;
        } else if (playerExp < 30)
        {
            maxExp = 20;
            subtractExp = 10;
        } else if (playerExp < 60)
        {
            maxExp = 30;
            subtractExp = 30;
        } else if (playerExp < 100)
        {
            maxExp = 40;
            subtractExp = 60;
        } else if (playerExp < 150)
        {
            maxExp = 50;
            subtractExp = 100;
        } else if (playerExp < 210)
        {
            maxExp = 60;
            subtractExp = 150;
        } else if (playerExp < 280)
        {
            maxExp = 70;
            subtractExp = 210;
        } else if (playerExp < 360)
        {
            maxExp = 80;
            subtractExp = 280;
        } else if (playerExp < 450)
        {
            maxExp = 90;
            subtractExp = 360;
        } else if (playerExp < 550)
        {
            maxExp = 100;
            subtractExp = 450;
        }
        ExpBarScript.SetMax(maxExp);
        int startExp = playerExp - subtractExp;
        int currentExp = startExp;
        ExpBarScript.SetHealth(startExp);
        bacteriaImageExp.sprite = sprites[PlayerPrefs.GetInt("playerSprite")];

        yield return new WaitForSeconds(0.2f);

        for (int i = 1; i <= expAddedEnd; i++)
        {
            yield return new WaitForSeconds(0.2f);
            currentExp++;
            if (currentExp < maxExp)
            {
                ExpBarScript.SetHealth(currentExp);
            } else {
                int newExp = playerExp + i;
                if (newExp < 10)
                {
                    maxExp = 10;
                    subtractExp = 0;
                } else if (newExp < 30)
                {
                    maxExp = 20;
                    subtractExp = 10;
                } else if (newExp < 60)
                {
                    maxExp = 30;
                    subtractExp = 30;
                } else if (newExp < 100)
                {
                    maxExp = 40;
                    subtractExp = 60;
                } else if (newExp < 150)
                {
                    maxExp = 50;
                    subtractExp = 100;
                } else if (newExp < 210)
                {
                    maxExp = 60;
                    subtractExp = 150;
                } else if (newExp < 280)
                {
                    maxExp = 70;
                    subtractExp = 210;
                } else if (newExp < 360)
                {
                    maxExp = 80;
                    subtractExp = 280;
                } else if (newExp < 450)
                {
                    maxExp = 90;
                    subtractExp = 360;
                } else if (newExp < 550)
                {
                    maxExp = 100;
                    subtractExp = 450;
                }

                ExpBarScript.SetMax(maxExp);
                startExp = playerExp - subtractExp;
                currentExp = startExp;
                ExpBarScript.SetHealth(startExp);
            }
        }
    } 

    public void goBack()
    {
        SwitchScene("Lab");
    }

    public void goCatch()
    {
        if (dataManager.gameData.Bacterias.Count < 5)
        {
            dataManager.SaveData();
            SwitchScene("MicroScene");
        }
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetCoins(int amount)
    {
        coins = amount;
        dataManager.gameData.coins = coins;
        goldText.text = coins.ToString();
    }


}
