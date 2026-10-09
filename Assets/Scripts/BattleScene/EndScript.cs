using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public GameObject player;
    public GameOperatorScript gameOperatorScript;
    public bool won = false;
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.x >= transform.position.x -11.5 && !won)
            {
                won = true;
                gameOperatorScript.won();
            }
        }
    }
}
