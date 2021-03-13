using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MoveableObject
{
    private Vector2 direction = new Vector2();

    private bool hasDirection = false;
    private bool isStoped = true;
    private bool isDestryed = false;

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

        TouchHandle();
    }

    private void TouchHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_collider2D == Physics2D.OverlapPoint(touchPosition))
            {
                if (!isDestryed)
                {
                    _soundHandler.PlayClip(sounds[Random.Range(0, sounds.Count)]);
                    StartCoroutine(SmoothDestroy());
                }
            }
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);

        if (!isDestryed) 
        {
            StartCoroutine(SmoothDestroy());
        }
    }

    private IEnumerator SmoothDestroy()
    {
        isDestryed = true;

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
