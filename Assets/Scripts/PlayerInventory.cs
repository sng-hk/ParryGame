using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfKeyItems { get; private set; }
    public bool[] sequence_of_coin = { false, false, false };
    public int number_of_coin = 0;

    public UnityEvent<PlayerInventory> OnKeyItemCollected;

    public void KeyItemCollected()
    {
        NumberOfKeyItems++;
        OnKeyItemCollected.Invoke(this);
    }

    public void KeyItemUsed()
    {
        NumberOfKeyItems--;
    }

    public void AddCoin(int x)
    {
        sequence_of_coin[x] = true;
        number_of_coin++;
    }

    public bool[] ReturnCoin()
    {
        return sequence_of_coin;
    }

    public int Coins()
    {
        return number_of_coin;
    }
}
