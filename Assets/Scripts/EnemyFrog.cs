using UnityEngine;

public class EnemyFrog : Enemy
{
    private Rigidbody2D _rb;
    private bool _faceLeft = true;
    private Collider2D _coll;
    private float _leftX, _rightX;

    //private Animator _anim;

    public Transform leftPoint, rightPoint;
    public float speed, jumpForce;
    public LayerMask ground;

    // @override
    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        //_anim = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        _leftX = leftPoint.position.x;
        _rightX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        SwitchAnim();
    }

    private void Movement()
    {
        if (_faceLeft)
        {
            if (_coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                _rb.velocity = new Vector2(-speed, jumpForce);
            }

            if (transform.position.x < _leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _faceLeft = false;
            }
        }
        else
        {
            if (_coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                _rb.velocity = new Vector2(speed, jumpForce);
            }

            if (transform.position.x > _rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _faceLeft = true;
            }
        }
    }

    private void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
            if (_rb.velocity.y < 0.1)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }

        if (_coll.IsTouchingLayers(ground) && anim.GetBool("falling")) anim.SetBool("falling", false);
    }
}