using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    public ItemBase[] items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float targetDistance = 3f;
    /// <summary>
    /// ������è������Ǹ��ɽ�����Ʒ
    /// </summary>
    public ItemBase Detect()
    {
        
        ItemBase nearestItem = null;
        float minDistance = Mathf.Infinity; // ��ʼ��Ϊ�����

        // �������пɽ�����Ʒ
        foreach (var item in items)
        {
            if (item == null)
            {
                Debug.LogWarning("�ɽ�����Ʒ�������п��");
                continue;
            }

            // ����è�뵱ǰ��Ʒ�ľ���
            float distance = Vector3.Distance(Cat.instance.transform.position, item.transform.position);

            // �����ǰ��Ʒ����������������Ʒ
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestItem = item;
            }
        }
        if(minDistance < targetDistance)
        {
            return nearestItem;
        }
        else { return null; }
    }
    public void Obsearve()
    {
        Debug.Log("Obsearve");
        Cat.instance.staticTime = 0;
        foreach (var item in items)
        {
              item.Show();
        }  
    }
    public void EndObsearve()
    {
        foreach (var item in items)
        {
            item.Hide();
        }

    }
}
