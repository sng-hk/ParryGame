using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private bool isActiveShield = false;
    private bool canActiveShield = true;
    private float ShieldActiveTimer = 1f;
    private float ShieldCoolDown = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.C) && canActiveShield)        
            StartCoroutine(nameof(ActivateShield));              
    }

    IEnumerator ActivateShield()
    {
        isActiveShield = true;
        canActiveShield = false;
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(ShieldActiveTimer);
        transform.GetChild(0).gameObject.SetActive(false);
        isActiveShield = false;
        yield return new WaitForSeconds(ShieldCoolDown);
        canActiveShield = true;
    }
}
