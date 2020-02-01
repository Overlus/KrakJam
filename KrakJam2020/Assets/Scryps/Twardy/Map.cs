using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public uint size;

    [SerializeField] GameObject[] roomTypes = new GameObject[3];

    Vector3 optymalizationVariable;

    public GameObject[,] gridOfRooms;

    GameObject additionalRoom;

    private void Start()
    {
        if (size % 2 == 0)
            size++;     // Labirynt - wielskość zawsze nieparzysta

        if (size > 11 || size < 5)  // Labirynt - wielkość 
            size = 11;

        Vector3 tempVector;


        additionalRoom = Instantiate(roomTypes[Random.Range((int)0, 3)]);
        additionalRoom.transform.position = Camera.main.transform.position + new Vector3(0, 10, 0); 

        gridOfRooms = new GameObject[size, size];

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {

                gridOfRooms[i, j] = Instantiate(roomTypes[Random.Range((int)0, 3)]);

                //if(size % 2 == 0)
                //    tempVector = new Vector3(i + 0.5f - (size / 2), 0, j + 0.5f - (size / 2));
                //else
                tempVector = new Vector3(i - (size / 2), 0, j - (size / 2));

                gridOfRooms[i, j].transform.parent = gameObject.transform;

                ApplayRoomRotation(gridOfRooms[i, j].transform);

                gridOfRooms[i, j].name = "Room (" + i + ", " + j + ") Rotation: " + gridOfRooms[i,j].transform.rotation.y;

                gridOfRooms[i, j].transform.position = tempVector;
            }

        }


    }


    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            ModyfyTheMaze();
    }




    void ApplayRoomRotation(Transform transformToRotate)
    {
        switch(Random.Range(0, 3))
        {
            case 1:
                optymalizationVariable = new Vector3(0, 90, 0);
                break;
            case 2:
                optymalizationVariable = new Vector3(0, 180, 0);
                break;
            default:
                optymalizationVariable = new Vector3(0, 270, 0);
                break;
        }

        transformToRotate.Rotate(optymalizationVariable);

    }


    public void ModyfyTheMaze()
    {
        int value = Random.Range((int)-(size - 1), (int)size - 1); // 0 - 6 Tested
        bool isLine = (Random.Range(0, 2) % 2 == 1);

        int absValue = Mathf.Abs(value);
        GameObject buffer;

        Debug.Log(isLine);


        if (isLine)
        {

            if (value >= 0)
            {

                buffer = additionalRoom;
                additionalRoom = gridOfRooms[absValue, size - 1];

                for (int i = (int)size - 2; i >= 0; i--)
                {
                        Debug.Log(i);

                    if (i != 0)
                    {
                        Debug.Log(size - 1);
                        gridOfRooms[absValue, size - 1] = gridOfRooms[absValue, size - 2];
                    }
                    else
                    {
                        Debug.Log(i);

                        gridOfRooms[absValue, 0] = buffer;
                    }

                }

            }
            else
            {

                //buffer = gridOfRooms[absValue, size - 1];
                //additionalRoom = gridOfRooms[absValue, 0];

                //for (int i = (int)size - 2; i > 1; i--)
                //{


                //}

            }


        }
        else
        {



        }


        additionalRoom.transform.position = Camera.main.transform.position + new Vector3(0, 10, 0);
    }


}
