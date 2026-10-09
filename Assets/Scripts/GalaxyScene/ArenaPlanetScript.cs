using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPlanetScript : MonoBehaviour
{
    public GameObject ArenaPlanet;
    public Camera mainCamera;
    public float rotationSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed);

        if (IsHovering() && ArenaPlanet.transform.localScale.x < 0.6)
        {
            ArenaPlanet.transform.localScale = new Vector3(ArenaPlanet.transform.localScale.x + 0.02f, ArenaPlanet.transform.localScale.y + 0.02f, ArenaPlanet.transform.localScale.z);
        }
        if (!IsHovering() && ArenaPlanet.transform.localScale.x > 0.5)
        {
            ArenaPlanet.transform.localScale = new Vector3(ArenaPlanet.transform.localScale.x - 0.02f, ArenaPlanet.transform.localScale.y - 0.02f, ArenaPlanet.transform.localScale.z);
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
}
