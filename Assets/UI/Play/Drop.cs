using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health_Point.HP -= Health_Point.MaxHP;
    }
}
