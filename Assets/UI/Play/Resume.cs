using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    [SerializeField]
    private GameObject pause_object;


    public void ClickResumeButton()
    {
        Pause.ResumeGame();
        pause_object.SetActive(false);
    }
}
