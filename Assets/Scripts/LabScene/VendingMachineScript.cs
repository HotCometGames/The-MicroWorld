using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineScript : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject vendingMachineUI;
    public bool on = true;
    public String currentCode;
    public String[] possibleCodes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsHovering() && on)
        {
            vendingMachineUI.SetActive(true);
        }
    }
    public void AddString(string code)
    {
        currentCode += code;
    }
    public void check()
    {
        for (int i = 0; i < possibleCodes.Length; i++)
        {
            if (possibleCodes[i] == currentCode)
            {
                Buy(currentCode);
            }
        }
    }
    public void Buy(string code)
    {
       Debug.Log("Bought " + currentCode);
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
