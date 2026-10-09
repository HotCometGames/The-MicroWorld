using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class PlayerBattleScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject toxinBulletPrefab;
    public GameObject sludgeBombPrefab;
    public GameObject deathMessage;
    public Camera MainCamera;
    public GameObject ParticleSystem;
    public GameObject particleSystemPos; // effects
    public ParticleSystem particlesSystem; // effects
    public BarScript HealthBarScript;
    public GameOperatorScript gameOperatorScript;
    public Volume volume;
    public Vignette vignette;
    public List<Sprite> sprites;
    public GameObject slash; // slash
    public GameObject abilities;
    public GameObject aim;
    public Image UIFill;
    public int maxHealth = 3;
    public int health;
    public int damage = 1;
    public int exp = 0;
    public int id = 0;
    public float cameraSpeed = 1.5f; // camera
    public float cameraBuffer = 5.5f; // camera
    public float moveSpeed = 5f; // Initial move speed - movement
    public float acceleration = 10f; // Acceleration rate - movement
    public float deceleration = 20f; // Deceleration rate - movement
    public float maxVelocity = 10f; // Maximum velocity - movement
    public float damageTikCooldown = 0f;
    public float damageTikReload = 0.5f;
    public bool won = false;
    public float rotationSpeed = 180f; // rotation
    public float correctionAngle = -45f; // rotation
    public float swimSpeed = 10f; // swim movement
    private float swimAngle = 0; // swim movement
    private bool swimGettingBigger = true; // swim movement
    public float correctionBulletAngle = -90f; // bullet
    public float bulletBuffer = 0.7f; // bullet
    public float reloadSpeed = 0.5f; // bullet
    public float reloadTimer = 1f; // bullet
    public int bulletType = 0; // bullet
    public float bulletDistance = 5.5f; // bullet
    public float bulletType1UpAmount = 1;
    public float slashReloadSpeed = 0.5f; // slash
    public float slashReloadTimer = 0.2f; // slash
    public float slashDuration = 0.2f; // slash
    public float slashBuffer = 0.3f; // slash
    public float flashDuration = 0.125f; // effects
    public SpriteRenderer spriteRenderer; // effects
    public Material orginalMaterial; // effects
    public Material flashMaterial; // effects
    public float maxLowHealthIntensity = 0.55f; // effects
    public float currentLowHealthIntensity = 0.4f; // effects
    public float lowHealthFlashSpeed = 2f; // effects
    private bool LowHealthIntensityGettingBigger = false; // effects
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = PlayerPrefs.GetInt("playerHealth");
        health = maxHealth;

        HealthBarScript.SetMax(maxHealth);
        HealthBarScript.SetHealth(health);

        damage = PlayerPrefs.GetInt("playerDamage");

        moveSpeed = PlayerPrefs.GetFloat("playerSpeed");
        maxVelocity = moveSpeed * 2;

        exp = PlayerPrefs.GetInt("playerExp");
        id = PlayerPrefs.GetInt("playerId");

        spriteRenderer.sprite = sprites[PlayerPrefs.GetInt("playerSprite")];
        if (PlayerPrefs.GetInt("playerSprite") == 10)
        {
            bulletType = 1;
        }

        volume.profile.TryGet(out vignette);

        if (bulletType == 1)
        {
            aim.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        abilities.transform.position = transform.position;

        if (!won)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = -1f;
            float cursorASquared = (transform.position.x - cursorPosition.x) * (transform.position.x - cursorPosition.x);
            float cursorBSquared = (transform.position.y - cursorPosition.y) * (transform.position.y - cursorPosition.y);
            float cursorC = Mathf.Sqrt(cursorASquared + cursorBSquared);
            //Death conditions
            if (damageTikCooldown > 0)
            {
                damageTikCooldown -= Time.deltaTime;
            } 

            if (health <= 0)
            {
                particleEffect();
                deathMessage.SetActive(true);
                gameOperatorScript.startExpAnimation();
                
                Destroy(gameObject);
            }


            //slash
            if (slashReloadTimer <= 0)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Slash(cursorPosition);
                    slashReloadTimer = slashReloadSpeed;
                }
            } else {
                slashReloadTimer -= Time.deltaTime;
                UIFill.fillAmount = Mathf.InverseLerp(0, slashReloadSpeed, slashReloadTimer);
            }

            //Movement ChatGPT
            // Calculate desired velocity based on input
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            
            Vector3 inputDirection = new Vector3(moveHorizontal, moveVertical, 0).normalized;
            Vector3 targetVelocity = inputDirection * moveSpeed;

            // Accelerate or decelerate towards the target velocity
            velocity = Vector3.MoveTowards(velocity, targetVelocity, Time.deltaTime * (velocity.magnitude < targetVelocity.magnitude ? acceleration : deceleration));
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

            // Apply velocity to the rigidbody
            rb.velocity = velocity;



            //Move camera
            float cameraXPos = MainCamera.transform.position.x;
            float currentXPos = transform.position.x;

            if (cameraXPos > currentXPos + cameraBuffer && cameraXPos > 0)
            {
                MainCamera.transform.position += new Vector3((currentXPos + cameraBuffer) - cameraXPos, 0, 0) * cameraSpeed * Time.deltaTime;
                ParticleSystem.transform.position = new Vector3(MainCamera.transform.position.x, ParticleSystem.transform.position.y, ParticleSystem.transform.position.z);
            }

            if (cameraXPos < currentXPos - cameraBuffer)
            {
                MainCamera.transform.position += new Vector3((currentXPos - cameraBuffer) - cameraXPos, 0, 0) * cameraSpeed * Time.deltaTime;
                ParticleSystem.transform.position = new Vector3(MainCamera.transform.position.x, ParticleSystem.transform.position.y, ParticleSystem.transform.position.z);
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



            //rotate
            if (rb.velocity.magnitude > 0.1)
            {
                Vector2 direction = new Vector2(rb.velocity.x, rb.velocity.y);

                float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                float lookAtAngle = rotationAngle + correctionAngle + swimAngle;

                float currentAngle = transform.eulerAngles.z;
                float newAngle = Mathf.MoveTowardsAngle(currentAngle, lookAtAngle, rotationSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0, newAngle);
            }


            //shoot
            if (reloadTimer <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (bulletType == 1 && cursorC > bulletDistance)
                    {
                        Vector3 shootDirection = (cursorPosition - transform.position).normalized;
                        shootDirection *= bulletDistance;
                        shootDirection.z = 0;
                        Vector3 shootTarget = transform.position + shootDirection;
                        Shoot(shootTarget);
                    } else{
                        Shoot(cursorPosition);
                    }
                    reloadTimer = reloadSpeed;
                }
            } else {
                reloadTimer -= Time.deltaTime;
            }

            if (bulletType == 1)
            {
                if (cursorC > bulletDistance)
                {
                    Vector3 shootDirection = (cursorPosition - transform.position).normalized;
                    shootDirection *= bulletDistance;
                    shootDirection.z = 0;
                    Vector3 shootTarget = transform.position + shootDirection;
                    aim.transform.position = shootTarget;
                } else{
                    aim.transform.position = cursorPosition;
                }
            }
            

            //low health effect
            if (maxHealth > 1 && health == 1)
            {
                if (currentLowHealthIntensity > maxLowHealthIntensity)
                {
                    LowHealthIntensityGettingBigger = false;
                }
                if (currentLowHealthIntensity <= 0)
                {
                    LowHealthIntensityGettingBigger = true;
                }

                if (LowHealthIntensityGettingBigger)
                {
                    currentLowHealthIntensity += lowHealthFlashSpeed * Time.deltaTime;
                } else {
                    currentLowHealthIntensity += -lowHealthFlashSpeed * Time.deltaTime;
                }
            } else {
                currentLowHealthIntensity = 0;
            }

            updateLowHealthEffect();
        } else {
            currentLowHealthIntensity = 0;
            updateLowHealthEffect();

            // Accelerate or decelerate towards the target velocity
            velocity = Vector3.MoveTowards(velocity, new Vector3(0, 0, 0), Time.deltaTime * deceleration);
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

            // Apply velocity to the rigidbody
            rb.velocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageTikCooldown <= 0)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                GameObject bullet = collision.gameObject;
                health += -bullet.GetComponent<ToxinBulletScript>().damage;
                HealthBarScript.SetHealth(health);
                StartCoroutine(FlashEffect());
                damageTikCooldown = damageTikReload;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            gameOperatorScript.SetCoins(gameOperatorScript.coins + 1);
            Destroy(other.gameObject);
        }

        if (damageTikCooldown <= 0)
        {
            if (other.gameObject.CompareTag("PoisonBoss"))
            {
                health += -other.gameObject.transform.parent.GetComponent<BossScript>().poisonDamage;
                HealthBarScript.SetHealth(health);
                StartCoroutine(FlashEffect());
                damageTikCooldown = damageTikReload;
            } else if (other.gameObject.CompareTag("PoisonEnemy"))
            {
                health += -other.gameObject.transform.parent.GetComponent<SludgeBombScript>().damage;
                HealthBarScript.SetHealth(health);
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

    void Slash(Vector3 targetPosition)
    {
        Vector2 shootDirection = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Vector3 slashSpawn = new Vector3(transform.position.x + (shootDirection.x * slashBuffer), transform.position.y + (shootDirection.y * slashBuffer), -1);
        slash.SetActive(true);
        slash.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        slash.transform.position = slashSpawn;
        StartCoroutine(SlashOff());

    }

    IEnumerator SlashOff()
    {
        yield return new WaitForSeconds(slashDuration);
        slash.SetActive(false);
    }

    void updateLowHealthEffect()
    {
        vignette.intensity.value = currentLowHealthIntensity;
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
}
