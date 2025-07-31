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

    private float timer = 0.0f; // ��ʱ��
    private bool isMoving2Cat = false; // �Ƿ������ƶ�
    private bool isByCat = false;//�Ƿ��Ѿ��ߵ�Сè�Ա߲�����
    public bool isReturning = false; // �Ƿ����ڷ�������
    public bool isDashing = false; // �Ƿ����ڳ��
    private bool isDodging = false; // �Ƿ����ں���
    public Vector3 dashDirection; // ��̷���
    public Vector3 dashTarget; // ��̷���
    public bool hasCat;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void inter()
    {
        base.inter();
        // ץ�ӹ����������ĺ��˺Ϳ����Ϊ
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
            // �ȴ���ʱ
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                // ����ȴ�ʱ�䳬�� 5 �룬��������
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
    /// ����������״̬�±�è���������ϣ������̽��֮ǰ��һ��״̬������Ҳٿ�
    /// (���������ƶ���������Ծ��
    /// ����Ծʱ��è�Լ��������������WASD �ǲٿ�è��������ԭ��ſ��)��
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
        //todo ����
        // ��������
        transform.position = Vector3.MoveTowards(transform.position, home.position, moveSpeed * Time.deltaTime);

        // ��������ѵ�λ�ã�ֹͣ����
        if (Vector3.Distance(transform.position, home.position) < 0.01f)
        {
            isReturning = false;
        }
    }
    /// <summary>
    /// Сè�е�ʱ����Сè��λ���ƶ�
    /// </summary>
    public void MoveToCat()
    {
        isReturning = false;
        isMoving2Cat = true;
        //todo ������·����
        transform.position = Vector3.MoveTowards(transform.position, Cat.instance.transform.position, moveSpeed * Time.deltaTime);
        // �������Сèλ�ã�ֹͣ�ƶ�
        if (Vector3.Distance(transform.position, Cat.instance.transform.position) <= Cat.instance.dogLength * 3)
        {
            isMoving2Cat = false;
            timer = 0.0f; // ���ü�ʱ��
        }
    }
    /// <summary>
    /// ��èץ��/����������к�������ܣ���վ����è/�������ͣ�����������ǰ���鿴
    /// </summary>
    public void Bark()
    {

    }
    /// <summary>
    /// ��������ʱ��λ��ֱ���ܶ������ȥ2�������ȵľ��룬������յ�ԭ��ſ��
    /// (��ײ��ǽ��ײ�������ƶ������壬���̨����ͷ��ȣ���ײ���ʹ���ϵ���Ʒ�ζ�����������
    /// ��������ײ����ĵ�
    ///��ſ��;��ײ�������ƶ������壬��ת�εȣ���ײ���ʹ���ƶ�1�������ȵľ���)����
    /// </summary>
    public void Dash()
    {
        // ��Ŀ�귽����
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
        // ����Ƿ������
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
            //todo ����è

            Bark();
            isDodging = false;
        });
    }

    public void ResetTimer()
    {
        // ���ü�ʱ��
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
