using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtenerRecursosPlayer : MonoBehaviour
{
    public DungeonManager managerRef;
    public string tagType;
    void OnTriggerEnter(Collider other) {
        if (other.tag == tagType) {
            managerRef.recolectarRecurso(other.GetComponent<Recurso>().getRecurso(), other.GetComponent<Recurso>().getQuantity());
            //Debug.Log(other.GetComponent<Recurso>().getRecurso()+" "+ other.GetComponent<Recurso>().getQuantity());
        }
    }
}
