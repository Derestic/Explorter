using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class castObject : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    bool createD = false;

    public GameObject pointer;
    public GameObject[] defensa;
    int index = 0;
    GameObject select;

    Vector3 origen;
    Vector3 traslateD;

    Vector3 rot;
    [SerializeField]Vector3 vrot;
    // Start is called before the first frame update
    void Start()
    {
        select = pointer;
        origen = new Vector3(transform.position.x,0,transform.position.z);
        traslateD = new Vector3(0, 0, 0);
        vrot = new Vector3(0, 50, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            select.transform.position = hit.point;
            origen.Set(transform.position.x, select.transform.position.y, transform.position.z);
            select.transform.LookAt(origen);
            select.transform.Rotate(rot);
            if(createD) {
                traslateD.Set(0, select.transform.localScale.y/2, 0);
                select.transform.position += traslateD; 
            }
        }

        if (createD)
        {
            if (Input.GetKeyUp(KeyCode.E))
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
                rot.Set(0,0,0);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                index = (index + 1) % defensa.Length;
                Destroy(select);
                select = Instantiate(defensa[index]);
                select.GetComponent<Collider>().enabled = false;
            }
            else if (Input.GetKey(KeyCode.R))
            {
                rot += vrot*Time.deltaTime;
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
}
