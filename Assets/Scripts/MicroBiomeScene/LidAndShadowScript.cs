using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidAndShadowScript : MonoBehaviour
{
    public GameObject lid;
    public GameObject missedCapture;
    public GameObject ShowCase;
    public BacteriaSpawnerScript bacteriaSpawnerScript;
    bool collided;
    bool goneDown = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!goneDown)
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Convert the screen position to a world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f)); // 10f is the distance from the camera

            // Set the GameObject's position to the world position
            transform.position = worldPosition;
        }

        if (Input.GetMouseButtonDown(0) && !goneDown)
        {
            goneDown = true;
            StartCoroutine(MoveDown());
        }
    }

    void OnTriggerEnter2D()
    {
        collided = true;
    }
    void OnTriggerExit2D()
    {
        collided = false;
    }


    IEnumerator MoveDown()
    {
        float movementPassed = 0f;
        while(movementPassed < 3.5)
        {
            lid.transform.position = new Vector3(lid.transform.position.x, lid.transform.position.y - 0.1f, lid.transform.position.z);
            movementPassed += 0.1f;
            yield return null;
        }

        if (collided)
        {
            bacteriaSpawnerScript.BacteriaCaught();
        } else {
            bacteriaSpawnerScript.PauseAll();
            missedCapture.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void MoveToBag()
    {
        StartCoroutine(MoveBag());
    }

    IEnumerator MoveBag()
    {
        //-2.5 -3.5
        Vector3 direction = (new Vector3(-2.5f, -7f, -0.1f) - transform.position).normalized;

        float a = -2.5f - transform.position.x;
        float aSquared = a * a;

        float b = -7f - transform.position.y;
        float bSquared = b * b;

        float cSquared = aSquared + bSquared;
        float c = Mathf.Sqrt(cSquared);

        float distance = c/60;


        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 120; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, i * 3f);
            yield return null;
        }

        for (int i = 0; i < 60; i++)
        {
            transform.Translate(direction * distance);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        ShowCase.SetActive(true);
        bacteriaSpawnerScript.GetReadyForShowCase();
    }
}
