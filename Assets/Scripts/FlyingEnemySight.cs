using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemySight : MonoBehaviour
{
    [Header("recognize")]
    public static bool recognize = false;
    FlyingEmeny parent_script;

    // Start is called before the first frame update
    void Start()
    {
        parent_script = GetComponentInParent<FlyingEmeny>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent_script.Recognize();
            recognize = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent_script.UnRecognize();
            recognize = false;
        }
    }
}
