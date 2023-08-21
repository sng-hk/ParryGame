using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoTitle2 : MonoBehaviour
{
    public void ClickGoTitle()
    {
        SceneManager.LoadScene("title");
        Resources.UnloadUnusedAssets();
    }
}
