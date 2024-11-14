using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int mapSize = 5; /// Width Height Length
    public int mapSizeY = 2; /// Width Height Length
    public float mult = 5;
    public GameObject g;

    private int mapSize3;

    private int[] grid;
    private Node[] nodeList;
    private int nodeNum = 0;
    private Node startNode;



    // Start is called before the first frame update
    void Start()
    {
        mapSize3 = mapSize * mapSize * mapSizeY;
        grid = new int[mapSize3];
        nodeList = new Node[mapSize3];

        startNode = generateNewNode(Random.Range(0,mapSize), Random.Range(0, mapSizeY), Random.Range(0, mapSize));
        startNode.expand();
        
        testing();

        for (int i = 0; i < nodeNum; i++) {
            int[] pos = nodeList[i].getMapPosition();
            GameObject aux = Instantiate(g, new Vector3(pos[0] * mult, pos[1]*mult, pos[2]*mult), Quaternion.identity);
            string walls = nodeList[i].getNodeStruct();
            //Debug.Log(walls);
            if (walls[3] == '1') aux.GetComponent<RoomScript>().turnOffWall(1);
            if (walls[2] == '1') aux.GetComponent<RoomScript>().turnOffWall(2);
            if (walls[1] == '1') aux.GetComponent<RoomScript>().turnOffWall(3);
            if (walls[0] == '1') aux.GetComponent<RoomScript>().turnOffWall(4);
            Debug.Log(i+":"+walls);
        }
    }

    /** --> a[y,z,x]
     * [
     *      [
     *          [0,0],
     *          [0,0],
     *      ],
     *      [
     *          [0,0],
     *          [0,0],
     *      ]
     * ];
     */
    void testing()
    {
        string s = "";
        for (int j = 0; j < mapSizeY; j++) {
            s += "y = " + j + "\n";
            for (int k = 0; k < mapSize; k++) {
                for (int i = 0; i < mapSize; i++) {
                    s += getFromGrid(i,j,k)+" ";
                }
                s+= "\n";
            }
        }
        Debug.Log(s);
        Debug.Log(nodeNum);
    }
    public int getFromGrid(int x,int y,int z) {
        if (x >= 0 && x < mapSize && y >= 0 && y < mapSizeY && z >= 0 && z < mapSize) {
            return grid[y * mapSize * mapSize + z * mapSize + x];
        }
        return -1;
    }
    public void insertInGrid(int x, int y, int z, int data) {
        if (x >= 0 && x < mapSize && y >= 0 && y < mapSizeY && z >= 0 && z < mapSize)
        {
            grid[y * mapSize * mapSize + z * mapSize + x] = data;
        }
    }
    public Node generateNewNode(int x, int y, int z) {
        if (nodeNum < mapSize3 && getFromGrid(x, y, z) == 0)
        {
            int[] pos = new int[3];
            pos[0] = x; pos[1] = y; pos[2] = z;
            nodeList[nodeNum] = new Node(this, nodeNum + 1, pos);
            nodeNum++;
            insertInGrid(x, y, z, nodeNum);
            return nodeList[nodeNum-1];
        }
        return null;
    }
    public Node getNode(int id) {
        if (id < mapSize3) return nodeList[id-1];
        return null;
    }
}