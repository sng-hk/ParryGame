using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{    
    private bool canActiveShield = true;
    private float ShieldActiveTimer = 0.2f;
    private float ShieldCoolDown = 0.1f;
    [SerializeField] private Animator playerAnimator;

    SpriteRenderer sr;

    public SoundManager sound_manager;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.패링 키) && canActiveShield)*/
        if (Input.GetKeyDown(KeyCode.LeftControl) && canActiveShield)
        {
            StartCoroutine(nameof(ActivateShield));
        }
    }

    IEnumerator ActivateShield()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.shild_on);        
        canActiveShield = false;
        transform.GetChild(0).gameObject.SetActive(true);
        /*animator.SetBool("isActive", true);*/
        playerAnimator.SetTrigger("ShieldTrigger");
        sr.flipX = true;
        yield return new WaitForSeconds(0.35f); /*패링 활성화 시간*/
        sr.flipX = false;
        transform.GetChild(0).gameObject.SetActive(false);
        /*yield return new WaitForSeconds(0.4f); *//*패링 쿨타임 (기호에 맞게) */
        canActiveShield = true;
    }       

}
