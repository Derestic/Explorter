using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    //Node Data
    private Dungeon dungeonLink;
    private Node[] links;
    private int id;
    private int[] mapPosition;

    public Node(Dungeon d, int id, int[] position)
    {
        links = new Node[6]; // 0:up(-y) 1:down(+y) 2:left(-x) 3:right(+x) 4:front(-z) 5:back(+z)
        this.dungeonLink = d;
        this.id = id;
        this.mapPosition = position;
        dungeonLink.addRoomCount();
    }
    public int getId() { return this.id; }
    public int[] getMapPosition() { return this.mapPosition; }
    public string getNodeStruct() {
        string s = "";
        if (links[2] != null) s += "1"; // -X
        else s += "0";
        if (links[3] != null) s += "1"; // +X
        else s += "0";
        if (links[4] != null) s += "1"; // -Z
        else s += "0";
        if (links[5] != null) s += "1"; // +Z
        else s += "0";
        return s;
    }
    public void linkAux(Node aux, int i) {
        this.links[i] = aux;
    }
    public void expand() {
        //Debug.Log("expanded:" + this.mapPosition[0]+", " + this.mapPosition[1]+", " + this.mapPosition[2]);
        // Y
        if (links[0] == null && Random.Range(0.0f, 1.0f) < 0.5f) 
        {
            links[0] = dungeonLink.generateNewNode(mapPosition[0], mapPosition[1] - 1, mapPosition[2]);
            if (links[0] != null)
            {
                links[0].linkAux(this, 1);
                links[0].expand();
            }
        }
        if (links[1] == null && Random.Range(0.0f, 1.0f) < 0.5f) 
        {
            links[1] = dungeonLink.generateNewNode(mapPosition[0], mapPosition[1] + 1, mapPosition[2]);
            if (links[1] != null)
            {
                links[1].linkAux(this, 0);
                links[1].expand();
            }
        }

        // X
        if (links[2] == null && Random.Range(0.0f, 1.0f) < 0.5f)
        {
            links[2] = dungeonLink.generateNewNode(mapPosition[0] - 1, mapPosition[1], mapPosition[2]);
            if (links[2] != null)
            {
                links[2].linkAux(this, 3);
                links[2].expand();
            }
        }
        if (links[3] == null && Random.Range(0.0f, 1.0f) < 0.5f)
        {
            links[3] = dungeonLink.generateNewNode(mapPosition[0] + 1, mapPosition[1], mapPosition[2]);
            if (links[3] != null)
            {

                links[3].linkAux(this, 2);
                links[3].expand();
            }
        }

        // Z
        if (links[4] == null && Random.Range(0.0f, 1.0f) < 0.5f) 
        {
            links[4] = dungeonLink.generateNewNode(mapPosition[0], mapPosition[1], mapPosition[2]-1);
            if (links[4] != null)
            {
                links[4].linkAux(this, 5);
                links[4].expand();
            }
        }
        if (links[5] == null && Random.Range(0.0f, 1.0f) < 0.5f) 
        {
            links[5] = dungeonLink.generateNewNode(mapPosition[0], mapPosition[1], mapPosition[2]+1);
            if (links[5] != null)
            {
                links[5].linkAux(this, 4);
                links[5].expand();
            }
        }
    }
}
