using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    public List<ItemBase> items;
    // Start is called before the first frame update
    void Start()
    {
        FindItems(transform);
    }

    void FindItems(Transform parent)
    {
        // ������ǰ Transform �µ�����������
        foreach (Transform child in parent)
        {
            // ����������Ƿ���� ItemBase �ű�
            ItemBase itemBase = child.GetComponent<ItemBase>();
            if (itemBase != null)
            {
                // ����У���ӵ� List ��
                items.Add(itemBase);
            }

            // �ݹ�����������������
            FindItems(child);
        }
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
            float distance = Mathf.Abs( Cat.instance.transform.position.x- item.transform.position.x);

            // �����ǰ��Ʒ����������������Ʒ
            if (distance < minDistance && item.enable)
            {
                minDistance = distance;
                nearestItem = item;
            }
        }
        Debug.Log(minDistance + " " + nearestItem.gameObject.name);
        if(minDistance < targetDistance && nearestItem.enable)
        {
            return nearestItem;
        }
        else 
            return null; 
    }
    public void Obsearve()
    {
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
