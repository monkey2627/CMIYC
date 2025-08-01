using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public static Cat instance;
    public SceneBase sceneNow;
    public float speed = 1f;
    public float staticTime;
    public float dogLength;
    Rigidbody rb;
    Animator animator;
    public LayerMask groundLayer; // 地板图层
    public Transform groundCheck; // 地板检测点
    public float groundDistance = 0.4f; // 地板检测距离
    public bool isGrounded; // 是否站在地面上
    public bool isOnDog;
    public bool isMeowing = false;
    public bool isScratching = false;
    public int materialNumber = -1;//此时嘴里的材料序号，没有时为-1
    public float tiaotiaoyaCount = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        dogLength = Dog.instance.gameObject.GetComponent<BoxCollider>().size.x;
    }
    private void Update()
    {
        #region tiaotiaoya 效果
        tiaotiaoyaCount += Time.deltaTime;
        if(tiaotiaoyaCount < 20)
        {
            maxJumpHeight = 9;
        }
        else
        {
            maxJumpHeight = 6;
        }
        if (tiaotiaoyaCount > 40)
            tiaotiaoyaCount = 40;
        #endregion
        #region jump
            if (isOnPicture)
            {
                transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
                if(transform.position.y <= -36.44)
                {
                
                        Debug.Log("levelPaint");
                        isOnPicture = false;
                        rb.useGravity = true;
                        rb.isKinematic = false;
                
                }else

                if (Input.GetKey(KeyCode.Space) && (!isMeowing && !isScratching)) //不允许二段跳，落地之后才能跳
                {
                    rb.isKinematic = false;
                    isOnPicture = false;
                    Jump();
                }
            }
            else
            {
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
                if (Input.GetKey(KeyCode.Space) && (isGrounded || isOnDog) && 
                (!isMeowing && !isScratching && !isJumping) && finishOnFloorAni) 
                {
                    Jump();
                }
               
            }
            animator.SetFloat("UpDown", rb.velocity.normalized.y);
            #endregion
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
        if (isOnDog)//在狗狗上的时候猫不动，狗移动
        {
            animator.SetBool("Walk", false);
            transform.position = new Vector3(gap.x + Dog.instance.transform.position.x, transform.position.y, gap.z + Dog.instance.transform.position.z);
        }
        else if(!isMeowing && !isScratching && !isOnPicture)
        {

            animator.SetBool("Walk", move);
            if (move)
            {
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                if(movement.x!=0.0f)
                animator.SetFloat("Horiziontal", movement.x);
                else
                animator.SetFloat("WalkDirection", movement.x);
                Move(movement);
            }
        }
      
        if (Input.GetKey(KeyCode.Q) && !isOnDog && !isJumping && !isScratching)
        {
            isMeowing = true;
            animator.SetBool("Meow", true);
            Meow();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
           Dog.instance.gameObject.GetComponent<Animator>().SetBool("Walk", false);
        }
        staticTime += Time.deltaTime;
        if(staticTime > 5f)
        {
            Obsearve();        
        }

        #endregion        
        ItemBase item = sceneNow.Detect();
        if(item)
        Debug.Log("周围有可互动的物体" + item.gameObject.name);
        if (Input.GetKey(KeyCode.E))
        {
            if (item)
            {
                item.inter();
            }
        }
    }

    /// <summary>
    /// 推不动，被硬控一小段时间
    /// </summary>
    public void PlayBall()
    {

    }
    /// <summary>
    /// 被鱼缸里的鱼吸引，被硬控一小段时间
    /// </summary>
    public void PlayFishTank()
    {

    }
    /// <summary>
    /// 咬住跳跳芽
    /// </summary>
    public void FetchTiaoTiaoYa()
    {

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
            if (Dog.instance.isDashing) return;
            Dog.instance.dashDirection =new Vector3((transform.position - Dog.instance.transform.position).x,0,(transform.position - Dog.instance.transform.position).z).normalized;
            Dog.instance.dashTarget = 2 * dogLength; 
            Dog.instance.isDashing = true;
            Dog.instance.dashLength = 0;
        }

    }

    public float jumpForce = 5.0f; // 跳跃力
    public bool isJumping = false;
    public int jumpCount = 0;
    public  float maxJumpHeight = 6.0f; // 最大弹跳高度
    /// <summary>
    /// 处理跳跃时的逻辑
    /// </summary>
    public void Jump()
    {
        Debug.Log("jump");
        finishOnFloorAni = false;
        animator.SetBool("True", false);
        rb.useGravity = true;
        staticTime = 0;
        sceneNow.EndObsearve();
        isJumping = true;
        animator.SetBool("Jump", true);
        animator.SetBool("OnFloor", false);
        isOnDog = false;
        Dog.instance.CatJumpOut();
        // 实现跳跃效果
        // 计算需要的垂直速度以达到最大弹跳高度
        float requiredVelocity = Mathf.Sqrt(2 * Physics.gravity.magnitude * maxJumpHeight);
        rb.isKinematic = false;
        rb.velocity = new Vector3(rb.velocity.x, requiredVelocity, rb.velocity.z);
    }
    public void Move(Vector3 move)
    {
        jumpCount = 0;
        staticTime = 0;
        sceneNow.EndObsearve();
        if(move.normalized.x!=0.0f)
        animator.SetFloat("WalkDirection", move.normalized.x);
        transform.Translate(move * speed * Time.deltaTime);
    }
    Vector3 gap;
    Collision lastCollision;
    // 碰撞检测 
    public bool isOnPicture = false; // 是否在画上
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag == "Paint");
        lastCollision = collision;
        if (collision.gameObject.tag == "Dog" && isJumping && !isMeowing)
        {
            isOnDog = true;
            isJumping = false;
            isGrounded = true;
            Debug.Log("yes");
            Debug.Log(collision.gameObject.name);
            Debug.Log(collision.gameObject.tag);
            Dog.instance.CatJumpOn();
            rb.velocity = Vector3.zero; // 将速度设置为0
            gap = transform.position - Dog.instance.transform.position;
            animator.SetBool("Jump", false);
            animator.SetBool("Walk", false);
            animator.SetBool("OnFloor", true);
            Paint.instance.inTime = 0;
        }
        if (collision.gameObject.tag == "Floor" && !isMeowing)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Walk", false);
            animator.SetBool("OnFloor", true);
            Paint.instance.inTime = 0;
        }
        if(collision.gameObject.tag == "Paint" && isJumping)
        {
            Debug.Log(isOnPicture);
            if(isOnPicture==false && Paint.instance.inTime==0)
            {
                Debug.Log("inter paint");

                isOnPicture = true;
                rb.useGravity = false;
                Paint.instance.inTime++;
                isJumping = false;
                rb.isKinematic = true;
                animator.SetBool("Jump", false);
                animator.SetBool("Walk", false);
                animator.SetBool("ForceEnd", true);
                rb.velocity = new Vector3(0,-0.2f,0); // 重置垂直速度
                
            }
        }

    }
    private bool finishOnFloorAni=true;
    /// <summary>
    /// 小猫跳跃落地动画结束时调用
    /// </summary>
    public void OnFloor()
    {
        finishOnFloorAni = true;
        isJumping = false;
        animator.SetBool("Jump", false);
        animator.SetBool("OnFloor",true);
        jumpCount = 0;
        animator.SetBool("ForceEnd", false);
        animator.SetBool("True", true);
    }
    private void OnCollisionExit(Collision collision)
    {
      
    }
    private void OnCollisionStay(Collision collision)
    {
       
    }
    /// <summary>
    /// 播放完叫动画后调用
    /// </summary>
    public void FinishMeow()
    {
        isMeowing = false;
        animator.SetBool("Meow", false);
    }
}
