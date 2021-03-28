using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Collider2D _collider2D;
    private Transform _transform;
    private ParticleSystem _particle;
    private ParticleSystem _childrenParticle;
    private SpriteRenderer _spriteRenderer;

    private BalloonsHandler _balloonsHandler;
    private PointsHandler _pointsHandler;
    private SoundHandler _soundHandler;

    private Color spriteColor;

    private GameObject moveableObject;
    private GameObject moveableObjectSprite;

    private float minSpeed;
    private float maxSpeed;

    private float speed;

    [SerializeField]
    private bool hideMoveableObject = true;
    [SerializeField]
    private bool isDestroyable = true;
    private bool isDestroyed = false;
    private bool earnPoints;
    private bool createMoveableObject;

    [SerializeField]
    [Range(0, 100)]
    private float spawnChance = 100;
    private float currentSpawnChance;


    private void Awake()
    {
        _balloonsHandler = FindObjectOfType<BalloonsHandler>();
    }

    private void OnEnable()
    {
        _balloonsHandler.onQuit += DestroyGameObject;
    }

    private void Start()
    {
        _soundHandler = FindObjectOfType<SoundHandler>();
        _pointsHandler = FindObjectOfType<PointsHandler>();

        _transform = GetComponent<Transform>();
        _collider2D = GetComponent<Collider2D>();
        _particle = GetComponent<ParticleSystem>();
        if (GetComponentsInChildren<ParticleSystem>().Length > 1) _childrenParticle = GetComponentsInChildren<ParticleSystem>()[1];
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Initialize();

        float scale = _transform.localScale.x + Random.Range(-_balloonsHandler.MaxSizeScale, 
            _balloonsHandler.MaxSizeScale);

        _transform.localScale = new Vector3(scale, scale, 1f);

        if (_particle) spriteColor = _particle.startColor;
        if (GetComponent<ParticleSystemRenderer>()) GetComponent<ParticleSystemRenderer>().material.color = spriteColor;

        speed = Random.Range(minSpeed, maxSpeed);

        moveableObject = _balloonsHandler.GetRandomMoveableObject();

        currentSpawnChance = Random.Range(0, 100);

        if (!hideMoveableObject) 
        {            
            if (spawnChance > currentSpawnChance) 
            {
                CreateMoveableObjectSprite();
            }
        }
    }

    private void CreateMoveableObjectSprite()
    {
        GameObject moveableObjectCopy = Instantiate(moveableObject, _transform.position, Quaternion.identity, _transform);

        moveableObjectCopy.GetComponent<MoveableObject>().enabled = false;
        moveableObjectCopy.GetComponent<Collider2D>().enabled = false;

        SpriteRenderer _sprite = moveableObjectCopy.GetComponent<SpriteRenderer>();
        _sprite.sortingOrder = _spriteRenderer.sortingOrder + 1;

        Color color = _sprite.color;
        color.a = 0.4f;
        _sprite.color = color;

        moveableObjectSprite = moveableObjectCopy;
    }

    private void Update()
    {
        MoveUp();

        if (!_balloonsHandler.UseMouse)
            TouchHandle();
        else
            MouseHandle();
    }

    private void OnDisable()
    {
        _balloonsHandler.onQuit -= DestroyGameObject;
    }

    private void Initialize()
    {
        minSpeed = _balloonsHandler.MinSpeed;
        maxSpeed = _balloonsHandler.MaxSpeed;
        earnPoints = _balloonsHandler.EarnPoints;
        createMoveableObject = _balloonsHandler.CreateMoveableObject;
    }

    private void MoveUp()
    {
        Vector3 delta = Vector3.up * speed * Time.deltaTime;
        _transform.position += delta;
    }

    private void TouchHandle()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            CheckInputPosition(touchPosition, true, true, earnPoints);
        }
    }

    private void MouseHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
            CheckInputPosition(touchPosition, true, true, earnPoints);
        }
    }

    private void CheckInputPosition(Vector2 position, bool playParticle, bool playSound, bool earnPoint)
    {
        if (_collider2D == Physics2D.OverlapPoint(position))
        {
            if (isDestroyable)
            {
                DestroyGameObject(playParticle, playSound, earnPoint, createMoveableObject);
            }
        }
    }

    private void DestroyGameObject(bool playParticle, bool playSound, bool earnPoint, bool useMoveableObject)
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            speed = 0f;

            if (playParticle)
            {
                _particle.Play();
                _childrenParticle.Play();
            }

            if (playSound) 
            {
                if (_soundHandler != null)
                {
                    _soundHandler.PlayBalloonClip();
                }
            }

            if (earnPoint)
            {
                _pointsHandler.IncreaseEarnedPointsCount();
            }

            if (useMoveableObject) 
            {
                if (spawnChance > currentSpawnChance)
                {
                    GameObject currentMoveableObject = Instantiate(moveableObject, _transform.position, Quaternion.identity, _balloonsHandler.transform);

                    if (currentMoveableObject.CompareTag("Blot"))
                    {
                        SpriteRenderer sprite = currentMoveableObject.GetComponent<SpriteRenderer>();

                        int maxOrder = 32767;

                        sprite.color = spriteColor;

                        if (_balloonsHandler.moveableObjectSortingOrder < maxOrder)
                        {
                            _balloonsHandler.moveableObjectSortingOrder += 1;
                            sprite.sortingOrder = _balloonsHandler.moveableObjectSortingOrder;
                        }
                        else sprite.sortingOrder = maxOrder;
                    }

                    _balloonsHandler.childs.Add(currentMoveableObject);
                }
            }

            if(gameObject.activeSelf)
            {
                StartCoroutine(WaitToDestroy());
            }
        }
    }

    private IEnumerator WaitToDestroy()
    {
        if (!hideMoveableObject)
        {
            if (moveableObjectSprite != null) 
            {
                Destroy(moveableObjectSprite);
            }
        }

        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(_particle.main.duration);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        DestroyGameObject(false, false, false, false);
    }
}
