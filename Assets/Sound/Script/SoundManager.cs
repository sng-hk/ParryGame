using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Text sound_value_text;
    public Slider sound_bar;

    [Header("sound")]
    public static int sound_value = 20;

    public AudioSource bgm;

    public void TextChanger(float value)
    {
        //사운드 크기에 맞춰 변하는 텍스트.
        sound_value = (int)(value * 100);
        sound_value_text.text = sound_value.ToString();
    }

    public void VolumeChanger(float value)
    {
        // 슬라이더 값으로 배경음악의 볼륨을 조절.
        bgm.volume = value;
    }

    void Start()
    {
        bgm.volume = sound_value / 100.0f;
        sound_value_text.text = sound_value.ToString();
        sound_bar.value = sound_value / 100.0f;
        bgm.Play();
    }

    void Update()
    {
        
    }


}
