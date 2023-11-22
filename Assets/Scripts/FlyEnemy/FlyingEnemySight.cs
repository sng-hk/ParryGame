using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemySight : EnemySight
{
    
    void Start()
    {
        enemy = GetComponentInParent<FlyingEmeny>();
        Debug.Log(enemy);
    }

    void Update()
    {

    }
}
