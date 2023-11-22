using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("EnemyUI")]
    public int enemy_hp;

    [Header("Sprite Renderer")]
    protected SpriteRenderer enemySr;

    [Header("Color")]
    private Color halfalphaColor;
    private Color fullalphaColor;

    [Header("Animator")]
    [SerializeField] protected Animator animator;

    private List<GameObject> missile_list = new List<GameObject>();

    private bool isHurt = false;
    public int isFacingRight = 1;
    public bool canAttack;

    public SoundManager sound_manager;

    [Header("Instantiate Object")]
    public GameObject bullet;
    private float bulletspawnDistance = 1;
    [SerializeField] protected GameObject deadSmokeParticle;

    public Vector3 defaultLocalScale;

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

    public virtual void Recognize()
    {
        /*StartCoroutine(SpawnBullet());*/
        canAttack = false;
        animator.SetTrigger("attack");
    }

    /*public void UnRecognize()
    {
        StopCoroutine(SpawnBullet());
    }*/

    private void Awake()
    {
        sound_manager = FindObjectOfType<SoundManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        enemy_hp = 10;
        enemySr = GetComponent<SpriteRenderer>();

        canAttack = true;

        halfalphaColor = new Color(enemySr.color.r, enemySr.color.g, enemySr.color.b, 0.6f);
        fullalphaColor = new Color(enemySr.color.r, enemySr.color.g, enemySr.color.b, 1f);

        animator = GetComponent<Animator>();

        defaultLocalScale = transform.localScale;
    }    

    // animator 에서 attack모션이 끝나는 시점에 애니메이션 이벤트로 추가
    public virtual void SpawnBullet()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.shot);
        // 플레이어, 적 사이를 잇는 직선상에서 적과 일정범위 떨어진 곳에 투사체 생성 bulletspawnDistance 값을 조정하여
        // 적과 어느정도 떨어진 거리에서 투사체를 생성할 건지 결정
        // 플레이어 방향을 구합니다.
        Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
        Vector3 instantiatePosition = Vector3.Normalize(playerDirection);

        // 플레이어 방향으로 회전한 각도를 구합니다.
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        GameObject missile_object = Instantiate(bullet, transform.position + instantiatePosition * bulletspawnDistance, Quaternion.Euler(0,0,angle));
        Missile missile_script = missile_object.GetComponent<Missile>();
        missile_script.MemoryShooter(this);
        missile_list.Add(missile_object);

        // 2초 뒤에 적이 공격할 수 있는 준비가 됨
        StartCoroutine(EnableAttackAfterSeconds(2));
    }

    IEnumerator EnableAttackAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    /*(애니메이션 넣기 전 spawnbullet)*/
    /*public IEnumerator SpawnBullet()
    {
        animator.SetTrigger("attack");
        
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
                break;
                // (수정 전 코드 : yield break; )
            }

        }
    }*/

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
        Facing();

        if (enemy_hp <= 0)
        {
            RemoveAll();
        }

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
    public void Facing()
    {
        Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;

        // 플레이어가 적보다 오른쪽에 있을 경우
        if (playerDirection.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFacingRight = 1;
        }
        // 플레이어가 적보다 왼쪽에 있을 경우
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFacingRight = -1;
        }
    }

    public void DeadParticle()
    {
        Instantiate(deadSmokeParticle, transform.position, Quaternion.identity);
    }

    public void DeadParticle(float x, float y, float z)
    {
        Instantiate(deadSmokeParticle, transform.position + new Vector3(x, y, z), Quaternion.identity);
    }

    public virtual void TakeDamage(int damage)
    {
        enemy_hp -= damage;
        if (enemy_hp <= 0)
        {
            RemoveAll();
            DeadParticle();
            Destroy(gameObject);
        }
    }


}
