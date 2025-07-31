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
    public LayerMask groundLayer; // 地板图层
    public Transform groundCheck; // 地板检测点
    public float groundDistance = 0.4f; // 地板检测距离
    public bool isGrounded; // 是否站在地面上
    public bool isOnDog;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 冻结旋转
    }
    private void Update()
    {
        #region move
        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;
        bool move = false;
        if (Input.GetKey(KeyCode.A)) // 按下 A 键
        {
            move = true;
            moveHorizontal = -1.0f; // 向左移动
        }
        else if (Input.GetKey(KeyCode.D)) // 按下 D 键
        {
            move = true;
            moveHorizontal = 1.0f; // 向右移动
        }

        if (Input.GetKey(KeyCode.W)) // 按下 W 键
        {
            move = true;
            moveVertical = 1.0f; // 向前移动
        }
        else if (Input.GetKey(KeyCode.S)) // 按下 S 键
        {
            move = true;
            moveVertical = -1.0f; // 向后移动
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
    /// 静止5s后观察
    /// </summary>
    public void Obsearve()
    {
        sceneNow.Obsearve();
    }
    /// <summary>
    /// 喵喵叫，吸引狗和猫
    /// 1.若和狗的距离大于A，狗往猫咪叫的地方走，直到和猫咪距离小于3狗
    /// 2.若和狗的距离小于A，狗会起身向喵喵叫时的位置直线跑动并冲出去2个狗长度的距离，
    /// 随后在终点原地趴坐(若撞到墙或撞到不可移动的物体，如吧台、床头柜等，则撞完后使其上的物品晃动并掉下来，
    /// 狗自身在撞到其的地
    ///方趴坐;若撞到可以移动的物体，如转椅等，则撞完后使其移动1个狗长度的距离)。
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

    public float jumpForce = 5.0f; // 跳跃力
    public float gravity = -9.81f; // 重力加速度
    public float terminalVelocity = -10.0f; // 最大下落速
    private Vector3 velocity; // 角色的速度
    /// <summary>
    /// 处理跳跃时的逻辑
    /// </summary>
    public void Jump()
    {
        rb.useGravity = true;
        staticTime = 0;
        sceneNow.EndObsearve();
        // 实现跳跃效果
        rb.AddForce(Vector3.up * jumpForce);
    }
    public void Move(Vector3 move)
    {
        staticTime = 0;
        sceneNow.EndObsearve();
        transform.Translate(move * speed * Time.deltaTime);
    }
     // 碰撞检测 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            isOnDog = true;
            Dog.instance.CatJumpOn();
            rb.velocity = Vector3.zero; // 将速度设置为0
            rb.useGravity = false;
        }

    }
}
