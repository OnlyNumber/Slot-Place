using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    [SerializeField]
    private PanelControl _loadingPanel;

    [SerializeField]
    private Image loadingBar;

    public void LoadSlots()
    {
        StartCoroutine(LoadScene(StaticFields.SLOTS_SCENE));

    }

    public void LoadBuildings()
    {
        StartCoroutine(LoadScene(StaticFields.BUILDINGS_SCENE));
    }

    private void Update()
    {
        if(asyncOperation != null)
        loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1, 0.01f);
    }

    AsyncOperation asyncOperation;

    public IEnumerator LoadScene(string someScene)
    {
        _loadingPanel.SetPanel(true);

        asyncOperation = SceneManager.LoadSceneAsync(someScene);

        asyncOperation.allowSceneActivation = false;


        //loadingBar.fillAmount = Mathf.Lerp(asyncOperation.progress, 1, 1);

        while (!asyncOperation.isDone)
        {

            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1, 0.01f);

            if (asyncOperation.progress >= 0.9f && !asyncOperation.allowSceneActivation)
            {
                //loadingBar.fillAmount = asyncOperation.progress;

                yield return new WaitForSeconds(2f);
                asyncOperation.allowSceneActivation = true;
            }
            else
            {
               // loadingBar.fillAmount = asyncOperation.progress;
            }

            yield return null;
        }

    }

    /*public IEnumerator LoadScene(string someScene)
    {
        _loadingPanel.SetPanel(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(someScene);

        asyncOperation.allowSceneActivation = false;


        loadingBar.fillAmount = Mathf.Lerp(asyncOperation.progress, 1, 1);

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f && !asyncOperation.allowSceneActivation)
            {
                loadingBar.fillAmount = asyncOperation.progress;

                yield return new WaitForSeconds(1f);
                asyncOperation.allowSceneActivation = true;
            }
            else
            {
                loadingBar.fillAmount = asyncOperation.progress;
            }

                yield return null;
        }

    }
    */


}
