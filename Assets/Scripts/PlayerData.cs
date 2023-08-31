using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{    
    public float health;
    public float[] position; // float[] <--> Vector3

    public PlayerData (PlayerController player)
    {
        health = player.player_helth_point;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;        
    }
}
