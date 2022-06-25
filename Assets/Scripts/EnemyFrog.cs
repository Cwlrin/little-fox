using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool Faceleft = true;
    private Collider2D _coll;
    private float _leftX, _rightX;

    private Animator _anim;

    public Transform leftpoint, rightpoint;
    public float speed, jumpForce;
    public LayerMask ground;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        _leftX = leftpoint.position.x;
        _rightX = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        SwitchAnim();
    }

    private void Movement()
    {
        if (Faceleft)
        {
            if (_coll.IsTouchingLayers(ground))
            {
                _anim.SetBool("jumping", true);
                _rb.velocity = new Vector2(-speed, jumpForce);
            }

            if (transform.position.x < _leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (_coll.IsTouchingLayers(ground))
            {
                _anim.SetBool("jumping", true);
                _rb.velocity = new Vector2(speed, jumpForce);
            }

            if (transform.position.x > _rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    private void SwitchAnim()
    {
        if (_anim.GetBool("jumping"))
            if (_rb.velocity.y < 0.1)
            {
                _anim.SetBool("jumping", false);
                _anim.SetBool("falling", true);
            }

        if (_coll.IsTouchingLayers(ground) && _anim.GetBool("falling")) _anim.SetBool("falling", false);
    }
}