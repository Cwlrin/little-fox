using UnityEngine;

public class Prarllax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    public bool lockY;

    private float _startPointX;
    private float _startPointY;

    private void Start()
    {
        _startPointX = transform.position.x;
    }

    private void Update()
    {
        if (lockY)
            transform.position = new Vector2(_startPointX + cam.position.x * moveRate, transform.position.y);
        else
            transform.position = new Vector2(_startPointX + cam.position.x * moveRate,
                _startPointY + cam.position.y * moveRate);
    }
}