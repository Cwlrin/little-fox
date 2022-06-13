using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // ��ȡ�ƶ�����
        var horizonalMove = Input.GetAxis("Horizontal");
        // �������������Ĳ���
        var faceDircetion = Input.GetAxisRaw("Horizontal");

        // �����ƶ�
        if (horizonalMove != 0) rb.velocity = new Vector2(horizonalMove * speed * Time.deltaTime, rb.velocity.y);
        // ת��
        if (faceDircetion != 0) transform.localScale = new Vector3(faceDircetion, 1, 1);
        // ��Ծ
        if (Input.GetButtonDown("Jump")) rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
    }
}
