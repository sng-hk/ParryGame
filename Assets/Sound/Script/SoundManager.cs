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
        //���� ũ�⿡ ���� ���ϴ� �ؽ�Ʈ.
        sound_value = (int)(value * 100);
        sound_value_text.text = sound_value.ToString();
    }

    public void VolumeChanger(float value)
    {
        // �����̴� ������ ��������� ������ ����.
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
