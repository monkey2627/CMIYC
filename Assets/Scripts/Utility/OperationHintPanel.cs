using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OperationHintPanel : MonoBehaviour
{
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener((() =>
        {
            StartCoroutine(Fade());
        }));
    }

    IEnumerator Fade()
    {
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        
        this.gameObject.SetActive(false);
        Cat.instance.enable = true;
    }
}
