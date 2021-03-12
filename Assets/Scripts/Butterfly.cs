using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MoveableObject
{
    private Vector2 direction = new Vector2();

    private bool hasDirection = false;

    protected override void ChooseDirection()
    {
        int angle = Random.Range(-180, 180);

        direction = RotateVector(Vector2.up, angle).normalized;

        hasDirection = true;
    }

    protected override void MoveObject()
    {
        if (!hasDirection) 
        {
            ChooseDirection();
        }

        Vector2 moveVector = direction * speed * Time.deltaTime;
        moveVector += (Vector2)_transform.position;
        _transform.position = moveVector;
    }
}
