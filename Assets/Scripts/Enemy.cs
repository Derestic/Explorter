using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    enum state
    {
        idle,
        setObjective,
        run,
        attack
    }
    [Header("Control de estados")]
    [SerializeField] state status;
    public GameObject ObjetivoF;
    GameObject Objetivo;
    public NavMeshSurface navS;
    NavMeshAgent agent;
    [SerializeField] float maxDistanceOjective = 10;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        status = state.idle;
        hit = new RaycastHit();
        visto = new List<GameObject> ();
        r = new Ray();
        Objetivo = ObjetivoF;
    }

    // Update is called once per frame
    void Update()
    {
        if (state.idle == status)
        {
            if(!agent.isStopped) agent.isStopped = true;
            if (Vector3.Distance(transform.position, Objetivo.transform.position) < maxVision) status++;
        }
        else if (state.setObjective == status)
        {
            status++;
            agent.SetDestination(Objetivo.transform.position);
        }
        else if(state.run == status)
        {
            agent.isStopped = false;
            if (Vector3.Distance(transform.position, Objetivo.transform.position) < maxDistanceOjective)
            {
                status++;
                agent.isStopped = true;
                Debug.Log("Atacando");
            }else if (Vector3.Distance(transform.position, Objetivo.transform.position) > maxVision)
            {
                status = state.idle;
            }
                else if (!agent.destination.Equals(transform.position))
            {
                agent.SetDestination(Objetivo.transform.position);
            }
        }
        else if (state.attack == status && Vector3.Distance(transform.position, Objetivo.transform.position) > maxDistanceOjective)
        {
            status--;
        }
        vision();
    }
    [Header("Vision")]
    [SerializeField] float angulo;
    [SerializeField] float maxVision;
    [SerializeField] int numRays = 2;
    Vector3 rotitoGizmo = new Vector3(0, 0, 0);
    Vector3 rotito = new Vector3(0, 0, 0);
    Ray r;
    RaycastHit hit;
    List<GameObject> visto;
    void vision()
    {
        float gap = -angulo / numRays;
        rotitoGizmo.Set(0, 0, 1);
        float ini = -(Vector3.Angle(rotitoGizmo, transform.forward) * 3.14f / 180 - angulo / 2);
        for (int i = 0; i <= numRays; i++)
        {
            rotito.Set(Mathf.Sin(ini), 0, Mathf.Cos(ini));
            r.origin = transform.position;
            r.direction = rotito;
            ini += gap;
            if(Physics.Raycast(r, out hit) && 
                Vector3.Distance(transform.position, hit.transform.position) < maxVision &&
                hit.collider.gameObject.tag == "Player")
            {
                Objetivo = hit.transform.gameObject;
                break;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        float gap = -angulo / numRays;
        rotitoGizmo.Set(0, 0, 1);
        
        float ini = -(Vector3.Angle(rotitoGizmo,transform.forward) * 3.14f/180 - angulo / 2);


        //Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxVision);
        //rotitoGizmo.Set(Mathf.Sin(ini), 0, Mathf.Cos(ini));
        //Gizmos.DrawLine(transform.position, transform.position + rotitoGizmo * maxVision);
        //rotitoGizmo.Set(Mathf.Sin(ini- angulo), 0, Mathf.Cos(ini - angulo));
        //Gizmos.DrawLine(transform.position, transform.position + rotitoGizmo * maxVision);
        
        for (int i = 0; i <= numRays; i++)
        {
            Gizmos.DrawLine(transform.position, transform.position + rotitoGizmo * maxVision);
            ini += gap;
            rotitoGizmo.Set(Mathf.Sin(ini), 0, Mathf.Cos(ini));
        }

    }
}
