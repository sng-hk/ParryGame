using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public bool isActiveShield = false;
    private bool canActiveShield = true;
    private float ShieldActiveTimer = 0.2f;
    private float ShieldCoolDown = 0.1f;

    [SerializeField] private Animator animator;
    [SerializeField] private Animator playerAnimator;

    public SoundManager sound_manager;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canActiveShield)
        {
            /*StartCoroutine(nameof(ActivateShield));*/
            ActivateShield();
        }
    }

    void ActivateShield()
    {
        sound_manager.SfxPlayer(SoundManager.sfx.shild_on);
        isActiveShield = true;
        canActiveShield = false;
        transform.GetChild(0).gameObject.SetActive(true);
        /*animator.SetBool("isActive", true);*/
        playerAnimator.Play("ParryAnim");
    }

    void EnableShield()
    {
        playerAnimator.SetBool("isActiveShield", false);
        transform.GetChild(0).gameObject.SetActive(false);
        isActiveShield = false;
        canActiveShield = true;
    }



}
