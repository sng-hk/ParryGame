using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            PlayerController.instance.player_helth_point -= PlayerController.instance.player_max_helth_point;
        }
    }
}
