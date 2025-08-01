using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : ItemBase
{
    public static Paint instance;
    public int inTime=0;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void inter()
    {
        base.inter();
        TipPopManager.instance.ShowTip("Painting");
    }
}
