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
       //aimLine.startWidth = 0.05f; // 라인 두께 설정
       // aimLine.endWidth = 0.05f; // 라인 두께 설정

        maincamera.transform.SetParent(cameraposition, false);
        //maincamera.transform.localPosition = Vector3.zero;
        //maincamera.transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //  MousePoint();
        Debug.Log(vAxis + "vAxis의 속도");
    }

    private void Move()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec3 = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec3 * speed * Time.deltaTime;

        Vector3 currentRotation = transform.rotation.eulerAngles;

        // 수평 입력 (Horizontal)에 따라 회전을 조절합니다.
        currentRotation.y += hAxis * rotationSpeed ;

        // 새로운 회전값을 적용합니다.
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
        // 마우스 포인터의 위치를 가져옴
        Vector3 mousePosition = Input.mousePosition;

        // 화면 상의 점을 월드 좌표로 변환
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // 레이캐스트로 쏠 위치 설정
        if (Physics.Raycast(ray, out hit, fireRange))
        {
            Vector3 targetPosition = hit.point;

            // 라인 렌더러로 조준 위치 시각적으로 나타냄
            aimLine.SetPosition(0, gunMuzzle.position);
            aimLine.SetPosition(1, targetPosition);

            // 마우스 왼쪽 버튼 클릭으로 발사 동작 구현
            if (Input.GetMouseButtonDown(0))
            {
                FireBullet(targetPosition);
            }
        }
        else
        {
            // 레이가 어떠한 물체에도 부딪치지 않을 경우 기본 거리로 설정
            Vector3 defaultTarget = ray.GetPoint(fireRange);
            aimLine.SetPosition(0, gunMuzzle.position);
            aimLine.SetPosition(1, defaultTarget);
        }
    }
    void FireBullet(Vector3 target)
    {
        // 총알 발사 동작을 구현
        // target 위치로 총알을 발사하는 동작을 추가할 수 있습니다.
        // 총알 발사, 피격 효과, 피격 음향 등을 처리합니다.
    }
}
