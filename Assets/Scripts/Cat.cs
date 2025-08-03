using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public static Cat instance;
    public GameObject MeowIcon;
    public int layerNow;//��־�����������ڲ㼶
    public Level sceneNow;
    public float speed = 1f;
    public float staticTime;
    public float dogLength;
   public Rigidbody rb;
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
    public bool isHiding;
    public int[] layerPlace;
    public GameObject[] herbSprites;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        materialNumber = -1;
        rb = GetComponent<Rigidbody>();
        layerNow = 2;
        transform.position = new Vector3(0, transform.position.y, 0);
        animator = GetComponent<Animator>();
        animator.SetBool("Back", false);
        rb.freezeRotation = true;
        dogLength = Dog.instance.gameObject.GetComponent<BoxCollider>().size.x;
    }
    bool isChangingLayer = false;
    float moveHorizontal = 0.0f;
    bool move = false;
    public int herbSpritesNumber;
    public bool isInCabinet;
    public int cabinetLayer;
    #region ���߼��
    public static bool CheckForWall()
    {
        // ��ȡ Cat.instance.gameObject ��λ��
        Vector3 origin = Cat.instance.gameObject.transform.position;

        // �������ߵķ�����󷽣����� Z �ᣩ
        Vector3 direction = new Vector3(0, 0, 1f);

        // ���ߵ���󳤶�
        float maxDistance = 6f;

        // �洢��ײ��Ϣ
        RaycastHit hit;
        Debug
.DrawRay(origin, direction * maxDistance, Color.red);

        // ��������
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            // �����ײ���������Ƿ���Ϊ "wall"
            if (hit.collider.gameObject.name == "wall")
            {
                return true;
            }
        }

        return false;
    }

    public static bool CheckForWallFront()
    {
        // ��ȡ Cat.instance.gameObject ��λ��
        Vector3 origin = Cat.instance.gameObject.transform.position;

        // �������ߵķ�����󷽣����� Z �ᣩ
        Vector3 direction = new Vector3(0, 0, -1f);

        // ���ߵ���󳤶�
        float maxDistance = 6f;

        // �洢��ײ��Ϣ
        RaycastHit hit;
        Debug
.DrawRay(origin, direction * maxDistance, Color.red);

        // ��������
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            // �����ײ���������Ƿ���Ϊ "wall"
            if (hit.collider.gameObject.name == "wall")
            {
                return true;
            }
        }

        return false;
    }
    public static bool CheckForGuizi()
    {
        // ��ȡ Cat.instance.gameObject ��λ��
        Vector3 origin = Cat.instance.gameObject.transform.position;

        // �������ߵķ�����󷽣����� Z �ᣩ
        Vector3 direction = new Vector3(0,0,1f);

        // ���ߵ���󳤶�
        float maxDistance = 3f;

        // �洢��ײ��Ϣ
        RaycastHit hit;
        // ���ӻ�����

        // ��������
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            //Debug.Log(hit.collider.gameObject.name == "���񱳰�");
            // �����ײ���������Ƿ���Ϊ "guizi"
            if (hit.collider.gameObject.name == "���񱳰�")
            {
                return true;
            }
        }

        return false;
    }
    public static bool CheckForExi()
    {
        // ��ȡ Cat.instance.gameObject ��λ��
        Vector3 origin = Cat.instance.gameObject.transform.position;

        // �������ߵķ�����󷽣����� Z �ᣩ
        Vector3 direction = new Vector3(0, 0, 1f);

        // ���ߵ���󳤶�
        float maxDistance = 3f;

        // �洢��ײ��Ϣ
        RaycastHit hit;
        // ���ӻ�����

        // ��������
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
           
            if (hit.collider.gameObject.name == "չ�񱳰�")
            {
                return true;
            }
        }

        return false;
    }
    #endregion
    public bool isInExihibt;
    public int exihibitLayer;
    public GameObject delay;
    public bool waitingTime;
    public float jumpHigh;
    int unMoveTime = 0;
    private void Update()
    {
        CheckForExi();
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        #region tiaotiaoya Ч��
        tiaotiaoyaCount += Time.deltaTime;
        if (tiaotiaoyaCount < 20)
        {
            maxJumpHeight = jumpHigh * 1.5f;
        }
        else
        {
            maxJumpHeight = jumpHigh;
            bubble.SetActive(false);
        }
        if (tiaotiaoyaCount > 40)
            tiaotiaoyaCount = 40;
        #endregion

        if (!waitingTime)
        {
            isInExihibt = CheckForExi();
            isInCabinet = CheckForGuizi();
        }
        if (!isInCabinet && !isInExihibt)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        #region ��չ����
        if (isInExihibt)
        {
            if (Cat.instance.transform.position.y >= 21.7 && Cat.instance.transform.position.y <= 26.8)
            {
                Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x,Exihibition.instance. vectors[0].y, Cat.instance.transform.position.z);
                Cat.instance.exihibitLayer = 0;
            }
            else if (Cat.instance.transform.position.y > 26.8 && Cat.instance.transform.position.y <= 30.8)
            {
                Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, Exihibition.instance.vectors[1].y, Cat.instance.transform.position.z);
                Cat.instance.exihibitLayer = 1;
            }

            
            if (Input.GetKeyDown(KeyCode.W))
                {

                    if (exihibitLayer < 1)
                    {
                        exihibitLayer++;
                        transform.position = new Vector3(transform.position.x,Exihibition.instance.vectors[exihibitLayer].y, transform.position.z);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (exihibitLayer > 0)
                    {
                        exihibitLayer--;
                        transform.position = new Vector3(transform.position.x,Exihibition.instance.vectors[exihibitLayer].y, transform.position.z);
                    Debug.Log(exihibitLayer + Exihibition.instance.vectors[cabinetLayer].y);
                    }
                    else
                    {
                    rb.isKinematic = false;
                    isInExihibt = false;
                    rb.useGravity = true;
                    waitingTime = true;
                    delay.transform.DOMove(new Vector3(0, 0, 0), 1).OnComplete(()=> { waitingTime = false; });
                    }
                }
            
        }
        #endregion
        #region �ڹ�����
        else if (isInCabinet)
        {
            if (Cat.instance.transform.position.y >= 21 && Cat.instance.transform.position.y <= 25)
            {
                Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, cabinet.instance.vectors[0].y, Cat.instance.transform.position.z);
                Cat.instance.cabinetLayer = 0;
            }
            else if (Cat.instance.transform.position.y > 25 && Cat.instance.transform.position.y <= 29.5)
            {
                Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, cabinet.instance.vectors[1].y, Cat.instance.transform.position.z);
                Cat.instance.cabinetLayer = 1;
            }
            else if (Cat.instance.transform.position.y > 29.5)
            {
                Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, cabinet.instance.vectors[2].y, Cat.instance.transform.position.z);
                Cat.instance.cabinetLayer = 2;
            }
            {
                if (Input.GetKeyDown(KeyCode.W)){

                    if (cabinetLayer < 2)
                    {
                        cabinetLayer++;
                        Debug.Log(cabinetLayer);
                        Debug.Log(transform.position.z);
                        transform.position = new Vector3(transform.position.x, cabinet.instance.vectors[cabinetLayer].y, transform.position.z);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (cabinetLayer > 0)
                    {
                        cabinetLayer--;
                        transform.position = new Vector3(transform.position.x, cabinet.instance.vectors[cabinetLayer].y, transform.position.z);
                    }
                    else
                    {
                        rb.isKinematic = false;
                        isInCabinet = false;
                        rb.useGravity = true;
                        waitingTime = true;
                        delay.transform.DOMove(new Vector3(0, 0, 0), 1).OnComplete(() => { waitingTime = false; });
                    }
                }
            }
        }
        #endregion
        #region �ڻ���
        else if (isOnPicture)
        {
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
            if (transform.position.y <= -36.44)
            {
                Debug.Log("levelPaint");
                isOnPicture = false;
                rb.useGravity = true;
                rb.isKinematic = false;
                animator.SetBool("Slide", false);

            } else if (Input.GetKeyDown(KeyCode.Space) && (!isMeowing && !isScratching)) //����������������֮�������
            {
                animator.SetBool("Slide", false);
                rb.isKinematic = false;
                isOnPicture = false;
                Jump();
            }
        }
        #endregion
        #region jump
        else
        {
            bool IDEL = stateinfo.IsName("IDEL");
            bool SLIDE = stateinfo.IsName("Slide");
            bool Walk = stateinfo.IsName("Walk");
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || isOnDog) &&
            (!isMeowing && !isScratching && !isJumping) && (IDEL || SLIDE || Walk))
            {
                Jump();
            }

        }
        animator.SetFloat("UpDown", rb.velocity.normalized.y);
        #endregion
        #region move
        move = false;            
        if (isOnDog)//�ڹ����ϵ�ʱ��è���������ƶ�
        {
            animator.SetBool("Walk", false);
            transform.position = new Vector3(gap.x + Dog.instance.transform.position.x, transform.position.y, gap.z + Dog.instance.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.W) && stateinfo.IsName("IDEL") && !isInCabinet && !isInExihibt) // ���� W ��
        {
            Debug.Log("yes");
            if (CheckForWall())
                return;
            if (layerNow > 0)
            {
                layerNow--;
                isChangingLayer = true;
                EndHide();
                animator.SetBool("Walk", false);
                if (isOnDog)
                {
                    Dog.instance.layer[0] = (Layer)layerNow;
                    Dog.instance.GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                    Dog.instance.GetComponent<SpriteRenderer>().sortingOrder = 6;
                    Dog.instance.transform.DOMove(new Vector3(Dog.instance.transform.position.x, Dog.instance.transform.position.y, layerPlace[layerNow]), 0.5f).OnComplete(
                     () => { isChangingLayer = false;
                         Dog.instance.transform.GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                         Dog.instance.transform.GetComponent<SpriteRenderer>().sortingOrder = 7;
                         GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                         GetComponent<SpriteRenderer>().sortingOrder = 7;
                         herbSprites[0].GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                         herbSprites[0].GetComponent<SpriteRenderer>().sortingOrder = 7;
                         sceneNow.ChangeLayer((Layer)(layerNow));
                         ChangeLayer();
                     });
                }
                else
                    transform.DOMove(new Vector3(transform.position.x, transform.position.y, layerPlace[layerNow]), 0.5f).OnComplete(
                    () => { isChangingLayer = false; });
                GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                GetComponent<SpriteRenderer>().sortingOrder = 7;
                herbSprites[0].GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                herbSprites[0].GetComponent<SpriteRenderer>().sortingOrder = 7;
                sceneNow.ChangeLayer((Layer)(layerNow));
                ChangeLayer();

            }
        }
        else if (Input.GetKeyDown(KeyCode.S) && stateinfo.IsName("IDEL") && !isInCabinet && !isInExihibt) // ���� S ��
        {
            if (CheckForWallFront())
                return;
            if (layerNow < 2)
            {
                layerNow++;
                EndHide();
                isChangingLayer = true;
                animator.SetBool("Walk", false);
                
                if (isOnDog)
                {
                    Dog.instance.layer[0] = (Layer)layerNow;
                    Dog.instance.GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                    Dog.instance.GetComponent<SpriteRenderer>().sortingOrder = 6;
                    Dog.instance.transform.DOMove(new Vector3(Dog.instance.transform.position.x, Dog.instance.transform.position.y, layerPlace[layerNow]), 0.5f).OnComplete(
                   () => { isChangingLayer = false;
                       Dog.instance.transform.GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                       Dog.instance.transform.GetComponent<SpriteRenderer>().sortingOrder = 7;
                       GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                       GetComponent<SpriteRenderer>().sortingOrder = 7;
                       herbSprites[0].GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                       herbSprites[0].GetComponent<SpriteRenderer>().sortingOrder = 7;
                       sceneNow.ChangeLayer((Layer)(layerNow));
                       ChangeLayer();
                   });
                }
                else
                    transform.DOMove(new Vector3(transform.position.x, transform.position.y, layerPlace[layerNow]), 0.5f).OnComplete(
                    () => { isChangingLayer = false; });
                GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                GetComponent<SpriteRenderer>().sortingOrder = 7;
                herbSprites[0].GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
                herbSprites[0].GetComponent<SpriteRenderer>().sortingOrder = 7;
                ChangeLayer();
                sceneNow.ChangeLayer((Layer)(layerNow));
            }
        }
        else if(!isChangingLayer && !isMeowing && !isScratching && !isOnPicture)
        {
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
            //���������ض�����������
            if (stateinfo.IsName("OnFloor"))
            {
                animator.SetBool("Walk", false);
            }
            else
            {
                //Debug.Log(move);
                if (!isOnDog)
                {
                    if (!move)
                    {
                        unMoveTime++;
                        if(unMoveTime > 5)
                        {
                            animator.SetBool("Walk", move);
                        }
                    }
                    else
                    {   animator.SetBool("Walk", move); 
                        unMoveTime = 0;
                    }
                    
                    
                
                
                }
                else
                    animator.SetBool("Walk", false);
                if (move)
                {
                    Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0);
                    if (movement.x != 0.0f)
                        animator.SetFloat("Horiziontal", movement.x);
                    else
                        animator.SetFloat("WalkDirection", movement.x);
                    Move(movement);
                }
            }
        }
        #endregion
        #region meow
        //��ֹ��û�и����������ʱ��ſ���������
        if (Input.GetKey(KeyCode.Q) && !isOnDog && !isJumping && !isScratching &&( stateinfo.IsName("IDEL") || stateinfo.IsName("Meow") )&& !isInCabinet && !isInExihibt && !isOnPicture && !isHiding)
        {
            isMeowing = true;
            animator.SetBool("Meow", true);
            Meow();
        }
        if (Input.GetKeyUp(KeyCode.Q))//ֹͣ��Q������è�䲻���ˣ���ʱ������
        {
            MeowIcon.SetActive(false);
            isMeowing = false;
            Dog.instance.rb.isKinematic = false;
            if (Dog.instance.isMoving2Cat)
                Dog.instance.gameObject.GetComponent<Animator>().SetBool("Walk", false);
        }
        #endregion
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
            Debug.Log("�����п��Խ���������"+item.name);
        }
        
        if (Input.GetKeyDown(KeyCode.E) && stateinfo.IsName("IDEL") && !isOnPicture)
        {
            if (item)
            {
                item.inter();
                if(item.interType==InterType.Scratch && Mathf.Abs(Dog.instance.transform.position.x-transform.position.x) < dogLength)
                {
                    Dog.instance.Scratch(item);
                }
            }
        }
    }
    public bool isPusing;
    public void Push()
    {
        isPusing = true;
        animator.SetBool("Push", true);
    }
    public void StopPush()
    {
        isPusing = false;
        animator.SetBool("Push", false);
    }
    public GameObject bubble;
    void ChangeLayer()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)layerNow).ToString();
        GetComponent<SpriteRenderer>().sortingOrder = 7;
        herbSprites[0].GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)layerNow).ToString();
        bubble.GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)layerNow).ToString();
        MeowIcon.GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)layerNow).ToString();
        herbSprites[0].GetComponent<SpriteRenderer>().sortingOrder = 7;
        bubble.GetComponent<SpriteRenderer>().sortingOrder = 7;
        MeowIcon.GetComponent<SpriteRenderer>().sortingOrder = 7;
        

    }
    public void GetMaterial(int herb)
    {
        materialNumber = herb;
        herbSprites[herb].SetActive(true); Cat.instance.herbSprites[Cat.instance.herbSpritesNumber].GetComponent<SpriteRenderer>().DOFade(1, 0.3f); herbSpritesNumber = herb; 
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
        isHiding = true;
        layerNow = 2;
        isChangingLayer = true;
        transform.DOMove(new Vector3(transform.position.x, transform.position.y, layerPlace[layerNow]), 0.5f).OnComplete(
                   () => { isChangingLayer = false; });
        ChangeLayer();
        animator.SetBool("Hide", true);
    }
    public void EndHide()
    {
        if (isHiding)
        {
            isHiding = false;
            layerNow = 1;
            isChangingLayer = true;
            transform.DOMove(new Vector3(transform.position.x, transform.position.y, layerPlace[layerNow]), 0.5f).OnComplete(
                       () => { isChangingLayer = false; });
            ChangeLayer();
            animator.SetBool("Hide", false);
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
        EndHide();
        sceneNow.EndObsearve();
        MeowIcon.SetActive(true);
        isMeowing = true;
        animator.SetBool("Meow", true);
        float dis = Mathf.Abs((Dog.instance.transform.position - transform.position).x);
        Debug.Log("Meow" + Dog.instance.transform.position + "   " + transform.position + " " + dis + " "+ 3 * dogLength);
        if (dis > 3 * dogLength)
        {
            if (Dog.instance.isDashing) return;
           // Debug.Log("Move2Cat");
            Dog.instance.MoveToCat();
            Dog.instance.ResetTimer();
        }
        else if (Dog.instance.isReturning)
        {
            Debug.Log("return");
            Dog.instance.isReturning = false;
            Dog.instance.SitDown();
        }
       /* else 
        {
            if (Dog.instance.isDashing) return;
            Debug.Log("Dash");
            Dog.instance.dashDirection =new Vector3((transform.position - Dog.instance.transform.position).x,0,(transform.position - Dog.instance.transform.position).z).normalized;
            Dog.instance.dashTarget = 2 * dogLength;
            Dog.instance.GetComponent<Animator>().SetBool("Run", true);
            Dog.instance.isDashing = true;
            Dog.instance.dashLength = 0;
        }*/

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
        EndHide();
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
        EndHide();
        if(move.normalized.x!=0.0f)
            animator.SetFloat("WalkDirection", move.normalized.x);
        transform.position += move.normalized * speed * Time.deltaTime;
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
            rb.isKinematic = true;
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
