using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ItemBase
{
    public Sprite closedDoor;
    public Sprite openingDoor;
    public GameObject handle;

    public void ChangeChangeDoorState(bool isOpen)
    {
        if (isOpen)
            this.GetComponent<SpriteRenderer>().sprite = openingDoor;
        else
            this.GetComponent<SpriteRenderer>().sprite = closedDoor;
        
        handle.SetActive(!isOpen);
    }

    private void OnEnable()
    {
        EventHandler.OnChangeDoorState += ChangeChangeDoorState;
    }
    
    private void OnDisable()
    {
        EventHandler.OnChangeDoorState -= ChangeChangeDoorState;
    }
}
