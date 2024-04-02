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

    public IEnumerator LoadScene(string someScene)
    {
        _loadingPanel.SetPanel(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(someScene);

        asyncOperation.allowSceneActivation = false;

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



}
