using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPortalScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Hey");
        }
    }
}
