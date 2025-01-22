using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class castObject : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    bool createD = false;


    [Header("Manager Link")]
    ManagerGen man;

    [Header("Objects Control")]
    public GameObject pointer;
    public GameObject[] defensa;
    int index = 0;
    GameObject select;
    [SerializeField] float maxDistancePointer = 30f;
    [SerializeField] LayerMask layerPointer;
    private bool crafteable;

    Vector3 origen;
    Vector3 traslateD;

    [Header("Rotation Control")]
    Vector3 rot;
    [SerializeField]Vector3 vrot;

    [Header("Creation Buttons")]
    [SerializeField] KeyCode rotateSelect = KeyCode.R;
    [SerializeField] KeyCode changeSelect = KeyCode.Q;
    // Start is called before the first frame update
    [SerializeField] miniScriptCraft mision;
    void Start()
    {
        man = Manager.Instance;
        select = pointer;
        origen = new Vector3(transform.position.x,0,transform.position.z);
        traslateD = new Vector3(0, 0, 0);
        vrot = new Vector3(0, 50, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // RayCast
        castCreation();
        if (man != null)
        {
                operateCreation();
        }
            
    }

    void operateCreation()
    {
        if (createD)
        {
            if (Input.GetMouseButtonUp((int)MouseButton.Left) && crafteable)
            {
                select.GetComponent<craft>().consumeRecursos(Inventario.Instance());
                select.GetComponent<Collider>().enabled = true;
                select.GetComponent<NavMeshObstacle>().enabled = true;

                WaveControl.Instance().obj.Add(defensa[index]);
                WaveControl.Instance().posicion.Add(select.transform.position);
                WaveControl.Instance().rotation.Add(select.transform.rotation);

                select = Instantiate(defensa[index]);
                select.GetComponent<Collider>().enabled = false;
                select.GetComponent<NavMeshObstacle>().enabled = false;
                crafteable = select.GetComponent<craft>().compareResources(Inventario.Instance());
                colorPanelRecursos(crafteable);
                if (mision != null) mision.updateMision();
            }
            else if (Input.GetKeyDown(changeSelect))
            {
                index = (index + 1) % defensa.Length;
                Destroy(select);
                select = Instantiate(defensa[index]);
                select.GetComponent<Collider>().enabled = false;
                select.GetComponent<NavMeshObstacle>().enabled = false;
                crafteable = select.GetComponent<craft>().compareResources(Inventario.Instance());
                colorPanelRecursos(crafteable);
                updateTableRecursos(select.GetComponent<craft>().getRecursos(), defensa[index].name);
            }
            else if (Input.GetKey(rotateSelect))
            {
                rot += vrot * Time.deltaTime;
            }
        }
    }

    public void changeMode()
    {
        if (createD)
        {
            Destroy(select);
            createD = false;
            select = pointer;
            pointer.SetActive(true);
            activatePanlRecursos(false);
        }
        else{
            createD = true;
            pointer.SetActive(false);
            select = Instantiate(defensa[index]);
            select.GetComponent<Collider>().enabled = false;
            select.GetComponent<NavMeshObstacle>().enabled = false;
            crafteable = select.GetComponent<craft>().compareResources(Inventario.Instance());
            colorPanelRecursos(crafteable);
            activatePanlRecursos(true);
            updateTableRecursos(select.GetComponent<craft>().getRecursos(), defensa[index].name);
        }
    }
    Vector3 punterito;
    void castCreation()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit,maxDistancePointer, layerPointer))
        {
            punterito = hit.point;
            punterito.y = 0;
            select.transform.position = punterito;
        }
        else
        {
            punterito = transform.position + transform.forward*maxDistancePointer;
            punterito.y = 0;
            select.transform.position = punterito;
        }
        print("Vector pos Craft: " + select.transform.position);
        origen.Set(transform.position.x, select.transform.position.y, transform.position.z);
        select.transform.LookAt(origen);
        select.transform.Rotate(rot);
        if (createD)
        {
            traslateD.Set(0, select.transform.localScale.y / 2, 0);
            select.transform.position += traslateD;
        }
    }

    [Header("Control Recursos Objetos")]
    [SerializeField] GameObject panelRecurso;
    [SerializeField] TMP_Text nombreRecurso;
    [SerializeField] TMP_Text[] recursos;
    [SerializeField] Color red;
    [SerializeField] Color green;

    void updateTableRecursos(int[] rec, string name)
    {
        nombreRecurso.text = name;
        for (int i = 0; i < recursos.Length; i++)
        {
            recursos[i].text = rec[i].ToString();
        }
    }
    void activatePanlRecursos(bool b)
    {
        if(b)panelRecurso.SetActive(true);
        else panelRecurso.SetActive(false);
    }
    void colorPanelRecursos(bool b)
    {
        if(b)panelRecurso.GetComponent<Image>().color = green;
        else panelRecurso.GetComponent<Image>().color = red;
    }
}
