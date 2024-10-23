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

    [Header("Control juador")]
    public GameObject player;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextState()
    {
        if (state == RoundState.oleada)
        {
            state = RoundState.preparation;
            prep = 0;
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
        if (player.GetComponent<Move>().isDead())
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
}
