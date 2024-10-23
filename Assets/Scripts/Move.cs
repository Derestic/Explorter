using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("ManagerLink")]
    Manager man;
    [Header("Control Movimiento")]
    [SerializeField] Vector3 speed;
    [SerializeField] float zoomSpeed = 0.1f;
    [SerializeField] float jumpForce = 10f;
    Vector3 move;

    [Header("Control Camara")]
    public Camera camara;
    [SerializeField]float sens = 1.0f;

    [Header("Control Vida")]
    float life = 100;
    [SerializeField] float maxLife = 100;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        man = Manager.Instance;
        life = maxLife;
        move = new Vector3 (0f, 0f, 0f);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            movement();

            jump();

            cameraMovement();

            cameraZoom();
        }
    }
    void movement()
    {
        move.Set(0f, 0f, 0f);
        move += Input.GetAxisRaw("Horizontal") * speed.x * Time.deltaTime * transform.right;
        move += Input.GetAxisRaw("Vertical") * speed.z * Time.deltaTime * transform.forward;
        // move.y = 0f;
        transform.localPosition = transform.localPosition + move;
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce * 100, 0));
        }
    }

    void cameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseX * sens, 0);
        camara.transform.Rotate(-mouseY * sens, 0, 0);
    }

    void cameraZoom()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        camara.fieldOfView -= zoom * zoomSpeed;
    }

    public void addLife(float extra)
    {
        life += extra;
        if(life < 0)
        {
            print("Dead");
            dead = true;
        }
    }

    public bool isDead() { return dead; }

    public void resetLife()
    {
        life = maxLife;
        dead = false;
    }
}
