using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeChanger(float value)
    {
        // �����̴� ������ ��������� ������ ����.
        bgm.volume = value;
    }
}
