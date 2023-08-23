using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEmeny : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy_object;

    [Header("EnemyUI")]
    public int enemy_hp;

    private List<GameObject> missile_list = new List<GameObject>();

    private bool isHurt = false;

    //public SoundManager sound_manager;

    public GameObject bullet;
    [SerializeField] Vector3 spawnBulletOffset;

    public float speed; // speed of the platform
    public int startingPoint; // starting index (position of the platform)
    public Transform[] points; // an array of transform points

    [SerializeField] private int i; // index of the array
    [SerializeField] private int id;


    private void Awake()
    {
        //sound_manager = FindObjectOfType<SoundManager>();
    }
    public void Recognize()
    {
        StartCoroutine(SpawnBullet());
    }
    public void UnRecognize()
    {
        StopCoroutine(SpawnBullet());
    }

    void Start()
    {
        enemy_hp = 10;
        speed = 2;
        transform.position = points[startingPoint].position;
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (EnemySight.recognize == true)
            {
                //sound_manager.SfxPlayer(SoundManager.sfx.shot);
                GameObject missile_object = Instantiate(bullet, transform.position + spawnBulletOffset, bullet.transform.rotation);
                Missile missile_script = missile_object.GetComponent<Missile>();
                //missile_script.MemoryShooter(this);
                missile_list.Add(missile_object);
                yield return new WaitForSeconds(2.0f);
            }
            else if (EnemySight.recognize == false)
            {
                yield break;
            }

        }
    }

    private void RemoveAll()
    {
        // 적이 비활성화될 때 자신이 쏜 모든 총알을 삭제
        foreach (GameObject missile_object in missile_list)
        {
            if (missile_object != null)
            {
                Destroy(missile_object);
            }
        }
        missile_list.Clear();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f) // check if the platform was on the olast point after the increase
        {
            i++;
            if (i == points.Length)
            {
                i = 0; // reset the index
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

        if (enemy_hp <= 0)
        {
            _enemy_object.SetActive(false);
            RemoveAll();
        }
    }

    public void TakeDamage(int damage)
    {
        enemy_hp -= damage;
    }
}
