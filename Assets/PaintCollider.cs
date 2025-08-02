using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Cat.instance.isJumping)
        {
            Debug.Log("paint");
            Cat.instance.isOnPicture = true;
            Cat.instance.rb.useGravity = false;
            Cat.instance.isJumping = false;
            Cat.instance.animator.SetBool("Jump", false);
            Cat.instance.animator.SetBool("Walk", false);
            Cat.instance.animator.SetBool("Slide", true);
            Cat.instance.rb.velocity = new Vector3(0, -0.2f, 0); // 重置垂直速度
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
