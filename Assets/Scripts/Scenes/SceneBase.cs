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
    /// 查找离猫最近的那个可交互物品
    /// </summary>
    public ItemBase Detect()
    {
        
        ItemBase nearestItem = null;
        float minDistance = Mathf.Infinity; // 初始化为无穷大

        // 遍历所有可交互物品
        foreach (var item in items)
        {
            if (item == null)
            {
                Debug.LogWarning("可交互物品数组中有空项！");
                continue;
            }

            // 计算猫与当前物品的距离
            float distance = Vector3.Distance(Cat.instance.transform.position, item.transform.position);

            // 如果当前物品更近，则更新最近物品
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
