using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStove : ItemBase
{
    public GameManager ganguo;
    public bool enable = true;
    public override void inter()
    {
        base.inter();
        if (!enable) return;
        //πÿ±’»º∆¯‘Ó

        //€·€ˆÕ£÷π√∞—Ã

        enable = false;

    }
    public override void Show()
    {
        if(enable)
        base.Show();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
