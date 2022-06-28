using UnityEngine;

public class FinalMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _coll;
    private Animator _anim;

    public float speed, jumpForce;
    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump;

    private bool _jumpPressed;
    private int _jumpCount;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _jumpCount > 0) _jumpPressed = true;
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        GroundMovement();
        Jump();
        SwitchAnim();
    }

    private void GroundMovement()
    {
        var horizontalMove = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(horizontalMove * speed, _rb.velocity.y);

        if (horizontalMove != 0) transform.localScale = new Vector3(horizontalMove, 1, 1);
    }

    private void Jump()
    {
        if (isGround)
        {
            _jumpCount = 2;
            isJump = false;
        }

        if (_jumpPressed && isGround)
        {
            isJump = true;
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _jumpCount--;
            _jumpPressed = false;
        }
        else if (_jumpPressed && _jumpCount > 0 && isJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _jumpCount--;
            _jumpPressed = false;
        }
        
    }

    void SwitchAnim()//¶¯»­ÇÐ»»
    {
        _anim.SetFloat("running", Mathf.Abs(_rb.velocity.x));

        if (isGround)
        {
            _anim.SetBool("falling", false);
        }
        else if (!isGround && _rb.velocity.y > 0)
        {
            _anim.SetBool("jumping", true);
        }
        else if (_rb.velocity.y < 0)
        {
            _anim.SetBool("jumping", false);
            _anim.SetBool("falling", true);
        }
    }

}