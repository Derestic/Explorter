using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    enum type { bosque, cave, def};

    public int mapSize = 5; /// Width Height Length
    public int mapSizeY = 2; /// Width Height Length
    public float mult = 5;
    public GameObject roomPrefab;
    public int salasMinimas = 5;
    public GameObject doorPrefab;
    public GameObject spawnerPrefab;
    public GameObject containerObjectRef;
    [SerializeField]
    type dunType;

    private int mapSize3;
    private int roomCount;

    private int[] grid;
    private Node[] nodeList;
    private int nodeNum = 0;
    private Node startNode;
    private GameObject playerRef;
    private int resets = 0;


    // Start is called before the first frame update
    void Awake()
    {
        mapSize3 = mapSize * mapSize * mapSizeY;
        do
        {
            grid = new int[mapSize3];
            nodeList = new Node[mapSize3];
            playerRef = GameObject.Find("Player");

            resets++;
            roomCount = 0;
            nodeNum = 0;
            startNode = generateNewNode(Random.Range(0, mapSize), Random.Range(0, mapSizeY), Random.Range(0, mapSize));
            startNode.expand();
        } while (roomCount <= salasMinimas);
        Debug.Log(resets);
        Debug.Log(roomCount);
        testing();

        int[] setPosPlayer = startNode.getMapPosition();
        playerRef.transform.position = new Vector3(setPosPlayer[0] * mult, setPosPlayer[1] * mult + 1, setPosPlayer[2] * mult);

        for (int i = 0; i < nodeNum; i++) {
            int[] pos = nodeList[i].getMapPosition();
            GameObject aux = Instantiate(roomPrefab, new Vector3(pos[0] * mult, pos[1]*mult, pos[2]*mult), Quaternion.identity);
            aux.transform.parent = containerObjectRef.transform;

            string walls = nodeList[i].getNodeStruct();
            if(dunType == type.bosque) {
                if (getFromGrid(pos[0], pos[1], pos[2] - 1) > 0) aux.GetComponent<RoomScript>().turnOffWall(2);
                if (getFromGrid(pos[0], pos[1], pos[2] + 1) > 0) aux.GetComponent<RoomScript>().turnOffWall(1);
                if (getFromGrid(pos[0] - 1, pos[1], pos[2]) > 0) aux.GetComponent<RoomScript>().turnOffWall(4);
                if (getFromGrid(pos[0] + 1, pos[1], pos[2]) > 0) aux.GetComponent<RoomScript>().turnOffWall(3);
            } else {
                if (walls[3] == '1') aux.GetComponent<RoomScript>().turnOffWall(1);
                if (walls[2] == '1') aux.GetComponent<RoomScript>().turnOffWall(2);
                if (walls[1] == '1') aux.GetComponent<RoomScript>().turnOffWall(3);
                if (walls[0] == '1') aux.GetComponent<RoomScript>().turnOffWall(4);
            }
            if (i != 0 && Random.Range(0, 10) == 0)
            {
                GameObject spawner = Instantiate(spawnerPrefab, new Vector3(pos[0] * mult, pos[1] * mult, pos[2] * mult), Quaternion.identity);
                spawner.transform.parent = aux.transform;
            }
            if(i == 0){
                GameObject door = Instantiate(doorPrefab, new Vector3(setPosPlayer[0] * mult, setPosPlayer[1] * mult + 1, setPosPlayer[2] * mult), Quaternion.identity);
                door.transform.parent = aux.transform;
            }
        }
        containerObjectRef.GetComponent<NavMeshSurface>().BuildNavMesh();
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
    public void addRoomCount() {
        roomCount++;
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
