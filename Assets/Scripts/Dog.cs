using UnityEngine;
using DG.Tweening;
public class Dog : ItemBase
{
    public static Dog instance;
    public float moveSpeed = 5.0f; // 移动速度
    public float dashSpeed = 10.0f; // 冲刺速度
    public float dodgeDistance = 2.0f; // 后退距离
    public float waitTime = 5.0f; // 等待时间
    public Transform home; // 狗窝的位置
    public Rigidbody rb; // 狗的 Rigidbody 组件
    public GameObject delay;
    private float timer = 0.0f; // 计时器
    public bool isMoving2Cat = false; // 是否正在移动
    private bool isByCat = false;//是否已经走到小猫旁边并坐下
    public bool isReturning = false; // 是否正在返回窝中
    public bool isDashing = false; // 是否正在冲刺
    private bool isDodging = false; // 是否正在后退
    public Vector3 dashDirection; // 冲刺方向
    public float dashTarget; 
    public bool hasCat;
    public bool seePendant;//是否在看吊灯
    public Animator animator;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
        rb = GetComponent<Rigidbody>();
    }

    public override void inter()
    {
        base.inter();
        // 抓挠狗，触发狗的后退和狂吠行为
        DodgeCat();
    }
    public float speed = 5;
    // Update is called once per frame
    public Vector3 lastPos;
    void Update()
    {
        if (hasCat && !Cat.instance.isJumping)
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
            else
            {
                animator.SetBool("Walk", move);
                if (move)
                {
                    Cat.instance.jumpCount = 0;
                    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                    animator.SetFloat("Horiziontal", movement.x);
                    Cat.instance.gameObject.GetComponent<Animator>().SetFloat("WalkDirection", movement.normalized.x);
                    animator.SetFloat("WalkDirection", movement.normalized.x);
                    lastPos = transform.position;
                    if(!isWall)
                    transform.Translate(movement * speed * Time.deltaTime);
                }
            }
            #endregion

        }
        if (isMoving2Cat)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                isMoving2Cat = false;
                isReturning = true;
            }
        }
        if (isReturning)
        {
            MoveToHome();
        }
        if (isDashing)
        {
            Dash();
        }
    }
    /// <summary>
    /// 狗抓挠吸item引主人
    /// </summary>
    public void Scratch(ItemBase item)
    {
        //1.根据item的种类走到相应位置
        animator.SetBool("Walk",true);
        Vector3 movement = new Vector3(item.position.x-transform.position.x, transform.position.y, item.position.z-transform.position.z);
        animator.SetFloat("WalkDirection", movement.x);
        transform.DOMove(transform.position + movement, 1).OnComplete(()=> {
            //停止走路
            animator.SetBool("walk", false);
            //
            item.ScratchByDog();
            //吸引主人
            AttentionEvent attentionEvent = new AttentionEvent(transform, AttentionEventType.DogDestruction);
            EventHandler.AttentionEventHappen(attentionEvent);
        });
    }
    public Vector3 gap;
    /// <summary>
    /// 若狗在任意状态下被猫跳到了身上，则立刻解除之前的一切状态并受玩家操控
    /// (可以左右移动但不能跳跃，
    /// 点跳跃时仅猫自己跳，且起跳后的WASD 是操控猫，狗会在原地趴坐)。
    /// </summary>
    public void CatJumpOn()
    {
        hasCat = true;
        isMoving2Cat = false;
        isReturning = false;
        isDashing = false;
    }
    public void CatJumpOut()
    {
        hasCat = false;
        animator.SetBool("Walk", false);
    }
    private void MoveToHome()
    {
        //todo 动画
        animator.SetBool("Walk",true);
        // 返回窝中
        transform.position = Vector3.MoveTowards(transform.position, home.position, moveSpeed * Time.deltaTime);

        // 如果到达窝的位置，停止返回
        if (Vector3.Distance(transform.position, home.position) < 0.01f)
        {
            animator.SetBool("Walk", false);
            isReturning = false;
        }
    }
    /// <summary>
    /// 小猫叫的时候，向小猫的位置移动
    /// </summary>
    public void MoveToCat()
    {
        layer[0] = ((Layer)Cat.instance.layerNow);
        GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
        GetComponent<SpriteRenderer>().sortingOrder = 6;
        int[] a = { 10, 5, 0 };
        transform.DOMove(new Vector3(transform.position.x, transform.position.y, a[(int)layer[0]]), 0.1f);
        isReturning = false;
        isMoving2Cat = true;
        timer = 0.0f;
        //todo 狗的走路动画
        transform.position += new Vector3((-transform.position+Cat.instance.transform.position).x,0, (-transform.position + Cat.instance.transform.position).z).normalized * moveSpeed * Time.deltaTime;
        animator.SetBool("Walk", true);
        animator.SetFloat("WalkDirection", (-transform.position + Cat.instance.transform.position).normalized.x);
        Debug.Log(Mathf.Abs(Cat.instance.transform.position.x - Dog.instance.position.x));
        // 如果到达小猫位置，停止移动
        if (Mathf.Abs(Cat.instance.transform.position.x - Dog.instance.position.x) <= Cat.instance.dogLength * 3)
        {
            isMoving2Cat = false;
            animator.SetBool("Walk", false);
            timer = 0.0f; // 重置计时器
        }
    }
    /// <summary>
    /// 被猫抓挠/被掉落物击中后会往后躲避，并站定向猫/掉落物狂吠，并吸引主人前来查看,只会被这一层的物体砸中
    /// </summary>
    public void Bark()
    {
        AttentionEvent attentionEvent = new AttentionEvent(transform, AttentionEventType.DogBark);
        EventHandler.AttentionEventHappen(attentionEvent);
    }
    public float dashLength;
    /// <summary>
    /// 向喵喵叫时的位置直线跑动并冲出去2个狗长度的距离，随后在终点原地趴坐
    /// (若撞到墙或撞到不可移动的物体，如吧台、床头柜等，则撞完后使其上的物品晃动并掉下来，
    /// 狗自身在撞到其的地
    ///方趴坐;若撞到可以移动的物体，如转椅等，则撞完后使其移动1个狗长度的距离)。“
    /// </summary>
    public void Dash()
    {
        // 向目标方向冲刺
         Vector3 gap = dashDirection * dashSpeed * Time.deltaTime;
         dashLength +=  gap.magnitude;
         transform.position += gap;
         isDashing = true;
         animator.SetFloat("WalkDirection", gap.normalized.x);
         animator.SetFloat("Horiziontal", gap.normalized.x);
        // 检查是否冲刺完成
        if (dashLength>dashTarget)
        {
            isDashing = false;
            SitDown();
            Cat.instance.isMeowing = false;
            Cat.instance.gameObject.GetComponent<Animator>().SetBool("Meow", false);
        }
    }
    public void SitDown()
    {
        hasCat = false;
        isMoving2Cat = false;
        isReturning = false;
        isDashing = false;
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
    }
    /// <summary>
    /// 被猫咪抓挠之后，退后然后叫
    /// </summary>
    public void DodgeCat()
    {
        isDodging = true;
        Vector3 movement = new Vector3((transform.position - Cat.instance.transform.position).x,0, (transform.position - Cat.instance.transform.position).z);
        transform.DOMove(transform.position+ movement.normalized * Cat.instance.dogLength, 1).OnComplete(() =>
        {
            //todo 面向猫
            if((-transform.position + Cat.instance.transform.position).x > 0)
            {
                animator.SetFloat("WalkDirection", 1);
            }
            else
            {
                animator.SetFloat("WalkDirection", -1);
            }
            Bark();
            isDodging = false;
        });
    }
    /// <summary>
    /// 狗被物品砸中
    /// </summary>
    /// <param name="itemTarget"></param>
    public void Dodge(Vector3 itemTarget)
    {
        isDodging = true;
        Vector3 movement = new Vector3((transform.position - itemTarget).x, 0, (transform.position - itemTarget).z);
        transform.DOMove(transform.position + movement.normalized * Cat.instance.dogLength, 1).OnComplete(() =>
        {
            //todo 面向物品
            if ((-transform.position + itemTarget).x > 0)
            {
                animator.SetFloat("WalkDirection", 1);
            }
            else
            {
                animator.SetFloat("WalkDirection", -1);
            }
            Bark();
            isDodging = false;
        });
    }
    public void ResetTimer()
    {
        // 重置计时器
        timer = 0.0f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObstacleCantMove"))
        {
            isDashing = false;
            collision.gameObject.GetComponent<ItemBase>().DropThings();
            SitDown();
        }
        else if (collision.gameObject.CompareTag("ObstacleCanMove"))
        {
            isDashing = false;
            collision.gameObject.GetComponent<ItemBase>().Move(dashDirection);
        }

    }
    public bool isWall;
    public void OnCollisionExit(Collision collision)
    {
        isWall = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
    public GameObject left;
    public GameObject right;
    public bool IsInLivingRoom()
    {
        if(transform.position.x <= right.transform.position.x && transform.position.z >=
            left.transform.position.z)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 走到吊灯处然后看
    /// </summary>
    public void Move2Pendant()
    {
        seePendant = true;
        transform.DOMove(new Vector3(0, 0, 0), 2).OnComplete(() => {
            delay.transform.DOMove(new Vector3(0, 0, 0), 3).OnComplete(()=> {
                seePendant = false;
            });
        });
    }
}
