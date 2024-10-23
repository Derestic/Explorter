using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{

    [Header("ManagerLink")]
    Manager man; 

    [Header("Control Vida")]
    float life = 100;
    [SerializeField] float maxLife = 100;

    // Start is called before the first frame update
    void Start()
    {
        man = Manager.Instance;
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addLife(float extra)
    {
        life += extra;
        if (life < 0)
        {
            man.gameOver();
        }
    }
}
