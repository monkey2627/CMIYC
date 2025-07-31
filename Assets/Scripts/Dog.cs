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

    private float timer = 0.0f; // 计时器
    private bool isMoving2Cat = false; // 是否正在移动
    private bool isByCat = false;//是否已经走到小猫旁边并坐下
    public bool isReturning = false; // 是否正在返回窝中
    public bool isDashing = false; // 是否正在冲刺
    private bool isDodging = false; // 是否正在后退
    public Vector3 dashDirection; // 冲刺方向
    public Vector3 dashTarget; // 冲刺方向
    public bool hasCat;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void inter()
    {
        base.inter();
        // 抓挠狗，触发狗的后退和狂吠行为
        DodgeCat();
    }
    // Update is called once per frame
    void Update()
    {
        if (hasCat)
        {
            transform.position += new Vector3(gap.x, 0, gap.z);
        }

        if(isMoving2Cat)
        {
            // 等待计时
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                // 如果等待时间超过 5 秒，返回窝中
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
    Vector3 gap;
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
        gap = -Cat.instance.transform.position + transform.position;
    }
    private void MoveToHome()
    {
        //todo 动画
        // 返回窝中
        transform.position = Vector3.MoveTowards(transform.position, home.position, moveSpeed * Time.deltaTime);

        // 如果到达窝的位置，停止返回
        if (Vector3.Distance(transform.position, home.position) < 0.01f)
        {
            isReturning = false;
        }
    }
    /// <summary>
    /// 小猫叫的时候，向小猫的位置移动
    /// </summary>
    public void MoveToCat()
    {
        isReturning = false;
        isMoving2Cat = true;
        //todo 狗的走路动画
        transform.position = Vector3.MoveTowards(transform.position, Cat.instance.transform.position, moveSpeed * Time.deltaTime);
        // 如果到达小猫位置，停止移动
        if (Vector3.Distance(transform.position, Cat.instance.transform.position) <= Cat.instance.dogLength * 3)
        {
            isMoving2Cat = false;
            timer = 0.0f; // 重置计时器
        }
    }
    /// <summary>
    /// 被猫抓挠/被掉落物击中后会往后躲避，并站定向猫/掉落物狂吠，并吸引主人前来查看
    /// </summary>
    public void Bark()
    {

    }
    /// <summary>
    /// 向喵喵叫时的位置直线跑动并冲出去2个狗长度的距离，随后在终点原地趴坐
    /// (若撞到墙或撞到不可移动的物体，如吧台、床头柜等，则撞完后使其上的物品晃动并掉下来，
    /// 狗自身在撞到其的地
    ///方趴坐;若撞到可以移动的物体，如转椅等，则撞完后使其移动1个狗长度的距离)。“
    /// </summary>
    public void Dash()
    {
        // 向目标方向冲刺
        transform.position += dashDirection * dashSpeed * Time.deltaTime;
         Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f);
         foreach (var collider in colliders)
         {
             if (collider.CompareTag("ObstacleCantMove"))
             {
                isDashing = false;
                collider.gameObject.GetComponent<ItemBase>().DropThings();
                SitDown();
                break;
             }
            else if (collider.CompareTag("ObstacleCanMove"))
            {
                isDashing = false;
                collider.gameObject.GetComponent<ItemBase>().Move(dashDirection);
            }
        }
        // 检查是否冲刺完成
        if (Vector3.Distance(transform.position, dashTarget) < 0.01f)
        {
            isDashing = false;
            SitDown();

        }
    }
    public void SitDown()
    {
        hasCat = false;
        isMoving2Cat = false;
        isReturning = false;
        isDashing = false;
    }
    public void DodgeCat()
    {
        isDodging = true;
        transform.DOMove((transform.position - Cat.instance.transform.position).normalized * Cat.instance.dogLength, 1).OnComplete(() =>
        {
            //todo 面向猫

            Bark();
            isDodging = false;
        });
    }

    public void ResetTimer()
    {
        // 重置计时器
        timer = 0.0f;
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Cat")
        {
            Cat.instance.isOnDog = false;
            hasCat = false;
            SitDown();
        }
    }
}
