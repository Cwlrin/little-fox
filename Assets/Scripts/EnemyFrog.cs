using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool Faceleft = true;
    private float _leftX, _rightX;

    public Transform leftpoint, rightpoint;
    public float speed;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        _leftX = leftpoint.position.x;
        _rightX = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Faceleft)
        {
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);
            if (transform.position.x < _leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
            if (transform.position.x > _rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }
}