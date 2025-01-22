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

    [SerializeField] bool cheat = false;

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
      [SerializeField] Image[] invImage;
      [SerializeField] Sprite[] invSprites;


    [Header("Control muerte")]
    [SerializeField] GameObject camaraM;

    [Header("Flechita")]
      [SerializeField] GameObject flechita1;
      [SerializeField] GameObject flechita2;
      [SerializeField] GameObject flechita3;

    [Header("Luces y Dias")]
    [SerializeField] GameObject dirLigth;

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
        if(WaveControl.Instance().prep >= 0) prep = WaveControl.Instance().prep;
        if (WaveControl.Instance().vidaN > 0)
        {
            nucleo.GetComponent<Core>().setLife(WaveControl.Instance().vidaN);
            print("Core vida de " + WaveControl.Instance().vidaN.ToString()+ " y " + nucleo.GetComponent<Core>().getLife());
        }
        inventory = Inventario.Instance();
        invText = new TMP_Text[3];
        invImage = new Image[3];
        int txt = 0, img = 0;
        for (int i = 0; i < inventario.transform.childCount; i++)
        {
            Transform aux = inventario.transform.GetChild(i);
            if(aux.name.Contains("Recurso")){
                invText[txt] = aux.GetComponent<TMP_Text>();
                txt++;
            }else{
                invImage[img] = aux.GetComponent<Image>();
                img++;
            }
        }
        print("Estoy creado, con estado " + WaveControl.Instance().prep);
        nextState();
        updateCanvasInventory();
        if(WaveControl.Instance().prep >= maxprep && state == RoundState.oleada)
        {
            player.GetComponent<Move>().desactivateModes();
        }
        GameObject g;
        for(int i = 0; i < WaveControl.Instance().obj.Count; i++)
        {
            g = Instantiate(WaveControl.Instance().obj[i]);
            g.transform.position = WaveControl.Instance().posicion[i];
            g.transform.rotation = WaveControl.Instance().rotation[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cheat && Input.GetKeyDown(KeyCode.M))
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
            Debug.Log("Desactivar 2");
            player.GetComponent<Move>().desactivateModes();
        }
        else
        {
            state = RoundState.preparation;
            player.GetComponent<Move>().activateModes();
        }
        if(dirLigth != null)dirLigth.transform.Rotate(0, -360 / (maxprep + 1) * WaveControl.Instance().prep, 0);
        updateDay(WaveControl.Instance().prep + 1);
        if (state == RoundState.oleada)
        {
            updateDay(4);
            if (flechita1!=null) flechita1.SetActive(false);
            if (flechita2 != null) flechita2.SetActive(false);
            if (flechita3 != null) flechita3.SetActive(false);
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
            if (flechita1 != null) flechita1.SetActive(true);
            if (flechita2 != null) flechita2.SetActive(true);
            if (flechita3 != null) flechita3.SetActive(true);
            WaveControl.Instance().vidaN = nucleo.GetComponent<Core>().getLife();
            WaveControl.Instance().days++;
            updateDay(0);
        }
    }

    public void updateCanvasInventory()
    {
        string[] k = inventory.getKeyRecursos();
        for (int i = 0; i < invText.Length; i++)
        {
            invText[i].text = ": " + inventory.getRecurso(k[i]);
        }
        for (int i = 0; i < invImage.Length; i++)
        {
            invImage[i].sprite = invSprites[i];
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
    public void blockCheat() { cheat = false; }
}
