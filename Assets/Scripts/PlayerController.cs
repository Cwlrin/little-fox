using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D coll;
    public float speed;
    public float jumpForce;
    public LayerMask ground;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Movement();
        SwichAmim();
    }

    private void Movement()
    {
        // 获取移动参数
        var horizontalMove = Input.GetAxis("Horizontal");
        // 定义调整朝向方向的参数
        var faceDirection = Input.GetAxisRaw("Horizontal");

        // 角色移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(faceDirection));
        }

        // 角色转向
        if (faceDirection != 0) transform.localScale = new Vector3(faceDirection, 1, 1);
        // 角色跳跃
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }
    }

    private void SwichAmim()
    {
        anim.SetBool("idle", false);

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
}