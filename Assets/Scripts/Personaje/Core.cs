using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : npc
{


    // Start is called before the first frame update
    void Start()
    {
        if (WaveControl.Instance().vidaN < 0) life = maxLife;
        else life = WaveControl.Instance().vidaN;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) { 
            Debug.Log("Game Over");
            Manager.Instance.goDungeon(4);
        }
        WaveControl.Instance().vidaN = life;
    }
    public void setLife(float lifeUpdate)
    {
        life = lifeUpdate;
    }
}
