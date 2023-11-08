using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("EnemyUI")]
    public int enemy_hp;

    [Header("Sprite Renderer")]
    private SpriteRenderer enemySr;

    [Header("Color")]
    private Color halfalphaColor;
    private Color fullalphaColor;

    private List<GameObject> missile_list = new List<GameObject>();

    private bool isHurt = false;
    public bool isFacingRight = true;

    public SoundManager sound_manager;

    public GameObject bullet;
    [SerializeField] Vector3 spawnBulletOffset;

    public void EnemyHurt()
    {
        StartCoroutine(SetIsHurtTime());
        StartCoroutine(AlphaBlink());
    }

    IEnumerator SetIsHurtTime()
    {
        isHurt = true;
        yield return new WaitForSeconds(1f);
        isHurt = false;
    }

    IEnumerator AlphaBlink()
    {
        while (isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            enemySr.color = halfalphaColor;
            yield return new WaitForSeconds(0.1f);
            enemySr.color = fullalphaColor;
        }
    }

    private void Awake()
    {
        sound_manager = FindObjectOfType<SoundManager>();
    }

    public void Recognize()
    {
        StartCoroutine(SpawnBullet());
    }
    public void UnRecognize()
    {
        StopCoroutine(SpawnBullet());
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_hp = 10;
        enemySr = GetComponent<SpriteRenderer>();

        halfalphaColor = new Color(enemySr.color.r, enemySr.color.g, enemySr.color.b, 0.6f);
        fullalphaColor = new Color(enemySr.color.r, enemySr.color.g, enemySr.color.b, 1f);
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (EnemySight.recognize == true)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.shot);
                GameObject missile_object = Instantiate(bullet, transform.position + spawnBulletOffset, bullet.transform.rotation);
                Missile missile_script = missile_object.GetComponent<Missile>();
                missile_script.MemoryShooter(this);
                missile_list.Add(missile_object);
                yield return new WaitForSeconds(2.0f);
            }
            else if(EnemySight.recognize == false)
            {
                yield break;
            }

        }
    }

    public void RemoveAll()
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

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
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
            RemoveAll();
        }


    }

    public virtual void TakeDamage(int damage)
    {
        enemy_hp -= damage;
        if(enemy_hp <= 0)
        {
            RemoveAll();
            Destroy(gameObject);
        }
    }


}
