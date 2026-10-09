using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SludgeBombScript : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public Vector3 targetPos;
    public Vector3 spawnPos;
    public GameObject sludgeObject;
    public Renderer render;
    public float totalDistanceX;
    public float upAmount = 1;
    float yAlreadyAdded = 0;
    float yAlreadyRemoved = 0;
    float halfOfTotalDistance = 0;
    bool placedSludge = false;
    bool startTimer;
    float timer;
    public float timeToDelete = 2;

    void Start()
    {
        totalDistanceX = Mathf.Abs(targetPos.x-spawnPos.x);
        halfOfTotalDistance = totalDistanceX/2;
    }

    void Update()
    {
        float xTraveled = Mathf.Abs(transform.position.x - spawnPos.x);
        if (xTraveled < halfOfTotalDistance)
        {
            float yUp = (xTraveled/halfOfTotalDistance)*upAmount;
            transform.position += new Vector3(0, yUp-yAlreadyAdded, 0);
            yAlreadyAdded = yUp;
        } else if (xTraveled < totalDistanceX){
            float yDown = ((xTraveled-halfOfTotalDistance)/halfOfTotalDistance)*upAmount*-1;
            transform.position += new Vector3(0, yDown-yAlreadyRemoved, 0);
            yAlreadyRemoved = yDown;
        } else if (!placedSludge) {
            placeSludge();
            placedSludge = true;
        }

        if (startTimer)
        {
            timer+=Time.deltaTime;
        }
        if (timer >= timeToDelete)
        {
            Destroy(gameObject);
        }



    }

    void placeSludge()
    {
        sludgeObject.SetActive(true);
        rb.velocity = new Vector3(0, 0, 0);
        render.enabled = false;
        startTimer = true;
    }
}
