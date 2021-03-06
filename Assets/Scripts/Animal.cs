using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MoveableObject
{
    protected override void MoveObject()
    {
        Vector2 moveVector = Vector2.down * speed * Time.deltaTime;
        moveVector += (Vector2)_transform.position;
        _transform.position = moveVector;
    }
}
