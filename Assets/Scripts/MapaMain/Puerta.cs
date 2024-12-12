using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] int numDungeon;
    [SerializeField] ManagerGen manager;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisiono");
        
        if (other.gameObject.tag.Equals("Player"))
        {
            if (manager.GetType().Equals(typeof(Manager)) && !((Manager)manager).creationState())
            {
                Debug.Log("No se puede entrar");
            }
            else
            {
                Debug.Log("Colisiono2");
                manager.goDungeon(numDungeon);
            }
        }
    }
}
