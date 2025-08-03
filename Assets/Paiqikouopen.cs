using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paiqikouopen :ItemBase
{
    
    // Start is called before the first frame update
    void Start()
    {
        
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
