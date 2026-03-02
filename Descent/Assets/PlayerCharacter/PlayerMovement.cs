using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed = 5f;

    void Awake()
    {
        if (body == null) body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 move = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)  move.x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) move.x += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)  move.y -= 1;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)    move.y += 1;
        }

        move = move.normalized; // prevents faster diagonal movement
        body.linearVelocity = move * speed;
    }
}
