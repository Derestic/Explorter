using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomObject : MonoBehaviour
{
    public GameObject[] objectList;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 2) == 0) {
            Vector3 v = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject go = Instantiate(objectList[Random.Range(0, objectList.Length)], v, Quaternion.identity);
            if(go.tag != "ArbolTag") go.transform.parent = transform;
        }
    }
}
