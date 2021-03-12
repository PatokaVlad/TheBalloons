using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MoveableObject
{
    private Vector2 direction = new Vector2();

    private bool hasDirection = false;
    private bool isStoped = true;

    [SerializeField]
    private float timeToDestroy = 7f;

    protected override void ChooseDirection()
    {
        int angle = Random.Range(-45, 46);

        direction = RotateVector(Vector2.down, angle).normalized;

        hasDirection = true;
    }

    protected override void MoveObject()
    {
        if (!hasDirection)
        {
            ChooseDirection();
        }

        Vector2 moveVector = direction * speed * Time.deltaTime;

        if (_transform.position.y > -Camera.main.orthographicSize + 1f)
        {
            moveVector += (Vector2)_transform.position;
            _transform.position = moveVector;
        }
        else
        {
            if (isStoped) 
            {
                StartCoroutine(WaitAndDestroy());
                isStoped = false;
            }
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        StartCoroutine(SmoothDestroy());
    }

    private IEnumerator SmoothDestroy()
    {
        float time = 0;
        float spriteTransparent = 1;

        Color color = _spriteRenderer.color;

        while (time < timeToDestroy) 
        {
            time += Time.deltaTime;
            spriteTransparent -= time / timeToDestroy;
            color.a = spriteTransparent;
            _spriteRenderer.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }
}
