using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class castObject : MonoBehaviour
{
    [SerializeField]
    Ray ray;
    RaycastHit hit;
    public GameObject pointer;
    public GameObject defensa;
    GameObject select;
    Vector3 origen;
    Vector3 traslateD;
    bool createD = false;
    // Start is called before the first frame update
    void Start()
    {
        select = pointer;
        origen = new Vector3(transform.position.x,0,transform.position.z);
        traslateD = new Vector3(0, 0, 0);
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
            if(createD) {
                traslateD.Set(0, select.transform.localScale.y/2, 0);
                select.transform.position += traslateD; 
            }
            /*if (createD )
            {
                select.transform.position.y += select.transform.localScale.y;
            }*/
            //Debug.DrawRay(transform.position, transform.forward);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (createD)
            {
                Destroy(select);
                createD = false;
                select = pointer;
                pointer.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                createD = true;
                pointer.GetComponent<Renderer>().enabled = false;
                select = Instantiate(defensa);
                select.GetComponent<Collider>().enabled = false;
            }
        }
        if(createD && Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            select.GetComponent<Collider>().enabled = true;
            select = Instantiate(defensa);
            select.GetComponent<Collider>().enabled = false;
        }
    }
}
