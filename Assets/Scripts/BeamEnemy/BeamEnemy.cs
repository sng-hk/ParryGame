using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy_object;

    [Header("EnemyUI")]
    public int enemy_hp;

    public GameObject player;
    public Vector3 player_position;
    public Vector3 line_destroy_point;

    private List<GameObject> missile_list = new List<GameObject>();

    public SoundManager sound_manager;


    public LayerMask layerMask;
    public GameObject danger_line;
    public GameObject beam;
    public float fireInterval;

    public bool recognize;

    [SerializeField] Vector3 spawnBulletOffset;

    private void Awake()
    {
        sound_manager = FindObjectOfType<SoundManager>();
    }

    public void Recognize()
    {
        Debug.Log("fire");
        recognize = true;
        StartCoroutine(FireBeam());
    }

    public void UnRecognize()
    {
        recognize = false;
        StopCoroutine(FireBeam());
        Debug.Log("stop");
    }

    private void RemoveAll()
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

    IEnumerator FireBeam()
    {
        while (true)
        {
            if(recognize == false)
            {
                yield break;
            }

            yield return new WaitForSeconds(fireInterval);
            DangerLine();
            yield return new WaitForSeconds(2f);
            ShootBeam();
        }
    }

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
            _enemy_object.SetActive(false);
            RemoveAll();
        }
    }
    public void TakeDamage(int damage)
    {
        enemy_hp -= damage;
    }
}