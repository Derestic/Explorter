using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Move : npc
{
    [Header("Control Movimiento")]
    [SerializeField] Vector3 speed;
    [SerializeField] float zoomSpeed = 0.1f;
    [SerializeField] float jumpForce = 10f;
    Vector3 move;

    [Header("Control Camara")]
    public Camera camara;
    [SerializeField]float sens = 1.0f;

    [Header("Control ataque")]
    public KeyCode ataque;
    Animator anim;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        man = Manager.Instance;
        life = maxLife;
        move = new Vector3 (0f, 0f, 0f);
        Cursor.visible = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            //movement();

            attack();

            jump();

            cameraMovement();

            cameraZoom();
        }
    }

    private void FixedUpdate()
    {
        move.Set(0f, gameObject.GetComponent<Rigidbody>().velocity.y, 0f);
        move += Input.GetAxisRaw("Horizontal") * speed.x * transform.right;
        move += Input.GetAxisRaw("Vertical") * speed.z * transform.forward;
        gameObject.GetComponent<Rigidbody>().velocity = move;
        //gameObject.GetComponent<Rigidbody>().(Input.GetAxis("Vertical") * speed.x, 0.0f,Input.GetAxis("Horizontal") * speed.z);

    }

    void movement()
    {
        move.Set(0f, 0f, 0f);
        move += Input.GetAxisRaw("Horizontal") * speed.x * Time.deltaTime * transform.right;
        move += Input.GetAxisRaw("Vertical") * speed.z * Time.deltaTime * transform.forward;
        // move.y = 0f;
        transform.localPosition = transform.localPosition + move;
    }

    void attack()
    {
        if (Input.GetKeyDown(ataque))
        {
            anim.SetBool("ataque",true);
        }
    }

    public void getAnimIdle()
    {
        anim.SetBool("ataque", false);
    }

    public void activeCollider() { weapon.GetComponent<Collider>().enabled = true; }
    public void desactiveCollider() { weapon.GetComponent<Collider>().enabled = false; }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce , 0),ForceMode.Impulse);
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
}
