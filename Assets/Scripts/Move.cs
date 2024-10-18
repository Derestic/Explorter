using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Control de personaje")]
    public Vector3 speed;
    public Camera camara;
    public float zoomSpeed = 0.1f;
    public float jumpForce = 10f;
    public GameObject father;
    public float sens = 1.0f;
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3 (0f, 0f, 0f);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        move.Set (0f, 0f, 0f);
        move += Input.GetAxisRaw("Horizontal") * speed.x * Time.deltaTime * transform.right;
        move += Input.GetAxisRaw("Vertical") * speed.z * Time.deltaTime * transform.forward;
        move.y = 0f;
        transform.localPosition = transform.localPosition + move;
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        camara.fieldOfView -= zoom*zoomSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce*100, 0));
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseX * sens, 0);
        camara.transform.Rotate(-mouseY * sens,0,0);
    }
}
