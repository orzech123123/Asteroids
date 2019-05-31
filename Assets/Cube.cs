using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float upForce = 1f;
    public float sideForce = 0.1f;

    public void Launch()
    {
        var xForce = Random.Range(-sideForce, sideForce);
        var yForce = Random.Range(upForce / 2f, upForce);
        var zForce = Random.Range(-sideForce, sideForce);

        var force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;
    }
}
