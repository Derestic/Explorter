using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] int numDungeon;
    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisiono");
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Colisiono2");
            manager.goDungeon(numDungeon);
        }
    }
}
