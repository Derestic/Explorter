using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craft : MonoBehaviour
{
    [SerializeField] int[] recursos = new int[3];

    public bool compareResources(Inventario inv)
    {
        string[] keys = inv.getKeyRecursos();
        for (int i = 0; i < recursos.Length; i++)
        {
            if (inv.getRecurso(keys[i]) < recursos[i]) return false;
        }
        return true;
    }

    public void consumeRecursos(Inventario inv)
    {
        if (compareResources(inv))
        {
            string[] keys = inv.getKeyRecursos();
            for(int i = 0;i < keys.Length; i++)
            {
                inv.addRecurso(keys[i], -recursos[i]);
            }
        }
    }
    public int[] getRecursos()
    {
        return recursos;
    }
}
