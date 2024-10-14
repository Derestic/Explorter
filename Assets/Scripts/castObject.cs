using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castObject : MonoBehaviour
{
    [SerializeField]
    Ray ray;
    RaycastHit hit;
    public GameObject pointer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            pointer.transform.position = hit.point;
            //Debug.DrawRay(transform.position, transform.forward);
        }
    }
}
