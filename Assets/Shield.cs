using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private bool isActiveShield = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isActiveShield = true;
        transform.GetChild(0).gameObject.SetActive(Input.GetKey(KeyCode.C));
    }
}
