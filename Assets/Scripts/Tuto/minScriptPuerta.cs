using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minScriptPuerta : MonoBehaviour
{
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject panelMisionP;
    [SerializeField] GameObject panelMisionA;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.transform.position = spawn.transform.position;
            panelMisionP.SetActive(false);
            panelMisionA.SetActive(true);
        }
    }
}
