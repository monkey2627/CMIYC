using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public GameObject operationHintPanel;
    public Button startButton;
    public Button exitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            //BlackMaskPanel.instance.Show();
            StartCoroutine(Fade());
            SceneManager.LoadSceneAsync(1);
        });
        
        
        exitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }

    IEnumerator Fade()
    {
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
