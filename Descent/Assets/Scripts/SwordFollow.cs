using UnityEngine;

public class SwordFollow : MonoBehaviour
{
    public PlayerState playerState;

    private bool facingRight = true;
    private SpriteRenderer swordRenderer;

    void Start()
    {
        if (playerState == null)
        {
            playerState = GetComponentInParent<PlayerState>();
        }

        swordRenderer = GetComponent<SpriteRenderer>();

        if (swordRenderer != null)
        {
            swordRenderer.enabled = false;
            swordRenderer.sortingOrder = 1;
        }

        UpdateSwordVisual();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        if (moveX > 0)
        {
            facingRight = true;
            UpdateSwordVisual();
        }
        else if (moveX < 0)
        {
            facingRight = false;
            UpdateSwordVisual();
        }

        if (playerState != null && swordRenderer != null)
        {
            swordRenderer.enabled = playerState.hasSword;
        }
    }

    void UpdateSwordVisual()
    {
        if (facingRight)
        {
            transform.localPosition = new Vector3(0.18f, 0.02f, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.localRotation = Quaternion.Euler(0f, 0f, -25f);
        }
        else
        {
            transform.localPosition = new Vector3(-0.18f, 0.02f, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 25f);
        }
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }
}