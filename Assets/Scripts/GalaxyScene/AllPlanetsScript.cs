using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllPlanetsScript : MonoBehaviour
{
    public void MoveRight(){
        StartCoroutine(Move(-0.1f));
    }

    public void MoveLeft(){
        StartCoroutine(Move(0.1f));
    }

    IEnumerator Move(float amountToMove)
    {
        float movementPassed = 0f;
        while(movementPassed < 20)
        {
            transform.position = new Vector3(transform.position.x + amountToMove, transform.position.y, transform.position.z);
            movementPassed += Mathf.Abs(amountToMove);
            yield return null;
        }
    }
}
