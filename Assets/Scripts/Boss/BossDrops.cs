using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrops : MonoBehaviour
{
    [SerializeField]
    private GameObject Drop_object1;
    private GameObject Drop_object2;

    // Start is called before the first frame update
    void Start()
    {
        Drop_object1.SetActive(true);
        Drop_object2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
