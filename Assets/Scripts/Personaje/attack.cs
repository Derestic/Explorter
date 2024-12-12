using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    GameObject parent;
    [SerializeField] LayerMask atacklayer;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Layer: " + (1 << other.gameObject.layer) + " to " + atacklayer.value + " is " + (atacklayer.value & (1 << other.gameObject.layer)));

        if ((atacklayer.value & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<npc>().addLife(-parent.GetComponent<npc>().getDamage());
            GetComponent<Collider>().enabled = false;
        }
    }
}
