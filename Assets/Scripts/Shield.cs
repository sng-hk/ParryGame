using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{    
    [SerializeField] private GameObject parrySucceed;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Missile bullet = collision.gameObject.GetComponent<Missile>();
            if(bullet != null)
            {
                Instantiate(parrySucceed, transform.position, Quaternion.identity);
            }
        }
    }
}
