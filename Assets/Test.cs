using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    float speed = 10;
    bool isMoving;
    int movedirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // 按下 A 键
        {
            isMoving = true;
            movedirection = -1;
        }
        if (Input.GetKeyDown(KeyCode.D)) // 按下 D 键
        {
            isMoving = true;
            movedirection = 1;
        }
        if (Input.GetKeyUp(KeyCode.A)) // 按下 A 键
        {
            isMoving = false;
        }
        if (Input.GetKeyUp(KeyCode.D)) // 按下 D 键
        {
            isMoving = false;
        }

        if (isMoving)
            transform.position += new Vector3(movedirection, 0, 0) * speed * Time.deltaTime;
    }
}
