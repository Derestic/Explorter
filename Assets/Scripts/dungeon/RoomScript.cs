using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public bool wall1 = true, wall2 = true, wall3 = true, wall4 = true;
    // Start is called before the first frame update
    void Start()
    {
        turnWalls();
    }

    void turnWalls() {
        if(wall1) this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        else this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        if(wall2) this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        else this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        if(wall3) this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        else this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        if(wall4) this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        else this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
    }
    public void turnOffWall(int i) {
        if (i == 1) wall1 = false; 
        if (i == 2) wall2 = false; 
        if (i == 3) wall3 = false; 
        if (i == 4) wall4 = false; 
    }
}
