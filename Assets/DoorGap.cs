using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGap : ItemBase
{
    public static DoorGap instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public override void inter()
    {
        base.inter();
        GameManager.instance.Win();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
