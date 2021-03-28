using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MoveableObject
{
    private Vector2 direction = new Vector2();

    private int angle = 0;

    private bool hasDirection = false;

    protected override void ChooseDirection()
    {
        gameObject.transform.Rotate(0, 0, -angle);

        angle = Random.Range(-180, 180);

        direction = RotateVector(Vector2.up, angle).normalized;
        gameObject.transform.Rotate(0, 0, angle);

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

        TouchHandle();
    }

    private void TouchHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_collider2D == Physics2D.OverlapPoint(touchPosition))
            {
                ChooseDirection();

                if (sounds.Count > 0)
                {
                    _soundHandler.PlayClip(sounds[Random.Range(0, sounds.Count)]);
                }
            }
        }
    }
}
