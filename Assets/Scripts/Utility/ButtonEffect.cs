using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleSize = 1.2f;
    private Image image;


    private void Awake()
    {
        image = transform.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale *= scaleSize;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale /= scaleSize;
    }

}