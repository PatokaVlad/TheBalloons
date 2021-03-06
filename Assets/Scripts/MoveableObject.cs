using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableObject : MonoBehaviour
{
    protected Transform _transform;

    [SerializeField]
    protected float speed = 5;

    private void Start()
    {
        _transform = GetComponent<Transform>();
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
