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
        if (Input.GetKeyDown(KeyCode.A)) // ���� A ��
        {
            isMoving = true;
            movedirection = -1;
        }
        if (Input.GetKeyDown(KeyCode.D)) // ���� D ��
        {
            isMoving = true;
            movedirection = 1;
        }
        if (Input.GetKeyUp(KeyCode.A)) // ���� A ��
        {
            isMoving = false;
        }
        if (Input.GetKeyUp(KeyCode.D)) // ���� D ��
        {
            isMoving = false;
        }

        if (isMoving)
            transform.position += new Vector3(movedirection, 0, 0) * speed * Time.deltaTime;
    }
}
