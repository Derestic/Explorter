using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    private const float PI = 3.141592653f;
    public float spawnRadius = 1.0f;
    public int spawnQuantity = 5;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnQuantity; i++) {
            int ang = Random.Range(0, 360);
            Instantiate(enemyPrefab, new Vector3(
                this.gameObject.transform.position.x + Mathf.Sin(ang * PI / 180) * spawnRadius,
                this.gameObject.transform.position.y,
                this.gameObject.transform.position.z + Mathf.Cos(ang * PI / 180) * spawnRadius
                ), Quaternion.identity);
        }
    }
}
