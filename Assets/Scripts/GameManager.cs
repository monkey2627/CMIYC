using DG.Tweening;
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
        if(Input.GetMouseButtonDown(0) && isEnding)
        {
            StartNewGame();
        }
    }
    public void StartNewGame()
    {
       
    }
    public GameObject Ending;
    public GameObject delay;
    public bool isEnding = false;
    /// <summary>
    /// ͨ��֮�����
    /// </summary>
    public void Win()
    {
        //һϵ�д���

        Destroy(scenes[0]);
        Destroy(Cat.instance.gameObject);
        Destroy(Dog.instance.gameObject);
        Destroy(MasterController.instance.gameObject);
        Ending.SetActive(true);
        delay.transform.DOMove(new Vector3(0, 0, 0),3).OnComplete(()=> { isEnding = true; });
        // ����Դ�м��س���
        GameObject prefab = Resources.Load<GameObject>("Level");

        // ���Prefab�Ƿ���سɹ�
        if (prefab != null)
        {
            // ʵ����Prefab��������
            GameObject instantiatedObject = Instantiate(prefab);
            Cat.instance.sceneNow = instantiatedObject.GetComponent<Level>();
            scenes[0] = instantiatedObject.GetComponent<Level>();
        }
        //����Сè��С�������˵�λ��

    }
}
