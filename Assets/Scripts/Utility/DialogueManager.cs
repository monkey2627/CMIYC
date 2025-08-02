using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    
    public CanvasGroup BubbleACanvasGroup;
    public CanvasGroup BubbleBCanvasGroup;
    /*public Text BubbleAText;
    public Text BubbleBText;*/
    public float Duration = 30f;
    
    public List<string> ASentences = new List<string>();
    public List<string> BSentences = new List<string>();

    private void Start()
    {
        // StartDialogue();
        BubbleACanvasGroup.gameObject.SetActive(false);
        BubbleBCanvasGroup.gameObject.SetActive(false);
    }

    public void StartDialogue()
    {
        BubbleACanvasGroup.gameObject.SetActive(false);
        BubbleBCanvasGroup.gameObject.SetActive(false);
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {
        float totalDuration = Duration / (ASentences.Count + BSentences.Count);
        
        for (int i = 0; i < Mathf.Max(ASentences.Count, BSentences.Count); i++)
        {
            if (i < ASentences.Count)
            {
                yield return ShowDialogue(BubbleACanvasGroup, ASentences[i], totalDuration);
            }

            if (i < BSentences.Count)
            {
                yield return ShowDialogue(BubbleBCanvasGroup, BSentences[i], totalDuration);
            }
        }
    }

    private IEnumerator ShowDialogue(CanvasGroup dialogueBubble, string dialogue, float duration)
    {
        dialogueBubble.alpha = 0;
        dialogueBubble.gameObject.SetActive(true);
        dialogueBubble.DOFade(1, 0.5f).OnStart(() => dialogueBubble.GetComponentInChildren<Text>().text = dialogue); // 淡入

        yield return new WaitForSeconds(duration);
        
        dialogueBubble.DOFade(0, 0.5f); // 淡出
        yield return new WaitForSeconds(0.5f);
        dialogueBubble.gameObject.SetActive(false);
    }
    
   
}
