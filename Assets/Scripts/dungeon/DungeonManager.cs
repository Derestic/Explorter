using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : ManagerGen
{
    private static DungeonManager _instance;

    [Header("Control jugador")]
    public GameObject player = null;

    [Header("Control Canvas")]
    [SerializeField] GameObject inventario;
    [SerializeField] TMP_Text[] invText;


    public static DungeonManager Instance
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
        inventory = Inventario.Instance();
        invText = new TMP_Text[inventario.transform.childCount];
        for (int i = 0; i < inventario.transform.childCount; i++)
        {
            invText[i] = inventario.transform.GetChild(i).GetComponent<TMP_Text>();
        }
        updateCanvasInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.addRecurso("Madera", 2);
            updateCanvasInventory();
        }
    }
    public void recolectarRecurso(string recurso, int cantidad) {
        inventory.addRecurso(recurso, cantidad);
        updateCanvasInventory();
    }

    public void gameOver()
    {
        print("Game Over");
    }


    public void updateCanvasInventory()
    {
        string[] k = inventory.getKeyRecursos();
        for (int i = 0; i < invText.Length; i++)
        {
            invText[i].text = k[i] + ": " + inventory.getRecurso(k[i]);
        }
    }

}
