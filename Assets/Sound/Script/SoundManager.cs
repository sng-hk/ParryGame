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
    public AudioSource[] sfx_player;
    public AudioClip[] sfx_clip;
    public enum sfx {dash, game_over};
    int sfx_cousor = 0;
    

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

    public void SfxPlayer(sfx type)
    {
        switch (type)
        {
            case sfx.dash:
                sfx_player[sfx_cousor].clip = sfx_clip[0];
                break;
            case sfx.game_over:
                sfx_player[sfx_cousor].clip = sfx_clip[1];
                break;
        }

        sfx_player[sfx_cousor].Play();
        sfx_cousor = (sfx_cousor + 1) % sfx_player.Length;
    }
}
