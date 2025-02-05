﻿using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : npc
{

    [Header("ManagerLink")]
    [SerializeField] protected ManagerGen man;

    enum Modos { Contruccion, Ataque, Recoleccion };
    [Header("Control Movimiento")]
    [SerializeField] Vector3 speed;
    [SerializeField] float zoomSpeed = 0.1f;
    [SerializeField] float jumpForce = 10f;
    bool jumpControl = true;
    Vector3 move;

    [Header("Control Camara")]
    public Camera camara;
    [SerializeField]float sens = 1.0f;

    [Header("Control ataque")]
    public KeyCode ataque;
    Animator anim;
    public GameObject weapon;

    [Header("Control modos")]
    [SerializeField] KeyCode changeMode = KeyCode.E;
    Modos mode = Modos.Ataque;
    [SerializeField] GameObject[] arma = new GameObject[3];
    [SerializeField] castObject constructor;
    bool recoletor;
    [SerializeField]bool activeChangeMode = true;

    [Header("Audio")]
    [SerializeField] AudioClip pasos;
    [SerializeField] AudioClip swingEspada;
    [SerializeField] AudioClip golpeHacha;
    AudioSource audioSourcePasos;
    [SerializeField] AudioSource audioSourceEspada;
    [SerializeField] AudioSource audioSourceHacha;
    //[SerializeField] AudioSource audioSourceMartillo;
    
    private bool PAUSE = false;
    [Header("GUIS")]
    [SerializeField] GameObject gui1;
    [SerializeField] GameObject gui2;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        move = new Vector3 (0f, 0f, 0f);
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        if (constructor == null) recoletor = true; else recoletor = false;
        audioSourcePasos = GetComponent<AudioSource>();
        audioSourcePasos.clip = pasos;
        audioSourceEspada.clip = swingEspada;
        audioSourceHacha.clip = golpeHacha;
    }
    public void SetRecolector(bool b)
    {
        recoletor = b;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(0);
            WaveControl.Instance().reset();
        }
        PAUSE = (int)Time.timeScale == 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuControl.zSwapPanels(gui1);
            MenuControl.zSwapPanels(gui2);
            MenuControl.zPauseTime();
        }
        if (!dead && !PAUSE)
        {
            if (!anim.GetBool("ataque")) attack();

            jump();

            cameraMovement();

            //cameraZoom();

            if (Input.GetKeyDown(changeMode) && activeChangeMode)
            {
                modes();
            }
        }
        //if (anim.GetBool("ataque")) { anim.SetBool("ataque", false); }
    }
    [SerializeField] float run = 7;
    private void FixedUpdate()
    {
        if (!dead && !PAUSE)
        {
            move.Set(0f, gameObject.GetComponent<Rigidbody>().velocity.y, 0f);
            float Sx = 1;
            float Sz = 1;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sx = run;
                Sz = run;
            }
            move += Input.GetAxisRaw("Horizontal") * speed.x * Sx * transform.right;
            move += Input.GetAxisRaw("Vertical") * speed.z * Sz * transform.forward;
            gameObject.GetComponent<Rigidbody>().velocity = move;
            //gameObject.GetComponent<Rigidbody>().(Input.GetAxis("Vertical") * speed.x, 0.0f,Input.GetAxis("Horizontal") * speed.z);
            if ((move.x != 0f || move.z != 0f) && Mathf.Abs(move.y) <= 0.00001f)
            {
                if (!audioSourcePasos.isPlaying) audioSourcePasos.Play();
            }
            else {
                if (audioSourcePasos.isPlaying) audioSourcePasos.Pause();
            }
        }
    }

    void modes()
    {
        weapon.SetActive(false);
        getAnimIdle();
        switch (mode)
        {
            case Modos.Ataque:
                if (recoletor) { 
                    mode = Modos.Recoleccion;
                    weapon = arma[2]; 
                }
                else
                {
                    constructor.changeMode();
                    mode = Modos.Contruccion;
                    weapon = arma[1];
                }
                break;
            case Modos.Contruccion:
                constructor.changeMode();
                mode = Modos.Ataque;
                weapon = arma[0];
                break;
            case Modos.Recoleccion:
                mode = Modos.Ataque;
                weapon = arma[0];
                break;
        }
        weapon.SetActive(true);
        Debug.Log("Mode: " + mode +
            "\n Combate: " + anim.GetBool("Combate") +
            "\n Construct: " + anim.GetBool("Construct") +
            "\n Recolectar: " + anim.GetBool("Recolectar")+ 
            "\n Atacando: " + anim.GetBool("ataque"));
        //anim = GetComponent<Animator>();
    }
    public void setAtaque()
    {
        weapon.SetActive(false);
        if (mode != Modos.Ataque)
        {
            if (Modos.Contruccion == mode) constructor.changeMode();
            mode = Modos.Ataque;
            weapon = arma[0];
        }
        weapon.SetActive(true);
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
        if (Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            anim.SetBool("ataque",true);
            if (mode == Modos.Ataque && !audioSourceEspada.isPlaying) audioSourceEspada.Play();
            if (mode == Modos.Recoleccion && !audioSourceHacha.isPlaying) audioSourceHacha.Play();
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
        if (Physics.BoxCast(transform.position, transform.localScale / 2, -transform.up, out RaycastHit hit, Quaternion.identity, transform.localScale.y / 2) && Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce , 0),ForceMode.Impulse);
        }
    }

    void cameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseX * sens, 0);
        //Debug.Log("Rotation x: " + Mathf.Abs(camara.transform.rotation.x));
        if(Mathf.Abs(camara.transform.rotation.x - Mathf.Sign(mouseY)*0.1f) <0.6f) 
            camara.transform.Rotate(-mouseY * sens, 0, 0);

    }

    void cameraZoom()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        camara.fieldOfView -= zoom * zoomSpeed;
    }

    public void activateModes()
    {
        activeChangeMode = true;
    }
    public void desactivateModes()
    {
        activeChangeMode = false;
    }
    public new void addLife(float extra)
    {
        life += extra;
        Debug.Log("Damage de: " + extra);
        if (life <= 0)
        {
            Debug.Log("new Dead");
            dead = true;
            if (man.GetType().Equals(typeof(Manager)))
            {
                Debug.Log("Personaje Dead");
                ((Manager)man).ChangeCamara();
            }
            else man.goDungeon(0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireCube(transform.position-transform.up*(transform.localScale.y / 2), transform.localScale / 2);
    }
}
