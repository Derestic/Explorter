using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class castObject : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    bool createD = false;


    [Header("Manager Link")]
    Manager man;

    [Header("Objects Control")]
    public GameObject pointer;
    public GameObject[] defensa;
    int index = 0;
    GameObject select;

    Vector3 origen;
    Vector3 traslateD;

    [Header("Rotation Control")]
    Vector3 rot;
    [SerializeField]Vector3 vrot;

    [Header("Creation Buttons")]
    [SerializeField] KeyCode selectMode = KeyCode.E;
    [SerializeField] KeyCode rotateSelect = KeyCode.R;
    [SerializeField] KeyCode changeSelect = KeyCode.Q;
    // Start is called before the first frame update

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
            bool b = man.creationState();
            // use cast
            if (b)
                operateCreation();
        }
            
    }

    void operateCreation()
    {
        if (createD)
        {
            if (Input.GetKeyUp(selectMode))
            {
                Destroy(select);
                createD = false;
                select = pointer;
                pointer.GetComponent<Renderer>().enabled = true;
            }
            else if (Input.GetMouseButtonUp((int)MouseButton.Left))
            {
                select.GetComponent<Collider>().enabled = true;
                select = Instantiate(defensa[index]);
                select.GetComponent<Collider>().enabled = false;
            }
            else if (Input.GetKeyDown(changeSelect))
            {
                index = (index + 1) % defensa.Length;
                Destroy(select);
                select = Instantiate(defensa[index]);
                select.GetComponent<Collider>().enabled = false;
            }
            else if (Input.GetKey(rotateSelect))
            {
                rot += vrot * Time.deltaTime;
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            createD = true;
            pointer.GetComponent<Renderer>().enabled = false;
            select = Instantiate(defensa[index]);
            select.GetComponent<Collider>().enabled = false;
        }
    }

    void castCreation()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            select.transform.position = hit.point;
            origen.Set(transform.position.x, select.transform.position.y, transform.position.z);
            select.transform.LookAt(origen);
            select.transform.Rotate(rot);
            if (createD)
            {
                traslateD.Set(0, select.transform.localScale.y / 2, 0);
                select.transform.position += traslateD;
            }
        }
    }
}
