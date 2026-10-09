using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAScript : MonoBehaviour
{
    public Camera mainCamera;
    public MatchGameScript matchGameScript;
    public GameObject green;
    public int id;
    public bool choosenAlready = false;
    // Start is called before the first frame update
    void Start()
    {
        green.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHovering() && Input.GetMouseButtonDown(0) && !choosenAlready)
        {
            if (matchGameScript.Slot1 == 0)
            {
                matchGameScript.Slot1 = id;
                green.SetActive(true);
            } else if (matchGameScript.Slot2 == 0)
            {
                matchGameScript.Slot2 = id;
                matchGameScript.CheckWin();
            }

            choosenAlready = true;
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
