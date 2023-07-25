using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    [SerializeField]
    private GameObject pause_object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickResumeButton()
    {
        Pause.ResumeGame();
        pause_object.SetActive(false);
    }
}
