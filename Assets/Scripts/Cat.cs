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
    public LayerMask groundLayer; // �ذ�ͼ��
    public Transform groundCheck; // �ذ����
    public float groundDistance = 0.4f; // �ذ������
    public bool isGrounded; // �Ƿ�վ�ڵ�����
    public bool isOnDog;
    public bool isMeowing = false;
    public bool isScratching = false;
    public int materialNumber = -1;//��ʱ����Ĳ�����ţ�û��ʱΪ-1
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
        #region tiaotiaoya Ч��
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

                if (Input.GetKey(KeyCode.Space) && (!isMeowing && !isScratching)) //����������������֮�������
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
        if (isOnDog)//�ڹ����ϵ�ʱ��è���������ƶ�
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
        Debug.Log("��Χ�пɻ���������" + item.gameObject.name);
        if (Input.GetKey(KeyCode.E))
        {
            if (item)
            {
                item.inter();
            }
        }
    }

    /// <summary>
    /// �Ʋ�������Ӳ��һС��ʱ��
    /// </summary>
    public void PlayBall()
    {

    }
    /// <summary>
    /// ������������������Ӳ��һС��ʱ��
    /// </summary>
    public void PlayFishTank()
    {

    }
    /// <summary>
    /// ҧס����ѿ
    /// </summary>
    public void FetchTiaoTiaoYa()
    {

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
            if (Dog.instance.isDashing) return;
            Dog.instance.dashDirection =new Vector3((transform.position - Dog.instance.transform.position).x,0,(transform.position - Dog.instance.transform.position).z).normalized;
            Dog.instance.dashTarget = 2 * dogLength; 
            Dog.instance.isDashing = true;
            Dog.instance.dashLength = 0;
        }

    }

    public float jumpForce = 5.0f; // ��Ծ��
    public bool isJumping = false;
    public int jumpCount = 0;
    public  float maxJumpHeight = 6.0f; // ������߶�
    /// <summary>
    /// ������Ծʱ���߼�
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
        // ʵ����ԾЧ��
        // ������Ҫ�Ĵ�ֱ�ٶ��Դﵽ������߶�
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
    // ��ײ��� 
    public bool isOnPicture = false; // �Ƿ��ڻ���
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
            rb.velocity = Vector3.zero; // ���ٶ�����Ϊ0
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
                rb.velocity = new Vector3(0,-0.2f,0); // ���ô�ֱ�ٶ�
                
            }
        }

    }
    private bool finishOnFloorAni=true;
    /// <summary>
    /// Сè��Ծ��ض�������ʱ����
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
    /// ������ж��������
    /// </summary>
    public void FinishMeow()
    {
        isMeowing = false;
        animator.SetBool("Meow", false);
    }
}
