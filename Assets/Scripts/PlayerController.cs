using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
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
        // 获取移动参数
        var horizonalMove = Input.GetAxis("Horizontal");
        // 定义调整朝向方向的参数
        var faceDircetion = Input.GetAxisRaw("Horizontal");

        // 角色移动
        if (horizonalMove != 0)
        {
            rb.velocity = new Vector2(horizonalMove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(faceDircetion));
        }
        // 角色转向
        if (faceDircetion != 0) transform.localScale = new Vector3(faceDircetion, 1, 1);
        // 角色跳跃
        if (Input.GetButtonDown("Jump")) rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
    }
}
