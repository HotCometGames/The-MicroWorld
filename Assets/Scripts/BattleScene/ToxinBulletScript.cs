using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToxinBulletScript : MonoBehaviour
{
    public int damage;
    public float bulletDistance = 5.5f;
    public Vector3 spawnPos;

    void Update()
    {
        float a = spawnPos.x - transform.position.x;
        float aSquared = a * a;
        float b = spawnPos.y - transform.position.y;
        float bSquared = b * b;

        float distance = Mathf.Sqrt(aSquared + bSquared);

        if (distance > bulletDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Poison"))
        {
            Destroy(gameObject);
        }
    }
}
