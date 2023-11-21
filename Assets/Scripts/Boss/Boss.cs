using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private GameObject exit_door;

    [SerializeField]
    private GameObject Boss_object;


    SpriteRenderer sr;

    public string boss_names;

    private void Awake()
    {
        boss_names = "skelton";
        enemy_hp = 50;
    }
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Facing();
    }


    public override void Recognize()
    {
        base.Recognize();
        Boss_object.SetActive(true);
    }

    new IEnumerator DeadParticle()
    {        
        base.DeadParticle();
        yield return new WaitForSeconds(0.2f);
        base.DeadParticle(-1, 0.5f, 0);
        yield return new WaitForSeconds(0.1f);
        base.DeadParticle(0, 1, 0);
        base.DeadParticle(-1f, 1, 0);
        base.DeadParticle(0, 3, 0);
        yield return new WaitForSeconds(0.1f);
        base.DeadParticle(0, 2, 0);
        base.DeadParticle(-0.2f, 1, 0);
        yield return new WaitForSeconds(0.2f);
        base.DeadParticle(0.6f, 2, 0);
        base.DeadParticle();
        yield return new WaitForSeconds(0.4f);
        base.DeadParticle(0, 3, 0);
        base.DeadParticle(-0.2f, 1, 0);
        yield return new WaitForSeconds(0.1f);
        base.DeadParticle(2, 1, 0);
        Destroy(gameObject);
    }

    public override void TakeDamage(int damage)
    {
        enemy_hp -= damage;
        if (enemy_hp <= 0)
        {
            RemoveAll();
            sr.color = new Color(0, 0, 0, 0);
            StartCoroutine(DeadParticle());
            exit_door.SetActive(true);
        }
    }

}
