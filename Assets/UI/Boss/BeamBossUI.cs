using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamBossUI : MonoBehaviour
{
    private BeamEnemy boss;

    public Text boss_name;
    public Slider boss_hp_bar;


    // Start is called before the first frame update
    void Start()
    {
        boss_name.text = "boss";
        boss = GameObject.Find("beam_boss").GetComponent<BeamEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        /*boss_name.text = boss.boss_names;*/
        boss_hp_bar.value = 1 - boss.enemy_hp / 50.0f;
    }
}
