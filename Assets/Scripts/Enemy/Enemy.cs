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
    public int isFacingRight = 1;

    public SoundManager sound_manager;

    public GameObject bullet;
    float bulletspawnDistance = 1;

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
        //지금 이거땜에 시야 나가면 다 사라짐.
        RemoveAll();
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
                // 플레이어, 적 사이를 잇는 직선상에서 적과 일정범위 떨어진 곳에 투사체 생성 bulletspawnDistance 값을 조정하여
                // 적과 어느정도 떨어진 거리에서 투사체를 생성할 건지 결정
                Vector3 instantiatePosition = Vector3.Normalize(PlayerController.instance.transform.position - transform.position);
                GameObject missile_object = Instantiate(bullet, transform.position + instantiatePosition * bulletspawnDistance, Quaternion.Euler(instantiatePosition));
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
