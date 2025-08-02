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
    private void OnTriggerEnter(Collider other)
    {
        if (Cat.instance.isJumping)
        {
            Cat.instance.isOnPicture = true;
            Cat.instance.rb.useGravity = false;
            Cat.instance.isJumping = false;
            Cat.instance.animator.SetBool("Jump", false);
            Cat.instance.animator.SetBool("Walk", false);
            Cat.instance.animator.SetBool("Slide", true);
            Cat.instance.rb.velocity = new Vector3(0, -0.2f, 0); // 重置垂直速度
        }
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
