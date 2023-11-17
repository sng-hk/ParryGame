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

    [Header("Animator")]
    private Animator animator;

    private List<GameObject> missile_list = new List<GameObject>();

    private bool isHurt = false;
    public int isFacingRight = 1;
    public bool canAttack;

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

    public void Recognize()
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
    }

    public void AnimationEventSpawnBullet()
    {
        SpawnBullet();
    }

    // animator ���� attack����� ������ ������ �ִϸ��̼� �̺�Ʈ�� �߰�
    public void SpawnBullet()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.shot);
        // �÷��̾�, �� ���̸� �մ� �����󿡼� ���� �������� ������ ���� ����ü ���� bulletspawnDistance ���� �����Ͽ�
        // ���� ������� ������ �Ÿ����� ����ü�� ������ ���� ����
        // �÷��̾� ������ ���մϴ�.
        Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
        Vector3 instantiatePosition = Vector3.Normalize(playerDirection);

        // �÷��̾� �������� ȸ���� ������ ���մϴ�.
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        GameObject missile_object = Instantiate(bullet, transform.position + instantiatePosition * bulletspawnDistance, Quaternion.Euler(0,0,angle));
        Missile missile_script = missile_object.GetComponent<Missile>();
        missile_script.MemoryShooter(this);
        missile_list.Add(missile_object);

        // 2�� �ڿ� ���� ������ �� �ִ� �غ� ��
        StartCoroutine(EnableAttackAfterSeconds(2));
    }

    IEnumerator EnableAttackAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    /*(�ִϸ��̼� �ֱ� �� spawnbullet)*/
    /*public IEnumerator SpawnBullet()
    {
        animator.SetTrigger("attack");
        
        while (true)
        {
            if (EnemySight.recognize == true)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.shot);
                // �÷��̾�, �� ���̸� �մ� �����󿡼� ���� �������� ������ ���� ����ü ���� bulletspawnDistance ���� �����Ͽ�
                // ���� ������� ������ �Ÿ����� ����ü�� ������ ���� ����
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
                // (���� �� �ڵ� : yield break; )
            }

        }
    }*/

    public void RemoveAll()
    {
        // ���� ��Ȱ��ȭ�� �� �ڽ��� �� ��� �Ѿ��� ����
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
        Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;

        if (playerDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // ������ �������� ��
            isFacingRight = 1;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // ���� �������� ��
            isFacingRight = -1;
        }

        for (int i = missile_list.Count - 1; i >= 0; i--)
        {
            GameObject missile_object = missile_list[i];
            if (missile_object == null)
            {
                // ����ü�� �����Ǿ��� ��� ����Ʈ���� ����
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
        if (enemy_hp <= 0)
        {
            RemoveAll();
            Destroy(gameObject);
        }
    }


}
