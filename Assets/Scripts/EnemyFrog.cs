using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool Faceleft = true;

    public Transform leftpoint, rightpoint;
    public float speed;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
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
            if (transform.position.x < leftpoint.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
            if (transform.position.x > rightpoint.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }
}