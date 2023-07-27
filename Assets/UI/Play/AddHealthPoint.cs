using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerController.instance.player_helth_point+2> PlayerController.instance.player_max_helth_point)
        {
            PlayerController.instance.player_helth_point = PlayerController.instance.player_max_helth_point;
        }
        else
        {
            PlayerController.instance.player_helth_point += 2;
        }
        gameObject.SetActive(false);
    }
}
