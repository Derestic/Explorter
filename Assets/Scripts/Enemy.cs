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
        run,
        attack
    }

    [Header("Control de estados")]
      [SerializeField] state status;
      GameObject ObjetivoF;
      GameObject Objetivo;
      NavMeshAgent agent;
      [SerializeField] float gapObjetivo = 0.1f;
      public Collider slimeColidder;

    [Header("Control Enemigo")]
      [SerializeField] float speed = 150f;
      [SerializeField] LayerMask atacklayer;
      public Collider AttackColidder;
      Vector3 rectifObjective;


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
        man = Manager.Instance;
        life = maxLife;
        ObjetivoF = man.getNucleo();

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

        agent.SetDestination(mejorColide.transform.position);
        if (!Objetivo.Equals(mejorColide))Objetivo = mejorColide;

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
    }

    void animationSync()
    {
        if (dead) { animator.SetBool("dead", true);  }
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
            agent.SetDestination(Objetivo.transform.position);
            status++;
        }
        else if (state.run == status)
        {
            agent.isStopped = false;
            if (agent.remainingDistance <= agent.stoppingDistance + gapObjetivo)
            {
                status++;
            }
            else if (agent.remainingDistance > maxVision && !Objetivo.Equals(ObjetivoF))
            {
                status = state.idle;
            }
            else if (Vector3.Distance(agent.destination, Objetivo.transform.position) > gapObjetivo*2)
            {
                agent.SetDestination(Objetivo.transform.position);
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
    public void deadInit() { slimeColidder.enabled = false; man.remouveEnemy(); }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & atacklayer.value) != 0)
        {
            other.gameObject.GetComponent<npc>().addLife(-damage);
        }
    }
}
