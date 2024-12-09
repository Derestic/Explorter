using System.Collections;
using System.Collections.Generic;
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
        if (other.gameObject.tag.Equals("Player") && ((
            manager.GetType().Equals(typeof(Manager))&&((Manager)manager).creationState())|| manager.GetType().Equals(typeof(DungeonManager))))
        {
            Debug.Log("Colisiono2");
            manager.goDungeon(numDungeon);
        }
    }
}
