using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEmeny : Enemy
{
    public GameObject drops;
    [SerializeField] Vector3 spawnBulletOffset;

    public float speed; // speed of the platform
    public int startingPoint; // starting index (position of the platform)
    public Transform[] points; // an array of transform points

    [SerializeField] private int i; // index of the array
    [SerializeField] private int id;
    SpriteRenderer sr;

    public override void Recognize()
    {
        canAttack = false;
        animator.SetTrigger("attack");
    }

    void Start()
    {
        speed = 2;
        transform.position = points[startingPoint].position;
        canAttack = true;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f) // check if the platform was on the olast point after the increase
        {
            i++;
            sr.flipX = true;
            if (i == points.Length)
            {
                i = 0; // reset the index
                sr.flipX = false;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

        for (int i = missile_list.Count - 1; i >= 0; i--)
        {
            GameObject missile_object = missile_list[i];
            if (missile_object == null)
            {
                // 투사체가 삭제되었을 경우 리스트에서 제거
                missile_list.RemoveAt(i);
            }
        }


    }

    public override void SpawnBullet()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.shot);
        GameObject missile_object = Instantiate(drops, transform.position + spawnBulletOffset, drops.transform.rotation);
        Drops missile_script = missile_object.GetComponent<Drops>();
        missile_script.MemoryShooter(this);
        missile_list.Add(missile_object);

        // 2초 뒤에 적이 공격할 수 있는 준비가 됨
        StartCoroutine(EnableAttackAfterSeconds(2));
    }

    /*
    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (FlyingEnemySight.recognize == true)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.shot);
                GameObject missile_object = Instantiate(drops, transform.position + spawnBulletOffset, drops.transform.rotation);
                Drops missile_script = missile_object.GetComponent<Drops>();
                missile_script.MemoryShooter(this);
                missile_list.Add(missile_object);
                yield return new WaitForSeconds(2.0f);
            }
            else if (FlyingEnemySight.recognize == false)
            {
                yield break;
            }

        }
    }
    */

}
