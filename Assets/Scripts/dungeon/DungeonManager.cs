using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    int[] rec = new int[3];
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
        invText = new TMP_Text[inventario.transform.childCount/2];
        int j = 0;
        for (int i = 1; i < inventario.transform.childCount; i+=2)
        {
            invText[j] = inventario.transform.GetChild(i).GetComponent<TMP_Text>();
            j++;
        }
        updateCanvasInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Move>().isDead())
        {
            this.goDungeon(0);
            string[] a = inventory.getKeyRecursos();
            for (int i = 0; i < rec.Length; i++)
            {
                inventory.addRecurso(a[i], rec[i]);
            }
        }
    }
    public void recolectarRecurso(string recurso, int cantidad) {
        inventory.addRecurso(recurso, cantidad);
        string[] a = inventory.getKeyRecursos();
        for (int i = 0;i < rec.Length; i++)
        {
            if (a[i] == recurso)
            {
                rec[i] += cantidad;
            }
        }
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
            invText[i].text =": " + inventory.getRecurso(k[i]);
        }
    }

}
