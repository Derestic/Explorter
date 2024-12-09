using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : ManagerGen
{
    private static Manager _instance;
    enum RoundState { preparation, oleada };

    [Header("Control Oleadas")]
      [SerializeField] RoundState state = RoundState.preparation;
      [SerializeField] int prep = 0;
      [SerializeField] int maxprep = 3;
      [SerializeField] GameObject nucleo;
      int countEnemies = 0;

    [Header("Control juador")]
      public GameObject player = null;

    [Header("Spawn Control")]
      public GameObject[] spawns = new GameObject[3];


    [Header("Control Canvas")]
      [SerializeField] GameObject inventario;
      [SerializeField] TMP_Text[] invText;


    [Header("Control muerte")]
    [SerializeField] GameObject camaraM;

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
        prep = WaveControl.Instance().prep;
        inventory = Inventario.Instance();
        invText = new TMP_Text[inventario.transform.childCount];
        for (int i = 0; i < inventario.transform.childCount; i++)
        {
            invText[i] = inventario.transform.GetChild(i).GetComponent<TMP_Text>();
        }
        print("Estoy creado, con estado " + state.ToString());
        nextState();
        updateCanvasInventory();
        if(state == RoundState.oleada)
            player.GetComponent<Move>().desactivateModes();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.addRecurso("Madera", 2);
        }
        updateCanvasInventory();
    }

    public void nextState()
    {
        if (prep >= maxprep)
        {
            state = RoundState.oleada;
            player.GetComponent<Move>().desactivateModes();
        }
        if (state == RoundState.oleada)
        {
            prep = 0;
            WaveControl.Instance().prep = 0;
            for (int i = 0; i < spawns.Length;i++) { spawns[i].GetComponent<spawn>().spawning = true; }
        }
        else
        {
            prep++;
            WaveControl.Instance().prep++;
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
            player.GetComponent<Move>().activateModes();
            state = RoundState.preparation;
            player.GetComponent<Move>().resetLife();
            // Resucitar jugador
            if (player != null && player.GetComponent<Move>().isDead())
            {
                ChangeCamara();
                player.GetComponent<Move>().resetLife();
            }
        }
    }

    public void updateCanvasInventory()
    {
        string[] k = inventory.getKeyRecursos();
        for (int i = 0; i < invText.Length; i++)
        {
            invText[i].text = k[i] + ": " + inventory.getRecurso(k[i]);
        }
    }

    public void ChangeCamara()
    {
        if (player.active)
        {
            player.SetActive(false);
            camaraM.SetActive(true);
        }
        else
        {
            player.SetActive(true);
            camaraM.SetActive(false);
        }
    }

}
