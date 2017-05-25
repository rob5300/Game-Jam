using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour {

    private bool loadScene = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private GameObject LoadingIMG;


    void Update()

    {// If the player has pressed the space bar and a new scene is not loading yet...
        if (Input.GetKeyUp(KeyCode.Space) && loadScene == false)
        { // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;
            loadingText.text = "Loading...";
            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());
            LoadingIMG.SetActive(true);
        }

        if (loadScene == true)  // If the new scene has started loading...
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
        }
    }

    public void onButtonClick()
    {
        loadScene = true;
        loadingText.text = "Loading...";
        StartCoroutine(LoadNewScene());
        LoadingIMG.SetActive(true);

        if (loadScene == true)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }
}
