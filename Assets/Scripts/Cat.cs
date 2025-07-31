using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public static Cat instance;
    public SceneManager sceneNow;
    float speed = 0.1f;
    float staticTime;
    public float dogLength;
    Rigidbody rb;
    public LayerMask groundLayer; // �ذ�ͼ��
    public Transform groundCheck; // �ذ����
    public float groundDistance = 0.4f; // �ذ������
    public bool isGrounded; // �Ƿ�վ�ڵ�����
    public bool isOnDog;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ������ת
    }
    private void Update()
    {
        #region move
        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;
        bool move = false;
        if (Input.GetKey(KeyCode.A)) // ���� A ��
        {
            move = true;
            moveHorizontal = -1.0f; // �����ƶ�
        }
        else if (Input.GetKey(KeyCode.D)) // ���� D ��
        {
            move = true;
            moveHorizontal = 1.0f; // �����ƶ�
        }

        if (Input.GetKey(KeyCode.W)) // ���� W ��
        {
            move = true;
            moveVertical = 1.0f; // ��ǰ�ƶ�
        }
        else if (Input.GetKey(KeyCode.S)) // ���� S ��
        {
            move = true;
            moveVertical = -1.0f; // ����ƶ�
        }
        if (move)
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            Move(movement);
        }
        #region jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        if (Input.GetKey(KeyCode.Space) && (isGrounded || isOnDog))
        {
            Jump();
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Meow();
        }
        staticTime += Time.deltaTime;
        if(staticTime > 5f)
        {
            Obsearve();        
        }
        ItemBase item = sceneNow.Detect();
        if (Input.GetKey(KeyCode.E))
        {
            if (item)
            {
                item.inter();
            }
        }
        #endregion
    }
    /// <summary>
    /// ��ֹ5s��۲�
    /// </summary>
    public void Obsearve()
    {
        sceneNow.Obsearve();
    }
    /// <summary>
    /// �����У���������è
    /// 1.���͹��ľ������A������è��еĵط��ߣ�ֱ����è�����С��3��
    /// 2.���͹��ľ���С��A������������������ʱ��λ��ֱ���ܶ������ȥ2�������ȵľ��룬
    /// ������յ�ԭ��ſ��(��ײ��ǽ��ײ�������ƶ������壬���̨����ͷ��ȣ���ײ���ʹ���ϵ���Ʒ�ζ�����������
    /// ��������ײ����ĵ�
    ///��ſ��;��ײ�������ƶ������壬��ת�εȣ���ײ���ʹ���ƶ�1�������ȵľ���)��
    /// </summary>
    public void Meow()
    {
        staticTime = 0;
        sceneNow.EndObsearve();
        if ((Dog.instance.transform.position - transform.position).magnitude > 3 * dogLength)
        {
            Dog.instance.MoveToCat();
            Dog.instance.ResetTimer();
        }
        else if (Dog.instance.isReturning)
        {
            Dog.instance.isReturning = false;
            Dog.instance.SitDown();
        }
        else
        {
            Dog.instance.dashDirection = (transform.position - Dog.instance.transform.position).normalized;
            Dog.instance.dashTarget = Dog.instance.transform.position + Dog.instance.dashDirection * 2 * dogLength; ;
            Dog.instance.isDashing = true;
        }

    }

    public float jumpForce = 5.0f; // ��Ծ��
    public float gravity = -9.81f; // �������ٶ�
    public float terminalVelocity = -10.0f; // ���������
    private Vector3 velocity; // ��ɫ���ٶ�
    /// <summary>
    /// ������Ծʱ���߼�
    /// </summary>
    public void Jump()
    {
        rb.useGravity = true;
        staticTime = 0;
        sceneNow.EndObsearve();
        // ʵ����ԾЧ��
        rb.AddForce(Vector3.up * jumpForce);
    }
    public void Move(Vector3 move)
    {
        staticTime = 0;
        sceneNow.EndObsearve();
        transform.Translate(move * speed * Time.deltaTime);
    }
     // ��ײ��� 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            isOnDog = true;
            Dog.instance.CatJumpOn();
            rb.velocity = Vector3.zero; // ���ٶ�����Ϊ0
            rb.useGravity = false;
        }

    }
}
