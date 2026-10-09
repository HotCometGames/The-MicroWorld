using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironmentSectionScript : MonoBehaviour
{
    public DataManager dataManager;
    public GameObject player;
    public List<GameObject> possibleEnemies;
    public List<Vector3> enemySpawns;
    public List<Sprite> enemySprites;
    public GameObject particleSystemPos;
    public ParticleSystem particlesSystem;
    public GameOperatorScript gameOperatorScript;
    public GameObject coin;
    public int enemiesHealth = 1;
    public int enemiesDamage = 1;
    public float enemiesMoveSpeed = 5f;
    public float enemiesMaxVelocity = 7f;
    public float enemiesReloadSpeed = 1f;
    public float enemiesStartAggroDistance = 5f;
    public float enemisStopAggroDistance = 7f;
    public float enemiesDistanceToStop = 2f;
    public int enemiesExp;
    private bool enemiesSpawned = false;


    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.x > transform.position.x - 32 && !enemiesSpawned)
            {
                SpawnEnemies();
            }
        }
    }

    public void SpawnEnemies()
    {
        for(int i = 0; i < enemySpawns.Count; i++)
        {
            int isALauncher = 0;
            if (PlayerPrefs.GetInt("launchers") == 1)
            {
                isALauncher = Random.Range(0, 2);
            }
            int randomInt = Random.Range(0, possibleEnemies.Count);
            int randomSprite = Random.Range(0, 5);
            Vector3 enemySpawnPos = enemySpawns[i];
            enemySpawnPos += new Vector3(transform.position.x - 25, 0, 0);
            GameObject enemy = Instantiate(possibleEnemies[randomInt], enemySpawnPos, Quaternion.identity);
            enemy.GetComponent<EnemyScript>().dataManager = dataManager;
            enemy.GetComponent<EnemyScript>().player = player;
            enemy.GetComponent<EnemyScript>().particleSystemPos = particleSystemPos;
            enemy.GetComponent<EnemyScript>().particlesSystem = particlesSystem;
            enemy.GetComponent<EnemyScript>().gameOperatorScript = gameOperatorScript;
            enemy.GetComponent<EnemyScript>().coin = coin;
            enemy.GetComponent<EnemyScript>().health = enemiesHealth;
            enemy.GetComponent<EnemyScript>().damage = enemiesDamage;
            enemy.GetComponent<EnemyScript>().exp = enemiesExp;
            enemy.GetComponent<EnemyScript>().moveSpeed = enemiesMoveSpeed;
            enemy.GetComponent<EnemyScript>().maxVelocity = enemiesMaxVelocity;

            if (isALauncher == 0)
            {
                enemy.GetComponent<EnemyScript>().reloadSpeed = enemiesReloadSpeed;
                enemy.GetComponent<EnemyScript>().startAggroDistance = enemiesStartAggroDistance;
                enemy.GetComponent<EnemyScript>().stopAggroDistance = enemisStopAggroDistance;
                enemy.GetComponent<EnemyScript>().distanceToStop = enemiesDistanceToStop;
                enemy.GetComponent<EnemyScript>().bulletType = 0;
                enemy.GetComponent<EnemyScript>().enemyData.sprite = randomSprite;
                enemy.GetComponent<SpriteRenderer>().sprite = enemySprites[randomSprite];
            } else if (isALauncher == 1){
                enemy.GetComponent<EnemyScript>().reloadSpeed = enemiesReloadSpeed * 2;
                enemy.GetComponent<EnemyScript>().startAggroDistance = enemiesStartAggroDistance * 1.3f;
                enemy.GetComponent<EnemyScript>().stopAggroDistance = enemisStopAggroDistance * 1.3f;
                enemy.GetComponent<EnemyScript>().distanceToStop = (enemiesStartAggroDistance * 1.3f) - 2;
                enemy.GetComponent<EnemyScript>().bulletType = 1;
                enemy.GetComponent<EnemyScript>().enemyData.sprite = 10;
                enemy.GetComponent<SpriteRenderer>().sprite = enemySprites[10];
            }
        }
        enemiesSpawned = true;
    }



}
