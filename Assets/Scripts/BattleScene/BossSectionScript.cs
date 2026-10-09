using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossSectionScript : MonoBehaviour
{
    public GameObject player;
    public GameOperatorScript gameOperatorScript;
    public GameObject lavaBossPrefab;
    public GameObject barbedWire;
    public GameObject bossGame;
    public GameObject bossBar;
    public TextMeshProUGUI bossText;
    public BarScript bossHealthBar;
    public Vector3 bossSpawn;
    public ParticleSystem poisonParticles;
    public ParticleSystem poisonShootParticles;
    public GameObject poisonParticlesPos;
    public GameObject poisonShootPartilesPos;
    public int boss;
    public int health = 25;
    public bool bossSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.x > transform.position.x - 9 && !bossSpawned)
            {
                bossBar.SetActive(true);
                if (boss == 1)
                {
                    SpawnLavaBoss();
                    bossText.text = "Lava Boss";
                    bossHealthBar.SetMax(25);
                    bossHealthBar.SetHealth(25);
                    health = 25;
                }

                bossSpawned = true;
            }
        }
    }

    void SpawnLavaBoss()
    {
        bossSpawn += new Vector3(transform.position.x - 5, 0, 0);
        GameObject boss = Instantiate(lavaBossPrefab, bossSpawn, Quaternion.identity);
        boss.GetComponent<BossScript>().player = player;
        boss.GetComponent<BossScript>().poisonParticles = poisonParticles;
        boss.GetComponent<BossScript>().poisonShootParticles = poisonShootParticles;
        boss.GetComponent<BossScript>().poisonParticlesPos = poisonParticlesPos;
        boss.GetComponent<BossScript>().poisonShootPartilesPos = poisonShootPartilesPos;
        boss.GetComponent<BossScript>().bossSectionScript = this;
        boss.GetComponent<BossScript>().health = 10;
        bossGame = boss;
        SpawnWire();
    }

    public void SpawnWire()
    {
        Vector3 wireSpawn = transform.position + new Vector3(-13.4f, -0.79f, -0.1f);
        Instantiate(barbedWire, wireSpawn, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        bossHealthBar.SetHealth(health);
        if (health <= 0)
        {
            Destroy(bossGame);
            gameOperatorScript.won();
        }
    }
}
