using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public Slider progressbar;
    public Text loading_text;

    private void Start()
    {
        Resources.UnloadUnusedAssets();
        StartCoroutine(LoadScene());
        Debug.Log(GoStage.loding_scene);
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(GoStage.loding_scene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if(progressbar.value < 0.9f)
            {
                Debug.Log("Working2");
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }else if(operation.progress >= 0.9f) {
                Debug.Log("Working3");
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (progressbar.value >= 1f)
            {
                loading_text.text = "Press Spacebar";
            }



            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
