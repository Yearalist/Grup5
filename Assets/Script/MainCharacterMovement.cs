using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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
    private int collectedObjects = 0;

    // Obje toplama
    public List<GameObject> inventory = new List<GameObject>();
    public Transform objectHoldPoint;

    // Fırlatma hızı
    public float throwForce = 5f;

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

        // Mouse tıklaması ile obje fırlatma
        if (Input.GetMouseButtonDown(0))
        {
            ThrowObject();
        }
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        
     
        if (collision.CompareTag("Collectible")) // Obje toplama
        {
            inventory.Add(collision.gameObject);
            collectedObjects++;
        
            // Rigidbody2D'yi bul ve yerçekimini sıfırla
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0; // Yerçekimi başlangıçta sıfır
                rb.linearVelocity = Vector2.zero; // Hareketi durdur
            }

            collision.gameObject.SetActive(false); // Objeyi sahneden gizle
            UIManager.Instance.UpdateCollectedCount(collectedObjects); // UI güncelle
        }
    }

    void ThrowObject()
    {
        if (inventory.Count > 0)
        {
            GameObject obj = inventory[0];
            inventory.RemoveAt(0); // Envanterden çıkar
            collectedObjects--;

            obj.SetActive(true); // Objeyi sahneye geri getir
            obj.transform.position = objectHoldPoint.position;

            Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
            if (objRb != null)
            {
                // Fırlatma yönünü hesapla
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                Vector2 throwDirection = (mousePosition - objectHoldPoint.position).normalized;

                // Fırlatma hızı uygula
                objRb.linearVelocity = throwDirection * 5f;

                // Yerçekimini geri aç
                objRb.gravityScale = 1;
            }

            // UI'yi güncelle
            UIManager.Instance.UpdateCollectedCount(collectedObjects);
        }
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
