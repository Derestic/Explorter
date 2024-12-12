using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : npc
{


    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) Debug.Log("Game Over");
        Manager.Instance.goDungeon(0);
    }
    public void setLife(float lifeUpdate)
    {
        life =lifeUpdate;
    }
}
