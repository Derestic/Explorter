using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{

    [Header("Control Vida")]
    protected float life;
    [SerializeField] protected float maxLife = 1;
    protected bool dead = false;
    [SerializeField] protected float damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void addLife(float extra)
    {
        life += extra;
        Debug.Log("Daño de: " + extra);
        if (life < 0)
        {
            Debug.Log("Dead");
            dead = true;
        }
    }

    public bool isDead() { return dead; }

    public void resetLife()
    {
        life = maxLife;
        dead = false;
    }
    public float getDamage()
    {
        return damage;
    }
}
