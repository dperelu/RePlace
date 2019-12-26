using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour {

    public GameObject loadingScreen;
    //public Slider slider;

    public void LoadLevel(int sceneIndex)
    {
        GameManagerComp gm = FindObjectOfType<GameManagerComp>();
        if (gm !=null)
            Destroy(gm.gameObject);
        StartCoroutine(LoadAsynchronously(sceneIndex));
        
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            //slider.value = progress;

            yield return null;

        }
    }

}
