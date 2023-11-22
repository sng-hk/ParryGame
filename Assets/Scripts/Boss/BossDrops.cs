using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrops : MonoBehaviour
{
    Rigidbody2D rb;
    Renderer rend;
    public SoundManager sound_manager;
    public bool is_reflect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rend = GetComponent<Renderer>();
        float delayInSeconds = Random.Range(3, 21);
        Invoke("ChangeColor", delayInSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            sound_manager.SfxPlayer(SoundManager.sfx.parrying);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            //������ �Ҹ� �߰� �ʿ�
            sound_manager.SfxPlayer(SoundManager.sfx.attacked);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾� �ǰݽ� ����
            if (!is_reflect)
            {
                sound_manager.SfxPlayer(SoundManager.sfx.attacked);
                PlayerController.instance.player_helth_point -= 1;
                Destroy(gameObject);
            }
        }
    }

    public void ChangeColor()
    {
        rend.material.color = Color.red;
        Invoke("Drop", 1f);
    }

    public void Drop()
    {
        //�������� �Ҹ� �߰�
        rb.isKinematic = false;
    }
}
