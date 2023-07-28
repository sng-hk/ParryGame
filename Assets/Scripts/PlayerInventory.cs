using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfKeyItems { get; private set; }

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
}
