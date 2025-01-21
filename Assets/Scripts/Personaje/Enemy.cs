using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : npc
{

    [Header("ManagerLink")]
    [SerializeField] public ManagerGen man;
    enum state
    {
        idle,
        run,
        attack
    }

    [Header("Control de estados")]
      [SerializeField] state status;
      [SerializeField]GameObject ObjetivoF;
      [SerializeField] GameObject Objetivo;
      NavMeshAgent agent;
      [SerializeField] float gapObjetivo = 0.1f;
      public Collider slimeColidder;

    [Header("Control Enemigo")]
      [SerializeField] float speed = 150f;
      [SerializeField] LayerMask atacklayer;
      public Collider AttackColidder;
      Vector3 rectifObjective;
      Vector3 moveAleatory = new Vector3();
      [SerializeField] float rangeAleatory = 10;


    [Header("Vision")]
      [SerializeField] float angulo;
      [SerializeField] float maxVision;
      [SerializeField] int numRays = 2;
      [SerializeField] float upperPosition = 0.5f;
      float gap; 
      GameObject mejorColide = null;
      Ray r;
      RaycastHit hit;

    [Header("Animation Control")]
      Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        if (man != null && man.GetType().Equals(typeof(Manager))) { 
            ObjetivoF = ((Manager)man).getNucleo(); 
        }

        gap = (angulo * Mathf.Deg2Rad) / numRays;
        agent = GetComponent<NavMeshAgent>();
        status = state.idle;
        hit = new RaycastHit();
        r = new Ray();
        Objetivo = ObjetivoF;
        agent.speed = speed;
        animator = GetComponent<Animator>();
        StartCoroutine(Vision());
    }

    // Update is called once per frame
    void Update()
    {
        if(animator != null) animationSync();
        battleLogic();
    }

    private IEnumerator Vision()
    {
        mejorColide = ObjetivoF;
        float ini = -angulo * Mathf.Deg2Rad / 2; 
        float d = maxVision*2;
        for (int i = 0; i <= numRays; i++)
        {
            Vector3 direction = Quaternion.Euler(0, ini * Mathf.Rad2Deg, 0) * transform.forward;

            r.origin = transform.position + transform.up * upperPosition; 
            r.direction = direction;
            ini += gap;

            if(Physics.Raycast(r, out hit, maxVision, atacklayer))
            {
                if (hit.distance < d) { mejorColide = hit.transform.gameObject; d = hit.distance; Debug.Log("Objective changed"); }
            }
        }
        if(mejorColide != null)
        {
            agent.SetDestination(mejorColide.transform.position);
            if (Objetivo == null || !Objetivo.Equals(mejorColide))
            {
                Objetivo = mejorColide;
                if (status == state.idle) status = state.run;
            }
        }

        yield return new WaitForSeconds(0.25f);
        StartCoroutine(Vision());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        float ini = -angulo * Mathf.Deg2Rad / 2; // Ángulo inicial en radianes (izquierda del abanico)
        float gap = (angulo * Mathf.Deg2Rad) / numRays;
        for (int i = 0; i <= numRays; i++)
        {
            // Calcula el vector en la dirección actual usando una rotación sobre transform.forward
            Vector3 direction = Quaternion.Euler(0, ini * Mathf.Rad2Deg, 0) * transform.forward;

            Gizmos.DrawLine(transform.position + transform.up* upperPosition, 
                (transform.position + direction * maxVision) + transform.up* upperPosition);

            ini += gap; // Aumenta el ángulo en cada paso
        }

        Gizmos.DrawSphere(moveAleatory,0.5f);
    }

    void animationSync()
    {
        if (dead) { animator.SetBool("dead", true); agent.isStopped = true;  }
        else if (state.idle == status) { animator.SetBool("atacking", false); animator.SetBool("runing", false); }
        else if (state.attack == status) { animator.SetBool("atacking", true); animator.SetBool("runing", false); }
        else if (state.run == status) { animator.SetBool("atacking", false); animator.SetBool("runing", true); }
    }

    void battleLogic()
    {
        if (state.idle == status)
        {
            if (!agent.isStopped) agent.isStopped = true;
            Objetivo = ObjetivoF;
            if (Objetivo != null) {
                Debug.Log("Objetivo " + Objetivo.name);
                agent.SetDestination(Objetivo.transform.position);
                status = state.run;
            }
            else
            {
                moveAleatory.x = Random.Range(rangeAleatory, rangeAleatory) + transform.position.x;
                moveAleatory.z = Random.Range(rangeAleatory, rangeAleatory) + transform.position.z;
                moveAleatory.y = transform.position.y;
                agent.SetDestination(moveAleatory);
                status = state.run;
            }
        }
        else if (state.run == status)
        {
            agent.isStopped = false;
            if (Objetivo == null)
            {
                if(Vector3.Distance(transform.position, agent.destination) <= gapObjetivo * 2)
                {
                    moveAleatory.x = Random.Range(-rangeAleatory+1, rangeAleatory-1) + transform.position.x;
                    if (moveAleatory.x < 0) moveAleatory.x -= 1; else moveAleatory.x += 1;
                        moveAleatory.z = Random.Range(-rangeAleatory+1, rangeAleatory-1) + transform.position.z;
                    if (moveAleatory.z < 0) moveAleatory.z -= 1; else moveAleatory.z += 1;
                    moveAleatory.y = transform.position.y;
                    agent.destination = moveAleatory;
                }
                else
                {
                    print(Vector3.Distance(transform.position, agent.destination));
                }
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance + gapObjetivo)
                {
                    status = state.attack;
                }
                else if (agent.remainingDistance > maxVision && !Objetivo.Equals(ObjetivoF))
                {
                    status = state.idle;
                }
                else if (Vector3.Distance(agent.destination, Objetivo.transform.position) > gapObjetivo * 2)
                {
                    agent.SetDestination(Objetivo.transform.position);
                }
            }
            
        }
        else if (state.attack == status)
        {
            agent.isStopped = true;
            rectifObjective = Objetivo.transform.position;
            rectifObjective.y = transform.position.y;
            transform.LookAt(rectifObjective);
            //Debug.Log("Remaining Distance: " + agent.remainingDistance);
            if (Vector3.Distance(transform.position,Objetivo.transform.position) >= agent.stoppingDistance + gapObjetivo) {status--;}
        }
    }
    public void activeAttack(){AttackColidder.enabled = true;}
    public void desactiveAttack() {AttackColidder.enabled = false;}
    public void deadDestroy() { Destroy(gameObject);}
    public void deadInit() { 
        slimeColidder.enabled = false; 
        agent.isStopped = true;
        if (man != null && man.GetType().Equals(typeof(Manager))) ((Manager)man).remouveEnemy(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("And binario" + ((1 << other.gameObject.layer) & atacklayer.value)+
            "\nbinario" + (1 << other.gameObject.layer) + 
            "\nLayer binario" + (atacklayer.value));
        if (((1 << other.gameObject.layer) & atacklayer.value) != 0)
        {
            Debug.Log("Enemigo atacado");
            if (other.gameObject.GetComponent<npc>().GetType().Equals(typeof(Move)))
                ((Move)other.gameObject.GetComponent<npc>()).addLife(-damage);
            else other.gameObject.GetComponent<npc>().addLife(-damage);
        }
    }
}
