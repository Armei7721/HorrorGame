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
    // Start is called before the first frame update
    void Start()
    {
        PRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec3 = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec3 * speed* Time.deltaTime;
    }



}
