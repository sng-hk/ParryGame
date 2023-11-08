using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private GameObject exit_door;

    // Start is called before the first frame update
    void Start()
    {
        enemy_hp = 100;
    }

    public override void TakeDamage(int damage)
    {
        //지금은 테스트용으로 데미지 5배(=2대만 맞아도 죽게 만듦)
        enemy_hp -= damage*5;
        if (enemy_hp <= 0)
        {
            RemoveAll();
            exit_door.SetActive(true);
            Destroy(gameObject);
        }
    }
}
