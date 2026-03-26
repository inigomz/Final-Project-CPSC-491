using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 1f;
    public float attackCooldown = 0.4f;

    private float lastAttackTime;
    private PlayerState playerState;
    private Animator animator;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) &&
            Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        if (playerState != null && playerState.hasSword && animator != null)
        {
            animator.SetTrigger("Attack");
        }

        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();

        foreach (EnemyHealth enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= attackRange)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}