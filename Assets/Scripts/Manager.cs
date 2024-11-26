using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    enum RoundState { preparation, oleada };

    [Header("Control Oleadas")]
      [SerializeField] RoundState state = RoundState.preparation;
      int prep = 0;
      [SerializeField] int maxprep = 3;
      [SerializeField] GameObject nucleo;
      int countEnemies = 0;

    [Header("Control juador")]
      public GameObject player = null;

    [Header("Spawn Control")]
      public GameObject[] spawns = new GameObject[3];

    public static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No hay manager");
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        print("Estoy creado, con estado " + state.ToString());
        nextState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextState()
    {
        if (state == RoundState.oleada)
        {
            prep = 0;
            for (int i = 0; i < spawns.Length;i++) { spawns[i].GetComponent<spawn>().spawning = true; }
        }
        else
        {
            prep++;
            if (prep >= maxprep)
            {
                state = RoundState.oleada;
            }
        }
        // Resucitar jugador
        if (player != null && player.GetComponent<Move>().isDead())
        {
            player.GetComponent<Move>().resetLife();
        }
    }

    public void gameOver()
    {
        print("Game Over");
    }
    public bool creationState()
    {
        return state == RoundState.preparation;
    }

    public GameObject getNucleo()
    {
        return nucleo;
    }

    public void addEnemy()
    {
        countEnemies++;
    }
    public void remouveEnemy()
    {
        countEnemies--;
        Debug.Log("Hay: " + countEnemies + "Enemigos");
        if(countEnemies <= 0)
        {
            state = RoundState.preparation;
        }
    }
}
