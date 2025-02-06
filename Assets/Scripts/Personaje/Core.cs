using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : npc
{


    // Start is called before the first frame update
    void Awake()
    {
        if (WaveControl.Instance().vidaN < 0)
        {
            Debug.Log("Se seteo la vida");
            life = maxLife;
            WaveControl.Instance().vidaN = maxLife;
        }
        else
        {
            life = WaveControl.Instance().vidaN;
        }
    }

    protected new void Start()
    {
        life = WaveControl.Instance().vidaN;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) { 
            Debug.Log("Game Over");
            Manager.Instance.goDungeon(3);
        }
        Debug.Log("vida" + life);
    }
    public void setLife(float lifeUpdate)
    {
        life = lifeUpdate;
    }
    public new void addLife(float extra)
    {
        base.addLife(extra);
        WaveControl.Instance().vidaN += extra;
    }
}
