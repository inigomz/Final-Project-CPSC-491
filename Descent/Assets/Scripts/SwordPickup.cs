using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public int damageBoost = 1;
    public float rangeBoost = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerState playerState = other.GetComponent<PlayerState>();
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();

            if (playerState != null)
            {
                playerState.hasSword = true;
            }

            if (playerAttack != null)
            {
                playerAttack.damage += damageBoost;
                playerAttack.attackRange += rangeBoost;
            }

            Destroy(gameObject);
        }
    }
}