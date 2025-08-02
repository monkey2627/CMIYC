using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
       
    }
    /// <summary>
    /// 通关之后调用
    /// </summary>
    public void Win()
    {
        //一系列处理

        Destroy(scenes[0]);
        // 从资源中加载场景
        GameObject prefab = Resources.Load<GameObject>("Level");
        // 检查Prefab是否加载成功
        if (prefab != null)
        {
            // 实例化Prefab到场景中
            GameObject instantiatedObject = Instantiate(prefab);
            Cat.instance.sceneNow = instantiatedObject.GetComponent<Level>();
            scenes[0] = instantiatedObject.GetComponent<Level>();
        }
        //重置小猫、小狗、主人的位置

    }
}
