using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Go_Title : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoTitle()
    {
        SceneManager.LoadScene("title");
        Health_Point.HP = Health_Point.MaxHP;
        Resources.UnloadUnusedAssets();
    }
}
