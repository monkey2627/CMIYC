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
        // 遍历当前 Transform 下的所有子物体
        foreach (Transform child in parent)
        {
            // 检查子物体是否带有 ItemBase 脚本
            ItemBase itemBase = child.GetComponent<ItemBase>();
            if (itemBase != null)
            {
                // 如果有，添加到 List 中
                items.Add(itemBase);
            }

            // 递归查找子物体的子物体
            FindItems(child);
        }
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
            float distance = Mathf.Abs( Cat.instance.transform.position.x- item.transform.position.x);

            // 如果当前物品更近，则更新最近物品
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
