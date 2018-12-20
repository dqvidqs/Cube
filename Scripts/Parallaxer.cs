
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{
    class PoolObject
    {
        public Transform transform;
        public bool inUse;

        public PoolObject(Transform t) { 
            transform = t;
        }
        public void Use() {
            inUse = true; 
        }
        public void Dispose() { 
            inUse = false;
        }
    }

    public GameObject Prefab;
    public int poolSize;
    public float shiftSpeed;
    public float spawnRate;

    public Vector3 defaultSpawnPos;
    public float coefficientDispose;
    public bool spawnImmediate;
    public Vector3 immediateSpawnPos;
    public Vector2 targetAspectRatio;
    public bool Now;
    int t = 0;
    int next = 0;
    float spawnTimer = 0;
    PoolObject[] poolObjects;
    float targetAspect;
    GameManager game;
    
    void Awake()
    {
        Configure();
    }
    void OnGameStarted()
    {
        game = GameManager.Instance;
    }

    void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
        GameManager.OnGameStarted += OnGameStarted;
    }
    
    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
        GameManager.OnGameStarted -= OnGameStarted;
    }

    void OnGameOverConfirmed()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].Dispose();
            poolObjects[i].transform.position = Vector3.one * 1000;
        }
        if (spawnImmediate)
        {
            SpawnImmediate();
        }
    }

    void Update()
    {        
        if(game!=null){

            if (game.GameOver) return;
            t++;
            if(t > 600)
            {
                shiftSpeed += shiftSpeed*(float)0.1;
                t = 0;
            }

            Shift();
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnRate && !Now)
            {
                Spawn();
                spawnTimer = 0;
            }
            if(spawnTimer > spawnRate && Now){
                Spawn_custome();
            }
        }
    }

    void Configure()
    {
        //spawning pool objects
        targetAspect = targetAspectRatio.x / targetAspectRatio.y;
        poolObjects = new PoolObject[poolSize];
        for (int i = 0; i < poolObjects.Length; i++)
        {
            GameObject go = Instantiate(Prefab) as GameObject;
            Transform t = go.transform;
            t.SetParent(transform);
            t.position = Vector3.one * 1000;
            poolObjects[i] = new PoolObject(t);
        }

        if (spawnImmediate)
        {
            SpawnImmediate();
        }
    }
    void Spawn_custome()
    {
        if(!poolObjects[next % 2].inUse){
        //moving pool objects into place
            Transform t = GetPoolObject();
            if (t == null) return;
            Vector3 pos = Vector3.zero;
            pos.y = 0 + defaultSpawnPos.y;// Random.Range(ySpawnRange.minY, ySpawnRange.maxY);
            pos.x = 0 + defaultSpawnPos.x;// (defaultSpawnPos.x * Camera.main.aspect) / targetAspect;
            pos.z = 0 + defaultSpawnPos.z;
            t.position = pos;
            next++;
        }
    }

    void Spawn()
    {
        //moving pool objects into place
        Transform t = GetPoolObject();
        if (t == null) return;
        Vector3 pos = Vector3.zero;
        pos.y = 0 + defaultSpawnPos.y;// Random.Range(ySpawnRange.minY, ySpawnRange.maxY);
        pos.x = 0 + defaultSpawnPos.x;// (defaultSpawnPos.x * Camera.main.aspect) / targetAspect;
        pos.z = 0 + defaultSpawnPos.z;
        t.position = pos;
    }

    void SpawnImmediate()
    {
        Transform t = GetPoolObject();
        if (t == null) return;
        Vector3 pos = Vector3.zero;
        pos.y = 0 + defaultSpawnPos.y;
        pos.x = 0 + defaultSpawnPos.x;
        pos.z = 0 + defaultSpawnPos.z;
        t.position = pos;
        //Spawn();
    }

    void Shift()
    {
        //loop through pool objects 
        //moving them
        //discarding them as they go off screen
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].transform.position += Vector3.left * shiftSpeed * Time.deltaTime;
            CheckDisposeObject(poolObjects[i]);
        }
    }

    void CheckDisposeObject(PoolObject poolObject)
    {
        //place objects off screen)
        if (poolObject.transform.position.x < (coefficientDispose * Camera.main.aspect) / targetAspect)
        {
            poolObject.Dispose();
            poolObject.transform.position = Vector3.one * 1000;
        }
    }

    Transform GetPoolObject()
    {
        //retrieving first available pool object
        for (int i = 0; i < poolObjects.Length; i++)
        {
            if (!poolObjects[i].inUse)
            {
                poolObjects[i].Use();
                return poolObjects[i].transform;
            }
        }
        return null;
    }
}