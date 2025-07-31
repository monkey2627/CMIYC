using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level;
    public static GameManager instance;
    public SceneManager[] scenes;
    public int sceneNow;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
