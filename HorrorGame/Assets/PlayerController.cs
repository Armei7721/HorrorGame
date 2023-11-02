using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float hAxis;
    float vAxis;
    Vector3 moveVec3;
    public Rigidbody PRigidbody;
    public int speed =5;
    public float rotationSpeed = 5;
    public Animator animator;

    public LineRenderer aimLine;
    public Transform gunMuzzle;
    public float fireRange = 100f;

    public Camera maincamera;
    public Transform cameraposition;

    bool runbool = false;
    // Start is called before the first frame update
    void Start()
    {
        PRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
       // aimLine = GetComponent<LineRenderer>();
       //aimLine.startWidth = 0.05f; // ���� �β� ����
       // aimLine.endWidth = 0.05f; // ���� �β� ����

        maincamera.transform.SetParent(cameraposition, false);
        //maincamera.transform.localPosition = Vector3.zero;
        //maincamera.transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //  MousePoint();
        Debug.Log(vAxis + "vAxis�� �ӵ�");
    }

    private void Move()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec3 = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec3 * speed * Time.deltaTime;

        Vector3 currentRotation = transform.rotation.eulerAngles;

        // ���� �Է� (Horizontal)�� ���� ȸ���� �����մϴ�.
        currentRotation.y += hAxis * rotationSpeed ;

        // ���ο� ȸ������ �����մϴ�.
        transform.rotation = Quaternion.Euler(currentRotation);

        if (vAxis != 0)
        {
            runbool = true;
            animator.SetBool("Run", runbool);
        }
        else
        {
            runbool = false;
            animator.SetBool("Run", runbool);
        }
    }

    private void MousePoint()
    {
        // ���콺 �������� ��ġ�� ������
        Vector3 mousePosition = Input.mousePosition;

        // ȭ�� ���� ���� ���� ��ǥ�� ��ȯ
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // ����ĳ��Ʈ�� �� ��ġ ����
        if (Physics.Raycast(ray, out hit, fireRange))
        {
            Vector3 targetPosition = hit.point;

            // ���� �������� ���� ��ġ �ð������� ��Ÿ��
            aimLine.SetPosition(0, gunMuzzle.position);
            aimLine.SetPosition(1, targetPosition);

            // ���콺 ���� ��ư Ŭ������ �߻� ���� ����
            if (Input.GetMouseButtonDown(0))
            {
                FireBullet(targetPosition);
            }
        }
        else
        {
            // ���̰� ��� ��ü���� �ε�ġ�� ���� ��� �⺻ �Ÿ��� ����
            Vector3 defaultTarget = ray.GetPoint(fireRange);
            aimLine.SetPosition(0, gunMuzzle.position);
            aimLine.SetPosition(1, defaultTarget);
        }
    }
    void FireBullet(Vector3 target)
    {
        // �Ѿ� �߻� ������ ����
        // target ��ġ�� �Ѿ��� �߻��ϴ� ������ �߰��� �� �ֽ��ϴ�.
        // �Ѿ� �߻�, �ǰ� ȿ��, �ǰ� ���� ���� ó���մϴ�.
    }
}
