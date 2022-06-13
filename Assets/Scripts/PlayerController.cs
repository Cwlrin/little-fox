using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizonalMove;

        // ��ȡ�ƶ�����
        horizonalMove = Input.GetAxis("Horizontal");
        // �����ƶ�
        if (horizonalMove != 0)
        {
            rb.velocity = new Vector2(horizonalMove * speed, rb.velocity.y);
        }
    }
}
