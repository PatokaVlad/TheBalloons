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
    }
}
