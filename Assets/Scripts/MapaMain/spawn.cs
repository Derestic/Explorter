using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class spawn : MonoBehaviour
{
    [Header("Spawn Control")]
      public bool spawning;
      [SerializeField] int numSpawns = 5;
      int countSpawn = 0;
      public GameObject enemie;
      [SerializeField] float spawnGap = 1.0f;
      float countGap = 0;
      GameObject inst;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning)
        {
            if (countGap < spawnGap) countGap += Time.deltaTime;
            else
            {
                Debug.Log("Se han spawneado: " + countSpawn + "/" + numSpawns);
                countSpawn++;
                countGap = 0;
                inst = Instantiate(enemie);
                inst.GetComponent<Enemy>().man = Manager.Instance;
                inst.transform.position = transform.position;
                Manager.Instance.addEnemy();
            }
            if (countSpawn >= numSpawns) { 
                spawning = false; 
                countSpawn = 0;
                numSpawns+=3;
            }
        }
    }

}
