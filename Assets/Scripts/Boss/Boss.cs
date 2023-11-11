using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private GameObject exit_door;

    [SerializeField]
    private GameObject Boss_object;

    public string boss_names;

    private void Awake()
    {
        boss_names = "skelton";
        enemy_hp = 100;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if(EnemySight.recognize == true)
        {
            Boss_object.SetActive(true);
        }
        else if(EnemySight.recognize == false || enemy_hp <= 0)
        {
            Boss_object.SetActive(false);
        }
    }

    public override void TakeDamage(int damage)
    {
        //지금은 테스트용으로 데미지 5배(=2대만 맞아도 죽게 만듦)
        enemy_hp -= damage*5;
        if (enemy_hp <= 0)
        {
            RemoveAll();
            exit_door.SetActive(true);
            Boss_object.SetActive(false);
            Destroy(gameObject);
        }
    }

}
