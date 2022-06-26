using UnityEngine;

public class EnemyEagle : Enemy
{
    private Rigidbody2D _rb;
    private float _topY, _bottomY;

    private bool _isUp = true;

    public Transform top, bottom;

    public float speed;

    // @override
    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _topY = top.position.y;
        _bottomY = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (_isUp)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, speed);
            if (transform.position.y > _topY)
            {
                _isUp = false;
            }
        }
        else
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -speed);
            if (transform.position.y < _bottomY) _isUp = true;
        }
    }
}