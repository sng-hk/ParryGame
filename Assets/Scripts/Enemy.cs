using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy_object;

    [Header("EnemyUI")]
    public int enemy_hp;

    [Header("Sprite Renderer")]
    private SpriteRenderer enemySr;

    [Header("Color")]
    private Color halfalphaColor;
    private Color fullalphaColor;


    private bool isHurt = false;


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
        StartCoroutine(SpawnBullet());
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_hp = 10;
        enemySr = GetComponent<SpriteRenderer>();

        halfalphaColor = new Color(enemySr.color.r, enemySr.color.g, enemySr.color.b, 0.6f);
        fullalphaColor = new Color(enemySr.color.r, enemySr.color.g, enemySr.color.b, 1f);
    }


    // Update is called once per frame
    void Update()
    {
        if(enemy_hp <= 0)
        {
            _enemy_object.SetActive(false);
        }
    }

    IEnumerator SpawnBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            if (EnemySight.recognize == true)
            {
                Instantiate(bullet, transform.position + spawnBulletOffset, bullet.transform.rotation);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemy_hp -= damage;
    }
}
