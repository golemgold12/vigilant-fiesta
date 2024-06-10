using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool loading = false;
    public void LoadIt()
    {

        if (loading == false)
        {
            loading = true;
            StartCoroutine(SceneTransition());
        }
    }
    IEnumerator SceneTransition()
    {
        Debug.Log(SceneManager.sceneCount);
        yield return new WaitForSeconds(.5f);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCount + 1 > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void QuitButton()
    {

        Application.Quit();

    }
}
