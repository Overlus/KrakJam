using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public uint size;

    [SerializeField] GameObject[] roomTypes = new GameObject[3];

    Vector3 optymalizationVariable;

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

                gridOfRooms[i, j].transform.parent = gameObject.transform;

                ApplayRoomRotation(gridOfRooms[i, j].transform);

                gridOfRooms[i, j].name = "Room (" + i + ", " + j + ") Rotation: " + gridOfRooms[i,j].transform.rotation.y;

                gridOfRooms[i, j].transform.position = tempVector;
            }

        }


    }



    void ApplayRoomRotation(Transform transformToRotate)
    {
        switch(Random.Range(0, 3))
        {
            case 1:
                optymalizationVariable = new Vector3(0, 90, 0);
                Debug.Log(1);
                break;
            case 2:
                optymalizationVariable = new Vector3(0, 180, 0);
                Debug.Log(2);
                break;
            default:
                optymalizationVariable = new Vector3(0, 270, 0);
                Debug.Log(0);
                break;
        }

        transformToRotate.Rotate(optymalizationVariable);

    }


}
