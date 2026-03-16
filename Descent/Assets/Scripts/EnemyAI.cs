using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 1;
    public float damageCooldown = 1f;

    private Transform player;
    private float lastDamageTime;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}