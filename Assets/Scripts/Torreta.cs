using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torreta : npc
{
    [Header("Partes Cuerpo")]
    public GameObject head;

    [Header("Ataque")]
    public GameObject objetivo = null;
    public GameObject bullet;
    public GameObject spawn;
    [SerializeField] float timer = 2;
    float time;
    [SerializeField]float bulletspeed=600;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Collider>().enabled)
        {
            if (objetivo != null)
            {
                if (Vector3.Distance(transform.position, objetivo.transform.position) >= maxVision)
                {
                    objetivo = null;
                }
                else
                {
                    head.transform.LookAt(objetivo.transform.position);
                    shot();
                }
            }
            vision();
        }
    }

    [Header("Vision")]
    [SerializeField] LayerMask layer;
    [SerializeField] float maxVision;
    void vision()
    {
        Collider[] g = Physics.OverlapSphere(transform.position, maxVision, layer);
        if (g.Length > 0 && objetivo == null)
        {
            Debug.Log("Enemigo cambiado");
            objetivo = g[0].gameObject;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,maxVision);
    }

    void shot()
    {
        time += Time.deltaTime;
        Debug.Log("Disparo?");
        if(time >= timer)
        {
            Debug.Log("Disparo");
            GameObject obj = Instantiate(bullet);
            obj.transform.position = spawn.transform.position;
            obj.transform.LookAt(objetivo.transform.position);
            obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward*bulletspeed);
            time = 0;
        }
    }
}
