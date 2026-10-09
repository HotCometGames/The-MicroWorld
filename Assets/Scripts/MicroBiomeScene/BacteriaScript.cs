using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BacteriaScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera mainCamera;
    public GameObject bacteriaMenu;
    public GameObject testButton;
    public TestButtonScript testButtonScript;
    public BacteriaMenuScript bacteriaMenuScript;
    public BacteriaSpawnerScript bacteriaSpawnerScript;
    public Sprite[] sprites; 
    private SpriteRenderer spriteRenderer;
    string[] actions = new string[2];
    string action = "swim";
    float actionTimer = 3f;
    int actionWaitTime = 3;
    int speed = 1;
    float recordRotation;
    float currentRotation;
    bool clockwise = true;
    public bool shouldMove = true;
    public bool allowPress = true;


    public BacteriaData bacteriaData;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        if (bacteriaData.sprite == 0)
        {
            bacteriaData.damage++;
        }else if (bacteriaData.sprite == 1)
        {
            bacteriaData.health++;
        } else if (bacteriaData.sprite == 2)
        {
            bacteriaData.agility+=2;
        } else if (bacteriaData.sprite == 3)
        {
            bacteriaData.agility+=2;
        } else if (bacteriaData.sprite == 4)
        {
            bacteriaData.health++;
        }
        //set sprite
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();

        Sprite sprite = sprites[bacteriaData.sprite];

        if (sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        else
        {
            Debug.LogError("Random sprite is null!");
        }

        //set actions
        actions[0] = "swim";
        actions[1] = "spin";
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldMove)
        {
            actionTimer += Time.deltaTime;

            if (actionTimer >= actionWaitTime)
            {
                int randomInt = Random.Range(0, 2);
                action = actions[randomInt];

                speed = Random.Range(1, 5);

                direction = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));

                actionTimer = 0f;
                actionWaitTime = Random.Range(2, 5);
                recordRotation = transform.rotation.z;
            }



            switch (action)
            {
                case "spin":
                    transform.Rotate(Vector3.forward, 70 * Time.deltaTime);
                    break;

                case "swim":
                    rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

                    if (clockwise)
                    {
                        transform.Rotate(Vector3.forward, 50 * Time.deltaTime);
                        currentRotation += 50 * Time.deltaTime;
                    } else {
                        transform.Rotate(Vector3.back, 50 * Time.deltaTime);
                        currentRotation -= 50 * Time.deltaTime;
                    }

                    if (currentRotation > recordRotation + 50)
                    {
                        clockwise = false;
                    } else if (currentRotation < recordRotation - 50)
                    {
                        clockwise = true;
                    }

                    break;

                default:
                    Debug.Log("Bacteria Movement Error");
                    break;
            }


            //walls

            if (IsHovering() && Input.GetMouseButtonDown(0) && allowPress)
            {
                testButton.SetActive(true);

                //set test button values
                testButtonScript.id = bacteriaData.id;
            }
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

    public void setBacteria()
    {
        bacteriaMenuScript.setText(bacteriaData.health, bacteriaData.damage, bacteriaData.agility);
    }

    public void Pause()
    {
        shouldMove = false;
        allowPress = false;
        direction = new Vector2(0, 0);
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    void OnCollisionEnter2D()
    {
        direction = new Vector2(-direction.x, -direction.y);
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
