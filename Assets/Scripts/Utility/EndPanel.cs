using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener((() =>
        {
            /*CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.DOFade(0, 0.5f);*/
            this.gameObject.SetActive(false); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }));   
    }

    // Update is called once per frame
    void Update()
    {
        Cat.instance.enable = false;
    }
}
