using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEnemy : Enemy
{            
    public Vector3 player_position;
    public Vector3 line_destroy_point;

    private List<GameObject> missile_list = new List<GameObject>();    

    public LayerMask layerMask;
    public GameObject danger_line;
    public GameObject beam;
    public float fireInterval;

    public bool recognize;

    [SerializeField] Vector3 spawnBulletOffset;

    public override void Recognize()
    {
        canAttack = false;
        animator.SetTrigger("attack");
        sound_manager.SfxPlayer(SoundManager.sfx.shot_danger_line);
        DangerLine();
    }    

    // �����غ� ����� ������ ���� �ִϸ��̼� �̺�Ʈ�� ó��
    public override void SpawnBullet()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.shot_beam);
        ShootBeam();
    }

    public void RemoveAll()
    {
        //�ڽ��� �� ��� �Ѿ��� ����
        foreach (GameObject missile_object in missile_list)
        {
            if (missile_object != null)
            {
                Destroy(missile_object);
            }
        }
        missile_list.Clear();
    }

    // ���� �غ� �߿� ����ü�� ���� ������ �̸� �����ش�.
    void DangerLine()
    {
        GameObject line_clone = Instantiate(danger_line, transform.position, Quaternion.identity);
        DangerLine missile_script = line_clone.GetComponent<DangerLine>();
        missile_script.MemoryShooter(this);
    }


    void ShootBeam()
    {
        GameObject beam_clone = Instantiate(beam, transform.position, Quaternion.identity);
        Beam beam_script = beam_clone.GetComponent<Beam>();
        beam_script.start_point = transform.position;
        beam_script.end_point = line_destroy_point;
    }

    void Start()
    {
        enemy_hp = 10;
        fireInterval = 2f;

        canAttack = true;
    }

    void Update()
    {
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
