using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoTitle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickGoTitle()
    {
        SceneManager.LoadScene("title");
        Pause.ResumeGame();
        PlayerController.instance.player_helth_point = PlayerController.instance.player_max_helth_point;
        Resources.UnloadUnusedAssets();
    }
}
