using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Point : MonoBehaviour
{
    [SerializeField]
    private Image Heart;

    [SerializeField]
    private Image Heart2;

    [SerializeField]
    private Image Heart3;

    [SerializeField]
    private Image GG_Background;

    [SerializeField]
    private Text GG_Text;

    [SerializeField]
    private Button Go_Title;

    [SerializeField]
    private Image Pause_Background;

    public static float MaxHP = 6;
    public static float HP = 6;
    // Start is called before the first frame update
    void Start()
    {
        Heart.fillAmount = 1;
        Heart2.fillAmount = 1;
        Heart3.fillAmount = 1;
        GG_Background.enabled = false;
        GG_Text.enabled = false;
        Go_Title.interactable = false;
        Go_Title.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 6 && HP >= 5)
        {
            Heart3.fillAmount = (HP - 4) / 2;
        }
        else if (HP <= 4 && HP >= 3)
        {
            Heart3.fillAmount = 0;
            Heart2.fillAmount = (HP - 2) / 2;
        }
        else if (HP <= 2 && HP >= 1)
        {
            Heart3.fillAmount = 0;
            Heart2.fillAmount = 0;
            Heart.fillAmount = HP / 2;
        }
        else
        {
            Heart3.fillAmount = 0;
            Heart2.fillAmount = 0;
            Heart.fillAmount = 0;
            GG_Background.enabled = true;
            GG_Text.enabled = true;
            Go_Title.interactable = true;
            Go_Title.gameObject.SetActive(true);
            Pause.PauseGame();
        }
    }
}
