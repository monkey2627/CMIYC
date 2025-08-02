using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cabinet : ItemBase
{
    public Vector3[] vectors = new Vector3[3];
    public static cabinet instance;
    private new void Start()
    {
        instance = this;
        vectors = new Vector3[3];
        vectors[0] = new Vector3(0, 22.2f, 0);
        vectors[1] = new Vector3(0, 27.7f, 0);
        vectors[2] = new Vector3(0, 31.6f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        Cat.instance.isInCabinet = true;
        Cat.instance.rb.useGravity = false;
        Cat.instance.rb.isKinematic = true;
        Cat.instance.animator.SetBool("Jump", false);
        Cat.instance.animator.SetBool("Walk", false);
        Cat.instance.animator.SetBool("OnFloor", true);
        if(Cat.instance.transform.position.y>=21 && Cat.instance.transform.position.y <= 25)
        {
            Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, vectors[0].y, Cat.instance.transform.position.z); 
            Cat.instance.cabinetLayer = 0;
        }else if(Cat.instance.transform.position.y > 25&&Cat.instance.transform.position.y <= 29.5)
        {
            Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, vectors[1].y, Cat.instance.transform.position.z);
            Cat.instance.cabinetLayer = 1;
        }
        else if (Cat.instance.transform.position.y > 29.5)
        {
            Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, vectors[2].y, Cat.instance.transform.position.z);
            Cat.instance.cabinetLayer = 2;
        }

    }



}
