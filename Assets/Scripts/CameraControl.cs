using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x, 0, -10f);
    }
}