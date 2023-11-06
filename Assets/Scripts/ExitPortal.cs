using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPortal : MonoBehaviour
{
    [SerializeField]
    bool player_in = false;

    [SerializeField]
    private GameObject clear_object;

    public SoundManager sound_manager;

    // Start is called before the first frame update
    void Start()
    {
        ClearDisable();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") > 0 && player_in == true)
        {
            ClearEnable();
            clear_object.SetActive(true);
        }
        else
        {
            ClearDisable();
        }
    }

    public void ClearEnable()
    {
        Pause.PauseGame();
        //���߿� Ŭ���� ���� �߰� �ʿ�
        sound_manager.SfxPlayer(SoundManager.sfx.page_up);
        //Ŭ���� â �ѱ�.
        clear_object.SetActive(true);
    }

    public void ClearDisable()
    {
        Pause.ResumeGame();
        //Ŭ���� â ����.
        clear_object.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_in = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_in = false;
        }
    }
}
