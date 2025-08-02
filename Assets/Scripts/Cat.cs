using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public static Cat instance;
    public int Layer;//��־�����������ڲ㼶
    public SceneBase sceneNow;
    public float speed = 1f;
    public float staticTime;
    public float dogLength;
    Rigidbody rb;
    public Animator animator;
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
        animator.SetBool("Back", false);
        rb.freezeRotation = true;
        dogLength = Dog.instance.gameObject.GetComponent<BoxCollider>().size.x;
    }
    private void Update()
    {
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        #region tiaotiaoya Ч��
        tiaotiaoyaCount += Time.deltaTime;
        if (tiaotiaoyaCount < 20)
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
            if (transform.position.y <= -36.44)
            {
                Debug.Log("levelPaint");
                isOnPicture = false;
                rb.useGravity = true;
                rb.isKinematic = false;
                animator.SetBool("Slide", false);

            } else if (Input.GetKey(KeyCode.Space) && (!isMeowing && !isScratching)) //����������������֮�������
            {
                animator.SetBool("Slide", false);
                rb.isKinematic = false;
                isOnPicture = false;
                Jump();
            }
        }
        else
        {
            bool IDEL = stateinfo.IsName("IDEL");
            bool SLIDE = stateinfo.IsName("Slide");
            bool Walk = stateinfo.IsName("Walk");
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            if (Input.GetKey(KeyCode.Space) && (isGrounded || isOnDog) &&
            (!isMeowing && !isScratching && !isJumping) && (IDEL || SLIDE || Walk))
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
        else if (!isMeowing && !isScratching && !isOnPicture)
        {
            //���������ض�����������

            if (stateinfo.IsName("OnFloor"))
            {
                animator.SetBool("Walk", false);
            }
            else
            {
                animator.SetBool("Walk", move);
                if (move)
                {
                    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                    if (movement.x != 0.0f)
                        animator.SetFloat("Horiziontal", movement.x);
                    else
                        animator.SetFloat("WalkDirection", movement.x);
                    Move(movement);
                }
            }
        }
        #endregion
        //��ֹ��û�и����������ʱ��ſ���������
        if (Input.GetKey(KeyCode.Q) && !isOnDog && !isJumping && !isScratching && stateinfo.IsName("IDEL"))
        {
            isMeowing = true;
            animator.SetBool("Meow", true);
            Meow();
        }
        if (Input.GetKeyUp(KeyCode.Q))//ֹͣ��Q������è�䲻���ˣ���ʱ������
        {
            isMeowing = false;
            if (Dog.instance.isMoving2Cat)
                Dog.instance.gameObject.GetComponent<Animator>().SetBool("Walk", false);
        }
        staticTime += Time.deltaTime;
        if (staticTime > 5f)
        {
            Obsearve();
        }
        else  
            sceneNow.EndObsearve(); 
        ItemBase item = sceneNow.Detect();
        if(item)
        {
            item.Show();
        }
        if (Input.GetKey(KeyCode.E) && stateinfo.IsName("IDEL"))
        {
            if (item)
            {
                item.inter();
                if(item.interType==InterType.Scratch && (Dog.instance.transform.position-transform.position).magnitude < dogLength)
                {
                    Dog.instance.Scratch(item);
                }
            }
        }
    }
    /// <summary>
    /// è���Ӷ���
    /// </summary>
    public void Scratch()
    {
        Cat.instance.animator.SetBool("Scratch", true);
        isScratching = true;
    }
    public void EndScratch()
    {
        animator.SetBool("Scratch", false);
    }

    /// <summary>
    /// è�俪��
    /// </summary>
    public void OpenDoor()
    {
        //1��è�䱳��
        Back();
        //2.�ſ��Ķ���

        //3.UI��ʾ�Ѿ���ȥ��
    }

    /// <summary>
    /// è����
    /// </summary>
    public void Back()
    {
        animator.SetBool("Back", true);
    }
    /// <summary>
    /// è�������
    /// </summary>
    public void Hide()
    {
        animator.SetBool("Hide", true);
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
        isMeowing = true;
        animator.SetBool("Meow", true);
        if ((Dog.instance.transform.position - transform.position).magnitude > 3 * dogLength)
        {
            if (Dog.instance.isDashing) return;
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
            Dog.instance.GetComponent<Animator>().SetBool("Run", true);
            Dog.instance.isDashing = true;
            Dog.instance.dashLength = 0;
        }

    }
    /// <summary>
    /// ������ж��������
    /// </summary>
    public void FinishMeow()
    {
        if(!isMeowing)
            animator.SetBool("Meow", false);
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
        rb.useGravity = true;
        staticTime = 0;
        sceneNow.EndObsearve();
        isJumping = true;
        animator.SetBool("Jump", true);
        animator.SetBool("OnFloor", false);
        animator.SetBool("EndJump", false);
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
        animator.SetBool("Hide", false);
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
            animator.SetBool("OnFloor", true);
            Paint.instance.inTime = 0;
        }
        if(collision.gameObject.tag == "Paint" && isJumping)
        {
            if(isOnPicture==false && Paint.instance.inTime==0)
            {
                isOnPicture = true;
                rb.useGravity = false;
                Paint.instance.inTime++;
                isJumping = false;
                rb.isKinematic = true;
                animator.SetBool("Jump", false);
                animator.SetBool("Walk", false);
                animator.SetBool("ForceEnd", true);
                animator.SetBool("Slide", true);
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
        isJumping = false;
        animator.SetBool("Jump", false);
        animator.SetBool("Walk", false);
        animator.SetBool("EndJump",true);
    }
    private void OnCollisionExit(Collision collision)
    {
      
    }
    private void OnCollisionStay(Collision collision)
    {
       
    }
   

}
