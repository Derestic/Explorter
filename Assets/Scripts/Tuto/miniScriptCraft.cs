using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniScriptCraft : MonoBehaviour
{
    [SerializeField] GameObject panelMisionA;
    [SerializeField] Color complete;
    [SerializeField] Color idle;
    [SerializeField] KeyCode key;
    [SerializeField] bool Recolector;
    [SerializeField] Move Player;
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
        if (Input.GetKeyDown(key))
        {
            gameObject.GetComponent<Image>().color = complete;
            StartCoroutine(wait());
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        panelMisionA.SetActive(true);
    }
}
