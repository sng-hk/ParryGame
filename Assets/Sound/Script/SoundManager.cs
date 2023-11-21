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
    public enum sfx { paper_turn, shot_beam, shot_danger_line, dash, game_over, attacked, parrying, shot, get, jump, door_open, time_stop, page_up, shild_on};
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
            case sfx.shot:
                sfx_player[sfx_cousor].clip = sfx_clip[2];
                break;
            case sfx.time_stop:
                sfx_player[sfx_cousor].clip = sfx_clip[3];
                break;
            case sfx.shild_on:
                sfx_player[sfx_cousor].clip = sfx_clip[4];
                break;
            case sfx.page_up:
                sfx_player[sfx_cousor].clip = sfx_clip[5];
                break;
            case sfx.parrying:
                sfx_player[sfx_cousor].clip = sfx_clip[6];
                break;
            case sfx.door_open:
                sfx_player[sfx_cousor].clip = sfx_clip[7];
                break;
            case sfx.jump:
                sfx_player[sfx_cousor].clip = sfx_clip[8];
                break;
            case sfx.get:
                sfx_player[sfx_cousor].clip = sfx_clip[9];
                break;
            case sfx.attacked:
                sfx_player[sfx_cousor].clip = sfx_clip[10];
                break;
            case sfx.shot_beam:
                sfx_player[sfx_cousor].clip = sfx_clip[11];
                break;
            case sfx.shot_danger_line:
                sfx_player[sfx_cousor].clip = sfx_clip[12];
                break;
            case sfx.paper_turn:
                sfx_player[sfx_cousor].clip = sfx_clip[13];
                break;
        }

        sfx_player[sfx_cousor].Play();
        sfx_cousor = (sfx_cousor + 1) % sfx_player.Length;
    }
}
