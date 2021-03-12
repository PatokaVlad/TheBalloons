using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableObject : MonoBehaviour
{
    private SoundHandler _soundHandler;

    protected Transform _transform;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider2D;

    [SerializeField]
    private List<AudioClip> sounds = new List<AudioClip>();

    [SerializeField]
    private float minSpeed = 3;
    [SerializeField]
    private float maxSpeed = 5;

    protected float speed;

    private void Start()
    {
        _soundHandler = FindObjectOfType<SoundHandler>();

        speed = Random.Range(maxSpeed, maxSpeed + 1);

        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();

        if (sounds.Count != 0)
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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
