using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;

    private bool _isHurt;

    public Collider2D coll;
    [Space]
    public float speed;
    public float jumpForce;
    [Space]
    public LayerMask ground;
    public int cherry;

    public Text cherryNum;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!_isHurt)
        {
            Movement();
        }
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
            _rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, _rb.velocity.y);
            _anim.SetFloat("running", Mathf.Abs(faceDirection));
        }

        // 角色转向
        if (faceDirection != 0) transform.localScale = new Vector3(faceDirection, 1, 1);
        // 角色跳跃
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce * Time.deltaTime);
            _anim.SetBool("jumping", true);
        }
    }

    // 动画切换
    private void SwichAmim()
    {
        _anim.SetBool("idle", false);

        if (_anim.GetBool("jumping"))
        {
            if (_rb.velocity.y < 0)
            {
                _anim.SetBool("jumping", false);
                _anim.SetBool("falling", true);
            }
        }
        else if (_isHurt)
        {
            _anim.SetBool("hurt",true);
            _anim.SetFloat("running",0);
            if (Mathf.Abs(_rb.velocity.x) < 0.1)
            {
                _anim.SetBool("hurt",false);
                _anim.SetBool("idle",true);
                _isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            _anim.SetBool("falling", false);
            _anim.SetBool("idle", true);
        }
    }

    // 收集物品
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Collection")
        {
            Destroy(collider.gameObject);
            cherry += 1;
            cherryNum.text = cherry.ToString();
        }
    }

    // 消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (_anim.GetBool("falling"))
            {
                Destroy(collision.gameObject);
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce * Time.deltaTime);
                _anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                _rb.velocity = new Vector2(-5, _rb.velocity.y);
                _isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                _rb.velocity = new Vector2(5, _rb.velocity.y);
                _isHurt = true;
            }
        }
    }
}