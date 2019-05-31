using BansheeGz.BGSpline.Curve;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(var pool in pools)
        {
            var objectPool = new Queue<GameObject>();

            for (var i = 0; i < pool.size; i++)
            {
                var go = Instantiate(pool.prefab);
                go.SetActive(false);
                objectPool.Enqueue(go);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
        



        var curve = GameObject.Find("Curve2").GetComponent<BGCurve>();
        var math = new BGCurveBaseMath(curve);
        Debug.LogWarning(string.Format("ile tych sykyryk: {0}", curve.Points.Length));
        Debug.LogWarning(string.Format("ile tych punktow: {0}", math.CalcPositionByT(curve.Points[2] as BGCurvePoint, curve.Points[3] as BGCurvePoint, 0)));
        
        for (var i = 0; i < curve.Points.Length; i++)
        {
            if (i == curve.Points.Length - 1)
            {
                break;
            }

            var point1 = curve.Points[i];
            var point2 = curve.Points[i + 1];

            for (var t = 0.04f; t <= 1; t += 0.04f)
            {
                var position = math.CalcPositionByT(point1 as BGCurvePoint, point2 as BGCurvePoint, t);
                var position2 = math.CalcPositionByT(point1 as BGCurvePoint, point2 as BGCurvePoint, t + 0.04f);
                

                var go = Instantiate(pools[1].prefab);
                go.transform.position = position;
                go.transform.up = (position2 - position);

                Debug.DrawLine(position, position2, Color.red, 2000, false);
            }
        }
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag...");
            return null;
        }

        var objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.GetComponent<Cube>().Launch();

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
}
