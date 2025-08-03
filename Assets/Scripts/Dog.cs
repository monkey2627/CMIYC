using UnityEngine;
using DG.Tweening;
public class Dog : ItemBase
{
    public static Dog instance;
    public float moveSpeed = 5.0f; // �ƶ��ٶ�
    public float dashSpeed = 10.0f; // ����ٶ�
    public float dodgeDistance = 2.0f; // ���˾���
    public float waitTime = 5.0f; // �ȴ�ʱ��
    public Transform home; // ���ѵ�λ��
    public Rigidbody rb; // ���� Rigidbody ���
    public GameObject delay;
    private float timer = 0.0f; // ��ʱ��
    public bool isMoving2Cat = false; // �Ƿ������ƶ�
    private bool isByCat = false;//�Ƿ��Ѿ��ߵ�Сè�Ա߲�����
    public bool isReturning = false; // �Ƿ����ڷ�������
    public bool isDashing = false; // �Ƿ����ڳ��
    private bool isDodging = false; // �Ƿ����ں���
    public Vector3 dashDirection; // ��̷���
    public float dashTarget; 
    public bool hasCat;
    public bool seePendant;//�Ƿ��ڿ�����
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
        // ץ�ӹ����������ĺ��˺Ϳ����Ϊ
        DodgeCat();
    }
    public float speed = 5;
    // Update is called once per frame
    public Vector3 lastPos;
    void Update()
    {
        Debug.Log(rb.velocity);
        if (hasCat && !Cat.instance.isJumping)
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
    /// ��ץ����item������
    /// </summary>
    public void Scratch(ItemBase item)
    {
        //1.����item�������ߵ���Ӧλ��
        animator.SetBool("Walk",true);
        Vector3 movement = new Vector3(item.position.x-transform.position.x, transform.position.y, item.position.z-transform.position.z);
        animator.SetFloat("WalkDirection", movement.x);
        transform.DOMove(transform.position + movement, 1).OnComplete(()=> {
            //ֹͣ��·
            animator.SetBool("Walk", false);
            //
            item.ScratchByDog();
            //��������
            AttentionEvent attentionEvent = new AttentionEvent(transform, AttentionEventType.DogDestruction);
            EventHandler.ItemBroke(item);
            EventHandler.AttentionEventHappen(attentionEvent);
        });
    }
    public Vector3 gap;
    /// <summary>
    /// ����������״̬�±�è���������ϣ������̽��֮ǰ��һ��״̬������Ҳٿ�
    /// (���������ƶ���������Ծ��
    /// ����Ծʱ��è�Լ��������������WASD �ǲٿ�è��������ԭ��ſ��)��
    /// </summary>
    public void CatJumpOn()
    {
        hasCat = true;
        animator.SetBool("HasCat", true);
        isMoving2Cat = false;
        isReturning = false;
        isDashing = false;
    }
    public void CatJumpOut()
    {
        hasCat = false;
        animator.SetBool("Walk", false);
        animator.SetBool("HasCat", false);
    }
    private void MoveToHome()
    {
        if (layer[0] != ((Layer)2))
        {
            layer[0] = ((Layer)2);
            GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)2).ToString();
            GetComponent<SpriteRenderer>().sortingOrder = 6;
            transform.DOMove(new Vector3(transform.position.x, transform.position.y, 5), 0.1f);
        }
        //todo ����
        animator.SetBool("Walk",true);
        Vector3 movement = new Vector3((home.position - transform.position).normalized.x * moveSpeed * Time.deltaTime, 0, 0);
        // ��������
        transform.position += movement;

        // ��������ѵ�λ�ã�ֹͣ����
        if (Vector3.Distance(transform.position, home.position) < 0.5f)
        {
            animator.SetBool("Walk", false);
            isReturning = false;
        }
    }
    /// <summary>
    /// Сè�е�ʱ����Сè��λ���ƶ�
    /// </summary>
    public void MoveToCat()
    {
        if (layer[0] != ((Layer)Cat.instance.layerNow))
        {
            layer[0] = ((Layer)Cat.instance.layerNow);
            GetComponent<SpriteRenderer>().sortingLayerName = ((Layer)Cat.instance.layerNow).ToString();
            GetComponent<SpriteRenderer>().sortingOrder = 6;
            int[] a = { 10, 5, 0 };
            transform.DOMove(new Vector3(transform.position.x, transform.position.y, a[(int)layer[0]]), 0.1f);
        }
        rb.isKinematic = true;
        isReturning = false;
        isMoving2Cat = true;
        timer = 0.0f;
        Vector3 movement = new Vector3((Cat.instance.transform.position - transform.position).normalized.x * moveSpeed * Time.deltaTime, 0, 0);
        transform.position += movement;
        Debug.Log(movement);
        animator.SetBool("Walk", true);
        animator.SetFloat("WalkDirection", (-transform.position + Cat.instance.transform.position).normalized.x);
        // �������Сèλ�ã�ֹͣ�ƶ�
        if (Mathf.Abs((Cat.instance.transform.position - transform.position).x) <= Cat.instance.dogLength * 3)
        {
            isMoving2Cat = false;
            Debug.Log("����");
            animator.SetBool("Walk", false);
            timer = 0.0f; // ���ü�ʱ��
        }
    }
    /// <summary>
    /// ��èץ��/����������к�������ܣ���վ����è/�������ͣ�����������ǰ���鿴,ֻ�ᱻ��һ�����������
    /// </summary>
    public void Bark()
    {
        AttentionEvent attentionEvent = new AttentionEvent(transform, AttentionEventType.DogBark);
        EventHandler.AttentionEventHappen(attentionEvent);
    }
    public float dashLength;
    /// <summary>
    /// ��������ʱ��λ��ֱ���ܶ������ȥ2�������ȵľ��룬������յ�ԭ��ſ��
    /// (��ײ��ǽ��ײ�������ƶ������壬���̨����ͷ��ȣ���ײ���ʹ���ϵ���Ʒ�ζ�����������
    /// ��������ײ����ĵ�
    ///��ſ��;��ײ�������ƶ������壬��ת�εȣ���ײ���ʹ���ƶ�1�������ȵľ���)����
    /// </summary>
    public void Dash()
    {
        // ��Ŀ�귽����
         Vector3 gap = dashDirection * dashSpeed * Time.deltaTime;
         dashLength +=  gap.magnitude;
         transform.position += gap;
         isDashing = true;
         animator.SetFloat("WalkDirection", gap.normalized.x);
         animator.SetFloat("Horiziontal", gap.normalized.x);
        // ����Ƿ������
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
    /// ��è��ץ��֮���˺�Ȼ���
    /// </summary>
    public void DodgeCat()
    {
        isDodging = true;
        Vector3 movement = new Vector3((transform.position - Cat.instance.transform.position).x,0, (transform.position - Cat.instance.transform.position).z);
        transform.DOMove(transform.position+ movement.normalized * Cat.instance.dogLength, 1).OnComplete(() =>
        {
            //todo ����è
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
    /// ������Ʒ����
    /// </summary>
    /// <param name="itemTarget"></param>
    public void Dodge(Vector3 itemTarget)
    {
        isDodging = true;
        Vector3 movement = new Vector3((transform.position - itemTarget).x, 0, (transform.position - itemTarget).z);
        transform.DOMove(transform.position + movement.normalized * Cat.instance.dogLength, 1).OnComplete(() =>
        {
            //todo ������Ʒ
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
        // ���ü�ʱ��
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
    /// �ߵ����ƴ�Ȼ��
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
