using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEnemy_Boss : BeamEnemy
{
    [SerializeField]
    private GameObject exit_door;

    [SerializeField]
    private GameObject Boss_object;

    public string boss_names;

    private void Awake()
    {
        boss_names = "darkmage";
        enemy_hp = 50;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }

    public override void Recognize()
    {
        recognize = true;
        StartCoroutine(FireBeam());
        Boss_object.SetActive(true);
    }
    public override void UnRecognize()
    {
        recognize = false;
        StopCoroutine(FireBeam());
        Boss_object.SetActive(false);
    }

    public override void TakeDamage(int damage)
    {
        enemy_hp -= damage;
        if (enemy_hp <= 0)
        {
            RemoveAll();
            exit_door.SetActive(true);
            Boss_object.SetActive(false);
            Destroy(gameObject);
        }
    }
}
