using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            enterDialog.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            enterDialog.SetActive(false);
        }
    }
}
