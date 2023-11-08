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
        //������ �׽�Ʈ������ ������ 5��(=2�븸 �¾Ƶ� �װ� ����)
        enemy_hp -= damage*5;
        if (enemy_hp <= 0)
        {
            RemoveAll();
            exit_door.SetActive(true);
            Destroy(gameObject);
        }
    }
}
