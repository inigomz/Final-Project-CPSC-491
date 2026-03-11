using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed = 5f;

    private Animator animator;
    private SpriteRenderer sprite;

    void Awake()
    {
        if (body == null)
            body = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector2 move = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) move.x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) move.x += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) move.y -= 1;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) move.y += 1;
        }

        move = move.normalized;

        body.linearVelocity = move * speed;

        if (animator != null)
        {
            animator.SetFloat("Speed", move.magnitude);
        }

        // Flip sprite left/right
        if (move.x < 0)
        {
            sprite.flipX = true;
        }
        else if (move.x > 0)
        {
            sprite.flipX = false;
        }
    }
}