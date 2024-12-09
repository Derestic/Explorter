using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniScript : MonoBehaviour
{
    [SerializeField] GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<npc>().isDead())
        {
            portal.SetActive(true);
        }
    }
}
