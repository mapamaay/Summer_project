using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentity : MonoBehaviour
{
    //Player variables//
    GameObject player1 = null;
    
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        if (player1 is null)
        {
            Debug.Log("I am Player 1");
            this.gameObject.tag = "Player 1";
        }
        else
        {
            Debug.Log(player1);
            Debug.Log("I am Player 2");
            this.gameObject.tag = "Player 2";
        }
    }

}
