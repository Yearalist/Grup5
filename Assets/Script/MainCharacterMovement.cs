using UnityEngine;

public class MainCharacterMovement : MonoBehaviour
{
   
    // Hareket hızları
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // Yer kontrolü için
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        // Rigidbody2D bileşenini al
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Karakteri hareket ettir
        Move();

        // Zıplama kontrolü
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Yer kontrolü
        CheckGround();
    }

    void Move()
    {
        // Yatay eksendeki girdi (A/D veya Sol/Sağ ok tuşları)
        float moveInput = Input.GetAxis("Horizontal");

        // Karakteri hareket ettir
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Karakteri yüzü hareket yönüne çevir
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Jump()
    {
        // Yukarı doğru kuvvet uygula
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void CheckGround()
    {
        // Karakterin yerle temasını kontrol et
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        // Yer kontrolü alanını görselleştir
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
