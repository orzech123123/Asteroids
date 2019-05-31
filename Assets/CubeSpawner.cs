using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        ObjectPooler.Instance.SpawnFromPool("Cube", transform.position + new Vector3(Random.Range(0, 2), 0, Random.Range(0, 2)), Quaternion.identity);
    }
}
