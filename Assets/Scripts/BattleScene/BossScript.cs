using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public ParticleSystem poisonParticles;
    public ParticleSystem poisonShootParticles;
    public GameObject poisonParticlesPos;
    public GameObject poisonBackground;
    public GameObject poisonShootPartilesPos;
    public BossSectionScript bossSectionScript;
    public int poisonDamage = 1;
    float attackReloadTimer = 4f;
    public float attackReloadDelay = 4f;
    public int attackBehaviour = 0;
    float behaviourTimer = 13;
    public float behaviourDelay = 13;
    public int health = 10; // fight stats
    public int exp = 2;
    public float distanceToStop = 2f; // path finding
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
    public float flashDuration = 0.125f; // effects
    public SpriteRenderer spriteRenderer; // effects
    public Material orginalMaterial; // effects
    public Material flashMaterial; // effects
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        poisonParticlesPos.transform.position = transform.position;
        poisonShootPartilesPos.transform.position = new Vector3(transform.position.x, transform.position.y, -3);

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

        //choose behaviour
        if (behaviourTimer <= 0)
        {
            int randomBehaviour = Random.Range(0, 2);
            attackBehaviour = randomBehaviour;
            behaviourTimer = behaviourDelay;
        } else {
            behaviourTimer -= Time.deltaTime;
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
        Vector2 direction = new Vector2(a, b);

        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float lookAtAngle = rotationAngle + correctionAngle + swimAngle;

        float currentAngle = transform.eulerAngles.z;            
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, lookAtAngle, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, newAngle);


        // attackBehaviour effect
        if (attackBehaviour == 0)
        {
            //close attack
            distanceToStop = 1.5f;
        } else if (attackBehaviour == 1)
        {
            //far snipe
            distanceToStop = 4.5f;
        }

        //movement
        if (distance > distanceToStop)
        {
            Vector3 inputDirection = new Vector3(a, b, 0).normalized;
            Vector3 targetVelocity = inputDirection * moveSpeed;

            // Accelerate or decelerate towards the target velocity
            velocity = Vector3.MoveTowards(velocity, targetVelocity, Time.deltaTime * (velocity.magnitude < targetVelocity.magnitude ? acceleration : deceleration));
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        } else {
            velocity = new Vector3(0, 0, 0);
        }

        // Apply velocity to the rigidbody
        rb.velocity = velocity;

        //attack
        if (attackReloadTimer <= 0)
        {
            if (attackBehaviour == 0)
            {
                PoisonMove();
            } else if (attackBehaviour == 1)
            {
                RotatePoisonShoot(a, b);
                ShootPoison();

            }
            attackReloadTimer = attackReloadDelay;
        } else {
            attackReloadTimer -= Time.deltaTime;
        }
    }
    public void PoisonMove()
    {
        poisonParticles.Clear();
        poisonParticles.Play();
        StartCoroutine(GrowPoisonBackground());
    }

    public void ShootPoison()
    {
        poisonShootParticles.Clear();
        poisonShootParticles.Play();
    }
    public void RotatePoisonShoot(float a, float b)
    {
        Vector2 direction = new Vector2(a, b);

        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float lookAtAngle = rotationAngle - 10;

        poisonShootPartilesPos.transform.eulerAngles = new Vector3(0, 0, lookAtAngle);
    }

    IEnumerator GrowPoisonBackground()
    {
        poisonBackground.transform.localScale = new Vector3(0, 0, 1);

        for (int i = 0; i < 20; i ++)
        {
            poisonBackground.transform.localScale += new Vector3(0.0185f, 0.0185f, 0);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < 20; i ++)
        {
            poisonBackground.transform.localScale -= new Vector3(0.0185f, 0.0185f, 0);
            yield return new WaitForSeconds(0.05f);
        }

        poisonBackground.transform.localScale = new Vector3(0, 0, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject bullet = collision.gameObject;
            health += -bullet.GetComponent<ToxinBulletScript>().damage;
            bossSectionScript.TakeDamage(bullet.GetComponent<ToxinBulletScript>().damage);
            StartCoroutine(FlashEffect());
        }
    }

    public IEnumerator FlashEffect()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = orginalMaterial;
    }
}
