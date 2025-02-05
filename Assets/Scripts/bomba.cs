using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bomba : npc
{
    private void Awake()
    {
        sonido.Stop();
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        sonido.Pause();
        particlesE.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            StartCoroutine(explosion());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo entro");
        if (other.gameObject.tag.Equals("enemigo"))
        {
            StartCoroutine(explosion());
            Debug.Log("Detecte un enemigo");
        }
    }

    [SerializeField] LayerMask layers;
    [SerializeField] float radius = 1.0f;
    [SerializeField] float timer = 1.0f;
    [SerializeField] ParticleSystem particlesE;
    [SerializeField] AudioSource sonido;
    private IEnumerator explosion()
    {
        yield return new WaitForSeconds(timer);
        particlesE.Play();
        sonido.Play();
        Collider[] o = Physics.OverlapSphere(transform.position, radius, layers);
        for (int i = 0; i < o.Length; i++)
        {
            o[i].gameObject.GetComponent<npc>().addLife(-damage);
        }
        yield return new WaitForSeconds(0.3f);
        destroyCraft();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
