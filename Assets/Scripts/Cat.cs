using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public static Cat instance;
    public GameObject MeowIcon;
    public int layerNow;//标志主角现在所在层级
    public Level sceneNow;
    public float speed = 1f;
    public float staticTime;
    public float dogLength;
   public Rigidbody rb;
    public Animator animator;
    public LayerMask groundLayer; // 地板图层
    public Transform groundCheck; // 地板检测点
    public float groundDistance = 0.4f; // 地板检测距离
    public bool isGrounded; // 是否站在地面上
    public bool isOnDog;
    public bool isMeowing = false;
    public bool isScratching = false;
    public int materialNumber = -1;//此时嘴里的材料序号，没有时为-1
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
    #region 射线检查
    public static bool CheckForWall()
    {
        // 获取 Cat.instance.gameObject 的位置
        Vector3 origin = Cat.instance.gameObject.transform.position;

        // 定义射线的方向（向后方，即负 Z 轴）
        Vector3 direction = new Vector3(0, 0, 1f);

        // 射线的最大长度
        float maxDistance = 6f;

        // 存储碰撞信息
        RaycastHit hit;
        Debug
.DrawRay(origin, direction * maxDistance, Color.red);

        // 发射射线
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            // 检查碰撞到的物体是否名为 "wall"
            if (hit.collider.gameObject.name == "wall")
            {
                return true;
            }
        }

        return false;
    }

    public static bool CheckForWallFront()
    {
        // 获取 Cat.instance.gameObject 的位置
        Vector3 origin = Cat.instance.gameObject.transform.position;

        // 定义射线的方向（向后方，即负 Z 轴）
        Vector3 direction = new Vector3(0, 0, -1f);

        // 射线的最大长度
        float maxDistance = 6f;

        // 存储碰撞信息
        RaycastHit hit;
        Debug
.DrawRay(origin, direction * maxDistance, Color.red);

        // 发射射线
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            // 检查碰撞到的物体是否名为 "wall"
            if (hit.collider.gameObject.name == "wall")
            {
                return true;
            }
        }

        return false;
    }
    public static bool CheckForGuizi()
    {
        // 获取 Cat.instance.gameObject 的位置
        Vector3 origin = Cat.instance.gameObject.transform.position;

        // 定义射线的方向（向后方，即负 Z 轴）
        Vector3 direction = new Vector3(0,0,1f);

        // 射线的最大长度
        float maxDistance = 3f;

        // 存储碰撞信息
        RaycastHit hit;
        // 可视化射线

        // 发射射线
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            //Debug.Log(hit.collider.gameObject.name == "橱柜背板");
            // 检查碰撞到的物体是否名为 "guizi"
            if (hit.collider.gameObject.name == "橱柜背板")
            {
                return true;
            }
        }

        return false;
    }
    public static bool CheckForExi()
    {
        // 获取 Cat.instance.gameObject 的位置
        Vector3 origin = Cat.instance.gameObject.transform.position;

        // 定义射线的方向（向后方，即负 Z 轴）
        Vector3 direction = new Vector3(0, 0, 1f);

        // 射线的最大长度
        float maxDistance = 3f;

        // 存储碰撞信息
        RaycastHit hit;
        // 可视化射线

        // 发射射线
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
           
            if (hit.collider.gameObject.name == "展柜背板")
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
        #region tiaotiaoya 效果
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
        #region 在展柜上
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
        #region 在柜子上
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
        #region 在画上
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

            } else if (Input.GetKeyDown(KeyCode.Space) && (!isMeowing && !isScratching)) //不允许二段跳，落地之后才能跳
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
        if (isOnDog)//在狗狗上的时候猫不动，狗移动
        {
            animator.SetBool("Walk", false);
            transform.position = new Vector3(gap.x + Dog.instance.transform.position.x, transform.position.y, gap.z + Dog.instance.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.W) && stateinfo.IsName("IDEL") && !isInCabinet && !isInExihibt) // 按下 W 键
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
        else if (Input.GetKeyDown(KeyCode.S) && stateinfo.IsName("IDEL") && !isInCabinet && !isInExihibt) // 按下 S 键
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
            //如果正在落地动作，不允许动
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
        //静止且没有干其他事情的时候才可以喵喵叫
        if (Input.GetKey(KeyCode.Q) && !isOnDog && !isJumping && !isScratching &&( stateinfo.IsName("IDEL") || stateinfo.IsName("Meow") )&& !isInCabinet && !isInExihibt && !isOnPicture && !isHiding)
        {
            isMeowing = true;
            animator.SetBool("Meow", true);
            Meow();
        }
        if (Input.GetKeyUp(KeyCode.Q))//停止按Q键代表猫咪不叫了，此时狗不走
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
            Debug.Log("附近有可以交互的物体"+item.name);
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
    /// 猫咪挠东西
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
    /// 猫咪开门
    /// </summary>
    public void OpenDoor()
    {
        //1。猫咪背身
        Back();
        //2.门开的动画

        //3.UI提示已经出去了
    }

    /// <summary>
    /// 猫背身
    /// </summary>
    public void Back()
    {
        animator.SetBool("Back", true);
    }
    /// <summary>
    /// 猫咪藏起来
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
    /// 播放完叫动画后调用
    /// </summary>
    public void FinishMeow()
    {
        if(!isMeowing)
            animator.SetBool("Meow", false);
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
        EndHide();
        if(move.normalized.x!=0.0f)
            animator.SetFloat("WalkDirection", move.normalized.x);
        transform.position += move.normalized * speed * Time.deltaTime;
    }
    Vector3 gap;
    Collision lastCollision;
    // 碰撞检测 
    public bool isOnPicture = false; // 是否在画上
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
            rb.velocity = Vector3.zero; // 将速度设置为0
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
    /// 小猫跳跃落地动画结束时调用
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
