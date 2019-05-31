using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _hiddenObjects;

    [SerializeField]
    private List<GameObject> _visibleObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            _hiddenObjects.ForEach(o => o.SetActive(true));
            _visibleObjects.ForEach(o => o.SetActive(false));
        }
        else
        {
            _hiddenObjects.ForEach(o => o.SetActive(false));
            _visibleObjects.ForEach(o => o.SetActive(true));
        }
    }
}
