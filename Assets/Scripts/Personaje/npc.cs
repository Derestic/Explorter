using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{

    [Header("Control Vida")]
    [SerializeField] protected float life;
    [SerializeField] protected float maxLife = 1;
    protected bool dead = false;
    [SerializeField] protected float damage = 1;

    private int index = -10;

    // Start is called before the first frame update
    protected void Start()
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
        Debug.Log("Dano de: " + extra);
        if (life <= 0)
        {
            Debug.Log("Dead");
            dead = true; 
            if (index > -1)
            {
                Debug.Log("Muerte2");
                destroyCraft();
            }
        }
        
    }
    public float getLife()
    {
        return life;
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

    public void setIndex(int i)
    {
        index = i;
    } 

    public void destroyCraft()
    {
        WaveControl.Instance().posicion.Remove(index);
        WaveControl.Instance().rotation.Remove(index);
        WaveControl.Instance().obj.Remove(index);
        Destroy(gameObject);
    }
}
