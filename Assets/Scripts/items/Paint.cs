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
        if (interactCnt >= 0)
        {
            TipPopManager.instance.ShowTip(
                "You’ve always hated this painting. How dare your LOWLY SERVANT forget who the true master is—not even " +
                "a single PORTRAIT of you in this house?! Time to teach her what real art looks like…… Meow~");
        }
        else
        {
            TipPopManager.instance.ShowTip("Now THIS is real art! Meow~");
        }
        interactCnt++;
    }
    
    private void OnTriggerEnter(Collider other)
    {
       
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        Cat.instance.animator.SetBool("Slide", false);
        Cat.instance.rb.useGravity = true;
        Cat.instance.rb.isKinematic = false;
        Cat.instance.isOnPicture = false;
    }
}
