using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonsHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> balloons = new List<GameObject>();

    private SceneLoader _sceneLoader;
    private PointsHandler _pointsHandler;

    [SerializeField]
    private int maxOneTimeSpawnCount = 20;
    [SerializeField]
    private int minOneTimeSpawnCount = 5;
    [SerializeField]
    private float spawnDeltaTime = 0.1f;
    [SerializeField]
    private float minSpeed = 4;
    [SerializeField]
    private float maxSpeed = 7;
    private float edgeX,
        edgeY;

    [SerializeField]
    private bool useMouse;
    [SerializeField]
    private bool earnPoints;
    private bool spawn = true;

    public delegate void OnQuit(bool playParticle, bool playSound, bool earnPoint);
    public event OnQuit onQuit;

    public bool UseMouse { get => useMouse; }
    public float MinSpeed { get => minSpeed; }
    public float MaxSpeed { get => maxSpeed; }
    public bool EarnPoints { get => earnPoints; }

    private void Awake()
    {;   
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _pointsHandler = FindObjectOfType<PointsHandler>();
    }

    private void Start()
    {
        edgeX = Camera.main.orthographicSize * Camera.main.aspect;
        edgeY = Camera.main.orthographicSize;

        StartSpawn();
    }

    public void SpawnBalloons(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-edgeX, edgeX), -edgeY - 2);
            Instantiate(balloons[Random.Range(0, balloons.Count)], spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (spawn)
        {
            int count = Random.Range(minOneTimeSpawnCount, maxOneTimeSpawnCount);
            SpawnBalloons(count);
            yield return new WaitForSeconds(spawnDeltaTime);
        }
    }

    private void StartSpawn()
    {
        StartCoroutine(SpawnObjects());
    }

    public void QuitGame()
    {
        if(_pointsHandler != null)
        {
            if(EarnPoints)
            {
                SaveHandler.SaveBestScore(_pointsHandler.PointsEarned);
            }
        }

        StartCoroutine(WaitToQuit());        
    }

    private IEnumerator WaitToQuit()
    {
        spawn = false;
        StopCoroutine(SpawnObjects());
        onQuit(true, true, false);
        yield return new WaitForSeconds(1f);
        _sceneLoader.LoadLevelScene(0);
    }
}
