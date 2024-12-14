using UnityEngine;

public class AreaController : MonoBehaviour
{
    private Enemy enemyObject;
    void Start()
    {
        enemyObject = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Oyuncu alana girdi ve ben bunu algiladim");
            enemyObject.isPlayerinArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Oyuncu alandan cikti ve ben bunu algiladim");
            enemyObject.isPlayerinArea = false;
        }
    }
}
