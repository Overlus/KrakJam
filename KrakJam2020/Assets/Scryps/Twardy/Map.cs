using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public uint size;

    [SerializeField] GameObject[] roomTypes = new GameObject[3];

    public GameObject[,] gridOfRooms;

    private void Start()
    {
        if (size > 10 || size == 0)
            size = 10;

        Vector3 tempVector;

        gridOfRooms = new GameObject[size, size];

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {

                gridOfRooms[i, j] = Instantiate(roomTypes[Random.Range((int)0, 3)]);
                if(size % 2 == 0)
                    tempVector = new Vector3(i + 0.5f - (size / 2), 0, j + 0.5f - (size / 2));
                else
                    tempVector = new Vector3(i - (size / 2), 0, j - (size / 2));
                gridOfRooms[i, j].transform.position = tempVector;
            }

        }


    }


}
