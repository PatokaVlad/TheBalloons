using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MoveableObject
{
    private readonly int[] directions = { -1, 1 };

    private int currentDirection;

    private bool hasDirection = false;

    protected override void ChooseDirection()
    {
        currentDirection = directions[Random.Range(0, 2)];
        hasDirection = true;

        if (currentDirection != 1)
        {
            FlipSprite(false);
        }
    }

    protected override void MoveObject()
    {
        if (!hasDirection)
        {
            ChooseDirection();
        }

        Vector2 moveVector = Vector2.right * currentDirection * speed * Time.deltaTime;
        moveVector += (Vector2)_transform.position;
        _transform.position = moveVector;

        TouchHandle();
    }

    private void FlipSprite(bool playSound)
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;

        if (playSound)
        {
            if (sounds.Count > 1) _soundHandler.PlayClip(sounds[Random.Range(0, sounds.Count)]);
        }
    }

    private void TouchHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_collider2D == Physics2D.OverlapPoint(touchPosition))
            {
                FlipSprite(true);
                currentDirection *= -1;
            }
        }
    }
}
