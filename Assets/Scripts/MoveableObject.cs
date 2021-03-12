using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableObject : MonoBehaviour
{
    protected SoundHandler _soundHandler;

    protected Transform _transform;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider2D;

    [SerializeField]
    protected List<AudioClip> sounds = new List<AudioClip>();

    [SerializeField]
    private float minSpeed = 3;
    [SerializeField]
    private float maxSpeed = 5;

    protected float speed;

    [SerializeField]
    protected bool playSoundOnSpawn;

    private void Start()
    {
        _soundHandler = FindObjectOfType<SoundHandler>();

        speed = Random.Range(minSpeed, maxSpeed + 1);

        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();

        if ((sounds.Count != 0) && playSoundOnSpawn)
        {
            _soundHandler.PlayClip(sounds[Random.Range(0, sounds.Count)]);
        }
    }

    private void Update()
    {
        MoveObject();
    }

    protected abstract void MoveObject();

    protected virtual void ChooseDirection() { }

    protected Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float radians = Mathf.Deg2Rad * degrees;

        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        return new Vector2(vector.x * cos - vector.y * sin, vector.x * sin + vector.y * cos);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
