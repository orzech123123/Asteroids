using UnityEngine;
using System.Collections;

public class RandomRotator2 : MonoBehaviour
{
    [SerializeField]
    private float tumble;

    private Vector3 axis;
    private int speed;

    private void Start()
    {
        axis = new Vector3(0, Random.value, 0);
        speed = Random.Range(45, 90);
    }

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, axis, Time.deltaTime * speed);
    }
}