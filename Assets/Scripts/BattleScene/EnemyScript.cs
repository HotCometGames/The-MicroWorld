using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public DataManager dataManager;
    public GameObject player;
    public Rigidbody2D rb;
    public GameObject toxinBulletPrefab;
    public GameObject sludgeBombPrefab;
    public Vector3 spawnPosition; // path finding
    public GameObject particleSystemPos; // effects
    public ParticleSystem particlesSystem; // effects
    public GameOperatorScript gameOperatorScript;
    public GameObject coin;
    public float startAggroDistance = 5f; // path finding
    public float stopAggroDistance = 7f; // path finding
    public float distanceToStop = 2f; // path finding
    public bool aggro; // aggro
    public BacteriaData enemyData; // fight stats
    public int health = 2; // fight stats
    public int damage = 1; // fight stats
    public int exp = 2;
    public float damageTikCooldown = 0f;
    public float damageTikReload = 0.5f;
    public float reloadSpeed = 1f; // fight stats - bullet
    public float moveSpeed = 5f; // Initial move speed - movement
    public float acceleration = 10f; // Acceleration rate - movement
    public float deceleration = 20f; // Deceleration rate - movement
    public float maxVelocity = 7f; // Maximum velocity - movement
    private Vector3 velocity; // movement
    public float rotationSpeed = 360f; // rotation
    public float correctionAngle = -45f; // rotation
    public float swimSpeed = 120f; // swim movement
    private float swimAngle = 0; // swim movement
    private bool swimGettingBigger = true; // swim movement
    public float correctionBulletAngle = -90f; // bullet
    public float reloadTimer = 1f; // bullet
    public float bulletBuffer = 1f; // bullet
    public int bulletType = 0; // bullet
    public GameObject warningPrefab; // bullet
    public GameObject currentWarning;
    Vector3 savePlayerPos; // bullet
    bool savedPlayerPos = false;
    public float bulletDistance = 5.5f; // bullet
    public float bulletType1UpAmount = 1;
    public float flashDuration = 0.125f; // effects
    public SpriteRenderer spriteRenderer; // effects
    public Material orginalMaterial; // effects
    public Material flashMaterial; // effects
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;  
        enemyData.health = health;
        enemyData.damage = damage;
        enemyData.agility = moveSpeed;
        enemyData.exp = 0;
        enemyData.addedHealth = 0;
        enemyData.addedDamage = 0;
        enemyData.addedAgility = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //health managment
        if (damageTikCooldown > 0)
            {
                damageTikCooldown -= Time.deltaTime;
            } 

        if (health <= 0)
        {
            dataManager.gameData.temporaryBacteria.Add(enemyData);
            gameOperatorScript.expAddedEnd += exp;
            particleEffect();
            spawnCoin();
            if (currentWarning != null)
            {
                Destroy(currentWarning);
            }
            Destroy(gameObject);
        }


        //getting distance
        float a = 0;
        float aSquared;
        float b = 0;
        float bSquared;
        float cSquared;
        float distance = 0;
        if (player != null)
        {
            a = player.transform.position.x - transform.position.x;
            aSquared = a * a;
            b = player.transform.position.y - transform.position.y;
            bSquared = b * b;
            cSquared = aSquared + bSquared;
            distance = Mathf.Sqrt(cSquared);
        }

        //finding out to be agro or not
        if (distance <= startAggroDistance)
        {
            aggro = true;
        }

        if (aggro && distance > stopAggroDistance)
        {
            aggro = false;
        }


        //swim movement
        if (swimAngle > 30)
        {
            swimGettingBigger = false;
        }
        if (swimAngle < -30)
        {
            swimGettingBigger = true;
        }

        if (swimGettingBigger)
        {
            swimAngle += swimSpeed * Time.deltaTime;
        } else {
            swimAngle += -swimSpeed * Time.deltaTime;
        }


        //rotate correctly
        if (rb.velocity.magnitude > 0.1)
        {
            Vector2 direction = new Vector2(a, b);

            float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float lookAtAngle = rotationAngle + correctionAngle + swimAngle;

            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, lookAtAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, newAngle);
        }

        //movement
        if (aggro && distance > distanceToStop)
        {
            Vector3 inputDirection = new Vector3(a, b, 0).normalized;
            Vector3 targetVelocity = inputDirection * moveSpeed;

            // Accelerate or decelerate towards the target velocity
            velocity = Vector3.MoveTowards(velocity, targetVelocity, Time.deltaTime * (velocity.magnitude < targetVelocity.magnitude ? acceleration : deceleration));
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        } else if (aggro && distance > (distanceToStop * 0.66)) {
            velocity = new Vector3(0, 0, 0);
        }else if (aggro){
            Vector3 inputDirection = new Vector3(spawnPosition.x - transform.position.x, spawnPosition.y - transform.position.y, 0).normalized * -1;
            Vector3 targetVelocity = inputDirection * moveSpeed;

            // Accelerate or decelerate towards the target velocity
            velocity = Vector3.MoveTowards(velocity, targetVelocity, Time.deltaTime * (velocity.magnitude < targetVelocity.magnitude ? acceleration : deceleration));
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        } else {
            Vector3 inputDirection = new Vector3(spawnPosition.x - transform.position.x, spawnPosition.y - transform.position.y, 0).normalized;
            Vector3 targetVelocity = inputDirection * moveSpeed;

            // Accelerate or decelerate towards the target velocity
            velocity = Vector3.MoveTowards(velocity, targetVelocity, Time.deltaTime * (velocity.magnitude < targetVelocity.magnitude ? acceleration : deceleration));
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        }

        // Apply velocity to the rigidbody
        rb.velocity = velocity;


        //shooting
        if (aggro && reloadTimer <= 0 && player != null)
        {
            Shoot(savePlayerPos);
            reloadTimer = reloadSpeed;
            savedPlayerPos = false;
            Destroy(currentWarning);
            currentWarning = null;
        } else if (aggro && player != null)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0.5f && !savedPlayerPos)
            {
                savePlayerPos = player.transform.position;
                savedPlayerPos = true;
                if (bulletType == 1)
                {
                    currentWarning = Instantiate(warningPrefab, savePlayerPos, Quaternion.identity);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject bullet = collision.gameObject;
            health += -bullet.GetComponent<ToxinBulletScript>().damage;
            StartCoroutine(FlashEffect());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (damageTikCooldown <= 0)
        {
            if (other.gameObject.CompareTag("PoisonBoss"))
            {
                health += -other.gameObject.transform.parent.GetComponent<BossScript>().poisonDamage;
                StartCoroutine(FlashEffect());
                damageTikCooldown = damageTikReload;
            } else if (other.gameObject.CompareTag("PoisonEnemy"))
            {
                health += -other.gameObject.transform.parent.GetComponent<SludgeBombScript>().damage;
                StartCoroutine(FlashEffect());
                damageTikCooldown = damageTikReload;
            }
        }
    }

    void Shoot(Vector3 targetPosition)
    {
        // Calculate direction towards the cursor
        Vector2 shootDirection = (targetPosition - transform.position).normalized;

        Vector3 bulletSpawn = new Vector3(transform.position.x + (shootDirection.x * bulletBuffer), transform.position.y + (shootDirection.y * bulletBuffer), -1);

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        float fixedAngle = angle + correctionBulletAngle;

        switch(bulletType)
        {
            case 0:
                // Instantiate bullet at the position of the game object
                GameObject bullet = Instantiate(toxinBulletPrefab, bulletSpawn, Quaternion.identity);
                bullet.transform.rotation = Quaternion.AngleAxis(fixedAngle, Vector3.forward);

                // Apply force to the bullet in the direction of the cursor
                bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f; // Adjust speed as needed
                bullet.GetComponent<ToxinBulletScript>().damage = damage;
                bullet.GetComponent<ToxinBulletScript>().spawnPos = bulletSpawn;
                bullet.GetComponent<ToxinBulletScript>().bulletDistance = bulletDistance;
                break;
            case 1:
                GameObject sludgeBomb = Instantiate(sludgeBombPrefab, bulletSpawn, Quaternion.identity);
                sludgeBomb.transform.rotation = Quaternion.AngleAxis(fixedAngle, Vector3.forward);
                sludgeBomb.GetComponent<Rigidbody2D>().velocity = shootDirection * 10f; // Adjust speed as needed
                sludgeBomb.GetComponent<SludgeBombScript>().damage = damage;
                sludgeBomb.GetComponent<SludgeBombScript>().spawnPos = bulletSpawn;
                sludgeBomb.GetComponent<SludgeBombScript>().targetPos = targetPosition;
                sludgeBomb.GetComponent<SludgeBombScript>().upAmount = bulletType1UpAmount;
                sludgeBomb.GetComponent<SludgeBombScript>().timeToDelete = 2f;

                break;

            default:
                break;
        }
    }

    void particleEffect()
    {
        particleSystemPos.transform.position = transform.position;
        particlesSystem.Clear();
        particlesSystem.Play();
    }

    public IEnumerator FlashEffect()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = orginalMaterial;
    }

    void spawnCoin()
    {
        GameObject coinSpawn = Instantiate(coin, transform.position, transform.rotation);
        coinSpawn.transform.position = transform.position;
    }
}
