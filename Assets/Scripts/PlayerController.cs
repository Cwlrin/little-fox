using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;

    private bool _isHurt;
    private bool _isGround;
    private bool _isJump;
    private bool _jumpPressed;
    private int _extraJump;

    public Transform cellingCheck, groundCheck;
    public Collider2D coll;
    public Collider2D disColl;
    [Space] public float speed;
    public float jumpForce;
    [Space] public LayerMask ground;
    [Space] public Text cherryNum;
    [SerializeField] private int _cherry;

    [Space] public AudioSource jumpAudio;
    public AudioSource hurtAudio;
    public AudioSource cherryAudio;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!_isHurt) Movement();
        //Jump();
        SwichAmim();
        
        NewJump();
    }

    private void Update()
    {
        _isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        cherryNum.text = _cherry.ToString();
        if (Input.GetButtonDown("Jump") && _extraJump > 0) _jumpPressed = true;
        Crouch();
    }

    // 移动
    private void Movement()
    {
        // 获取移动参数
        var horizontalMove = Input.GetAxis("Horizontal");
        // 定义调整朝向方向的参数
        var faceDirection = Input.GetAxisRaw("Horizontal");

        // 角色移动
        if (horizontalMove != 0)
        {
            _rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, _rb.velocity.y);
            _anim.SetFloat("running", Mathf.Abs(faceDirection));
        }

        // 角色转向
        if (faceDirection != 0) transform.localScale = new Vector3(faceDirection, 1, 1);
    }

    // void Jump()
    // {   
    //     if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
    //     {
    //         _rb.velocity = new Vector2(_rb.velocity.x, jumpForce * Time.fixedDeltaTime);
    //         jumpAudio.Play();
    //         _anim.SetBool("jumping", true);
    //     }
    // }

    private void NewJump()
    {
        if (_isGround)
        {
            _extraJump = 2;
            _isJump = false;
        }

        if (_jumpPressed && _isGround)
        {
            _isJump = true;
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _extraJump--;
            _anim.SetBool("jumping", true);
            _jumpPressed = false;
        }
        else if (_jumpPressed && _extraJump > 0 && !_isGround)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _extraJump--;
            _anim.SetBool("jumping", true);
            _jumpPressed = false;
        }
    }

    // 动画切换
    private void SwichAmim()
    {
        //_anim.SetBool("idle", false);

        // 当角色空中掉落时，切换为下落动画
        if (_rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground)) _anim.SetBool("falling", true);
        // 当角色跳跃时
        if (_anim.GetBool("jumping"))
        {
            if (_rb.velocity.y < 0)
            {
                _anim.SetBool("jumping", false);
                _anim.SetBool("falling", true);
            }
        }
        // 当角色受到伤害时
        else if (_isHurt)
        {
            _anim.SetBool("hurt", true);
            _anim.SetFloat("running", 0);
            if (Mathf.Abs(_rb.velocity.x) < 0.1)
            {
                _anim.SetBool("hurt", false);
                //_anim.SetBool("idle", true);
                _isHurt = false;
            }
        }
        // 当角色接触到地面时
        else if (coll.IsTouchingLayers(ground))
        {
            _anim.SetBool("falling", false);
            //_anim.SetBool("idle", true);
        }
    }

    // 碰撞触发器
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 碰撞樱桃
        if (collider.CompareTag("Collection"))
        {
            cherryAudio.Play();
            //Destroy(collider.gameObject);
            //_cherry += 1;
            collider.GetComponent<Animator>().Play("IsGet");
            //cherryNum.text = _cherry.ToString();
        }

        if (collider.CompareTag("DeadLine"))
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke(nameof(ReStart), 2f);
        }
    }

    // 消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            // 下落时碰撞
            if (_anim.GetBool("falling"))
            {
                enemy.JumpOn();
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce * Time.deltaTime);
                _anim.SetBool("jumping", true);
            }
            // 受伤
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                _rb.velocity = new Vector2(-5, _rb.velocity.y);
                hurtAudio.Play();
                _isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                _rb.velocity = new Vector2(5, _rb.velocity.y);
                hurtAudio.Play();
                _isHurt = true;
            }
        }
    }

    // 判断趴下
    private void Crouch()
    {
        if (Input.GetButton("Crouch"))
        {
            _anim.SetBool("crouching", true);
            disColl.enabled = false;
        }
        else if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, ground))
        {
            _anim.SetBool("crouching", false);
            disColl.enabled = true;
        }
    }

    public void CherryCount()
    {
        _cherry++;
    }

    private void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}