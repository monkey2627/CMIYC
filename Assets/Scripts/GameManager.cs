using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level;
    public static GameManager instance;
    public SceneBase[] scenes;
    public int sceneNow;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartNewGame()
    {
        Cat.instance.sceneNow = scenes[0];
    }
    public void AddScene()
    {
        sceneNow++;
        if(sceneNow == scenes.Length)
        {
            sceneNow = 0;
        }
        Cat.instance.sceneNow = scenes[sceneNow];
    }
}
