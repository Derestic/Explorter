using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float timer=3;
    float time;
    [SerializeField] float damage = 20;
    [SerializeField] LayerMask atacklayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= timer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((atacklayer.value & (1 << other.gameObject.layer)) != 0)
        {
            other.GetComponent<npc>().addLife(-damage);
        }
    }
}
