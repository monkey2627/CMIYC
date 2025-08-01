using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : ItemBase
{
    public FishTank fishTank;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void DropThings()
    {
        base.DropThings();
        fishTank.DropFromDesk();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
