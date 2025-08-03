using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            BlackMaskPanel.instance.Show();
            this.gameObject.SetActive(false);
            operationHintPanel.gameObject.SetActive(true);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
