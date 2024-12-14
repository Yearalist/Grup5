using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    private float opacityValue = 0.5f;

    private float speed = 0.1f;

    private Vector3 _enemyPos;
    private Vector3 _enemyPointA;
    private Vector3 _enemyPointB;

    private SpriteRenderer spriteRenderer;

    public bool isPlayerinArea;

    public Transform player;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        _enemyPos = gameObject.transform.position;
        _enemyPointA = _enemyPos + Vector3.left * 3f;
        _enemyPointB = _enemyPos + Vector3.right * 3f;

        //Debug.Log(_enemyPointA + "a");
        //Debug.Log(_enemyPointB + "b");
    }


    void Update()
    {
        if (!isPlayerinArea)
        {
            EnemyPatrolMovement();
        }

        if (isPlayerinArea)
        {
            EnemytoPlayerMovement();
        }
    }

    private void EnemyPatrolMovement()
    {
        transform.position = Vector3.Lerp(_enemyPointA, _enemyPointB, Mathf.PingPong(Time.time * speed, 1));
    }

    public void EnemytoPlayerMovement()
    {
        //Debug.Log(player.position+"playerpos");
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Düşman, player ile çarpıştı!");
            EnemyChangeOpacity(0.1f);
        }

        if (collision.collider.CompareTag("LightBall"))
        {
            Debug.Log("Düşman, LightBall ile çarpıştı!");
            Destroy(collision.gameObject);
            EnemyChangeOpacity(0.1f);
        }
    }

    private void EnemyChangeOpacity(float alphaValue)
    {
        opacityValue += alphaValue; // Opaklık değerini doğrudan ayarla
        SetOpacity(opacityValue);
    }

    private void SetOpacity(float alphaValue)
    {
        Color color = spriteRenderer.color;
        color.a = alphaValue; // Alpha değerini doğrudan ayarla
        spriteRenderer.color = color;
        if (opacityValue >= 1)
        {
            Destroy(gameObject);
        }
    }
}