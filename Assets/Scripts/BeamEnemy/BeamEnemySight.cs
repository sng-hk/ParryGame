using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEnemySight : EnemySight
{    
    void Start()
    {
        enemy = GetComponentInParent<BeamEnemy>();
        Debug.Log(enemy);
    }

    void Update()
    {
        
    }
    

}
