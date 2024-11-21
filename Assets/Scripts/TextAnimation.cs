using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 10f;
    [SerializeField]
    float maxRotation = 20f;
    [SerializeField]
    float minRotation = -20f;

    float z = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        z += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, z);
        if ((z >= maxRotation && rotationSpeed > 0) || (z <= minRotation && rotationSpeed < 0))
        {
            rotationSpeed *= -1;
        }
    }
}
