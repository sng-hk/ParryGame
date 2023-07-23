using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI keyItemText;

    private void Start()
    {
        keyItemText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateKeyItemText(PlayerInventory playerInventory)
    {
        keyItemText.text = playerInventory.NumberOfKeyItems.ToString();
    }

}
