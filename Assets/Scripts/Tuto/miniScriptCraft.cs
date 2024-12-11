using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniScriptCraft : MonoBehaviour
{
    [SerializeField] protected GameObject portal;
    [SerializeField] protected GameObject panelMisionA;
    [SerializeField] protected Color complete;
    [SerializeField] protected Color idle;
    [SerializeField] protected KeyCode key;
    [SerializeField] protected bool Recolector;
    [SerializeField] protected Move Player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().color = idle;
        if (Recolector)
        {
            Player.SetRecolector(true);
        }
        else
        {
            Player.SetRecolector(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Tecla: " + key.ToString());
        if (key!=KeyCode.None && Input.GetKeyDown(key))
        {
            updateMision();
        }
    }

    protected IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        panelMisionA.SetActive(true);
    }

    public void updateMision()
    {
        gameObject.GetComponent<Image>().color = complete;
        StartCoroutine(wait());
        if(portal!=null)portal.SetActive(true);
    }
}
