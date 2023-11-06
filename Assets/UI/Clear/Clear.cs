using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    public PlayerInventory player_inventory;

    public Image[] coins;
    public bool[] coin = { false, false, false };



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coin = player_inventory.ReturnCoin();
        for (int i = 0; i < 3; i++)
        {
            if (coin[i] == true)
            {
                coins[i].enabled = true;
            }
            else
            {
                coins[i].enabled = false;
            }
        }
    }
}
