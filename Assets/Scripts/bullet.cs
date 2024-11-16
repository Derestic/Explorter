using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float damage = 20;
    [SerializeField] LayerMask atacklayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((atacklayer.value & (1 << other.gameObject.layer)) != 0)
        {
            other.GetComponent<npc>().addLife(-damage);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
