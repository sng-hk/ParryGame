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
        // 슬라이더 값으로 배경음악의 볼륨을 조절.
        bgm.volume = value;
    }
}
