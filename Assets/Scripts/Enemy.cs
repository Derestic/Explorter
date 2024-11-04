using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Enemy : npc
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
    //public NavMeshSurface navS;
    NavMeshAgent agent;
    [SerializeField] float maxDistanceOjective = 10;

    [Header("Control Enemigo")]
    [SerializeField] float speed = 150f;
    Animator animator;
    bool atacking = false;
    [SerializeField]float atackGap = 5;
    [SerializeField] float countAtack = 0;
    [SerializeField] float damage = 10;
    [SerializeField] Vector3 posAttack;
    [SerializeField] Vector3 scaleAttack;
    [SerializeField] LayerMask atacklayer;

    // Start is called before the first frame update
    void Start()
    {
        setAll();
    }

    public void setAll()
    {
        ObjetivoF = Manager.Instance.nucleo;

        agent = GetComponent<NavMeshAgent>();
        status = state.idle;
        hit = new RaycastHit();
        visto = new List<GameObject>();
        r = new Ray();
        Objetivo = ObjetivoF;
        Debug.Log("Speed: " + speed );
        agent.speed = speed;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.idle == status)
        {
            animator.SetBool("atacking", false);
            if (!agent.isStopped) agent.isStopped = true;
                Objetivo = ObjetivoF;
                status++;
        }
        else if (state.setObjective == status)
        {
            status++;
            agent.SetDestination(Objetivo.transform.position);
        }
        else if(state.run == status)
        {
            animator.SetBool("runing",true);
            agent.isStopped = false;
            if (Vector3.Distance(transform.position, Objetivo.transform.position) < maxDistanceOjective)
            {
                status++;
                StartCoroutine(ExampleCoroutine());
                agent.isStopped = true;
                Debug.Log("Atacando");
                animator.SetBool("runing", false);
            }
            else if (Vector3.Distance(transform.position, Objetivo.transform.position) > maxVision && !Objetivo.Equals(ObjetivoF))
            {
                status = state.idle;
            }
                else if (!agent.destination.Equals(transform.position))
            {
                agent.SetDestination(Objetivo.transform.position);
            }
        }
        else if (state.attack == status)
        {
            animator.SetBool("atacking", true); 
            if(Vector3.Distance(transform.position, Objetivo.transform.position) > maxDistanceOjective*1.2f)
            {
                animator.SetBool("atacking", false);
                status--;
                StopCoroutine(ExampleCoroutine());
            }
        }
        vision();
    }
    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Comienza la corrutina");
        yield return new WaitForSeconds(GetAnimationInterval(animator, "WalkFWD"));
        // Espera 2 segundos
        if (Physics.BoxCast(transform.position + transform.up / 2 -transform.forward, scaleAttack,
                transform.forward,
                out hit, transform.rotation, maxDistanceOjective * 1.5f, atacklayer))
        {
            Debug.Log("Atack-----");
            hit.transform.gameObject.GetComponent<npc>().addLife(-damage);
        }
        Debug.Log("Termina la corrutina");
        StartCoroutine(ExampleCoroutine());
    }

    float GetAnimationInterval(Animator animator, string animName)
    {
        // Accede a los clips del Animator
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animName)
            {
                return clip.length; // Devuelve la duración de la animación
            }
        }

        Debug.LogWarning("No se encontró la animación con el nombre: " + animName);
        return 0f;
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
        //float gap = -angulo / numRays;
        float gap = (angulo * Mathf.Deg2Rad) / numRays;
        //rotitoGizmo.Set(0, 0, 1);
        //float ini = -(Vector3.Angle(rotitoGizmo, transform.forward) * 3.14f / 180 - angulo / 2);
        float ini = -angulo * Mathf.Deg2Rad / 2;
        for (int i = 0; i <= numRays; i++)
        {
            Vector3 direction = Quaternion.Euler(0, ini * Mathf.Rad2Deg, 0) * transform.forward;

            //rotito.Set(Mathf.Sin(ini), 0, Mathf.Cos(ini));

            r.origin = transform.position; 
            r.direction = transform.position + direction * maxVision;
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
        /*Gizmos.color = Color.yellow;
        float gap = -angulo / numRays;
        rotitoGizmo.Set(0, 0, 1);
        
        float ini = Mathf.Abs(Vector3.Angle(rotitoGizmo,transform.forward) * 3.14f/180 - angulo / 2);


        //Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxVision);
        //rotitoGizmo.Set(Mathf.Sin(ini), 0, Mathf.Cos(ini));
        //Gizmos.DrawLine(transform.position, transform.position + rotitoGizmo * maxVision);
        //rotitoGizmo.Set(Mathf.Sin(ini- angulo), 0, Mathf.Cos(ini - angulo));
        //Gizmos.DrawLine(transform.position, transform.position + rotitoGizmo * maxVision);
        //rotitoGizmo = Quaternion.Euler(0, angulo/2, 0) * transform.forward;
        for (int i = 0; i <= numRays; i++)
        {
            Gizmos.DrawLine(transform.position, transform.position + rotitoGizmo * maxVision);
            ini += gap;
            rotitoGizmo.Set(Mathf.Sin(ini), 0, Mathf.Cos(ini));
        }

        Gizmos.DrawWireCube(transform.position + transform.forward, scaleAttack);*/
        Gizmos.color = Color.yellow;
        float gap = (angulo * Mathf.Deg2Rad) / numRays; // Paso del ángulo en radianes
        float ini = -angulo * Mathf.Deg2Rad / 2; // Ángulo inicial en radianes (izquierda del abanico)

        for (int i = 0; i <= numRays; i++)
        {
            // Calcula el vector en la dirección actual usando una rotación sobre transform.forward
            Vector3 direction = Quaternion.Euler(0, ini * Mathf.Rad2Deg, 0) * transform.forward;

            Gizmos.DrawLine(transform.position, transform.position + direction * maxVision);

            ini += gap; // Aumenta el ángulo en cada paso
        }
        
        // Dibuja el cubo de ataque
        Gizmos.DrawWireCube(transform.position + transform.forward + transform.up/2, scaleAttack);

    }
}
