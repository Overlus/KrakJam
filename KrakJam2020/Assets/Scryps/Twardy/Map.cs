using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] uint _size_;

    static public uint size;

    [SerializeField] GameObject[] roomTypes = new GameObject[3];

    Vector3 optymalizationVariable;

    static public GameObject[,] gridOfRooms;

    GameObject additionalRoom;

    [SerializeField] GameObject portal;

    public GameObject repItem;

    static public GameObject repparingItem;


    private void Start()
    {
        size = _size_;

        repparingItem = repItem;

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

                gridOfRooms[i, j] = Instantiate(roomTypes[Random.Range(0, 3)]);

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


        SpawnItemToReppairPortal(size, repparingItem, gridOfRooms);

        SpawnPortal();

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //Debug.Log("XD");
           MoveRow(additionalRoom, Random.Range(0, (int) size )); //podać obiekt który będzie podmieniany.
           MoveRow(additionalRoom, Random.Range(0, (int)size));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //Debug.Log("XD");
            MoveColumns(additionalRoom, Random.Range(0, (int)size)); //podać obiekt który będzie podmieniany.
            MoveColumns(additionalRoom, Random.Range(0, (int)size));
        }
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

    private void MoveColumns(GameObject extraRoom, int columnNumber )
    {

        GameObject buffer = gridOfRooms[columnNumber, 0]; 

        for (int i = 0; i < size - 1; i++) 
        {
            gridOfRooms[columnNumber, i] = gridOfRooms[columnNumber, i + 1]; 

            optymalizationVariable = new Vector3(columnNumber  - (size / 2), 0, i - (size / 2)); 
            gridOfRooms[columnNumber, i].transform.position = optymalizationVariable;      
        }
        gridOfRooms[columnNumber, size - 1] = extraRoom;  
        optymalizationVariable = new Vector3(columnNumber - (size / 2), 0,  (size - 1) - (size / 2)); 
        gridOfRooms[columnNumber, size - 1].transform.position = optymalizationVariable; 

        additionalRoom = buffer;

    }

    private void MoveRow(GameObject extraRoom, int rowNumber)
    {

        GameObject buffer = gridOfRooms[0, rowNumber];  // zapisz tymczasowo pokój maxymalnie po lewej

        for (int i = 0; i < size - 1; i++)    // indexy od 0 do (size - 1)
        {
            gridOfRooms[i, rowNumber] = gridOfRooms[i + 1, rowNumber]; // przesuń wszystkie pokoje oprócz ostatniego o 1 w lewo (pokój 0,0 wypada do buffera wyżej) (bierzemy pokój z prawej i wsadzamy go w index po lewej)   

            optymalizationVariable = new Vector3(i - (size / 2), 0, rowNumber - (size / 2));    // weź rząd oraz kolumnę i policz dla nich pozycję
            gridOfRooms[i, rowNumber].transform.position = optymalizationVariable;      // przypisz policzoną pozycję
        }
        gridOfRooms[size - 1, rowNumber] = extraRoom;   // pokój maxymalnie w prawo staje się dodatkowym pokojem
        optymalizationVariable = new Vector3((size - 1) - (size / 2), 0, rowNumber - (size / 2));  // policz pozycję pokoju
        gridOfRooms[size - 1, rowNumber].transform.position = optymalizationVariable; // przypisz pozycję

        additionalRoom = buffer;
    }
    

    static public void SpawnItemToReppairPortal( uint _size, GameObject _repparingItem, GameObject[,] _gridOfRooms)
    {
        int i = Random.Range(0, (int)_size);
        int j = Random.Range(0, (int)_size);

        var item = Instantiate(_repparingItem);
        item.transform.position = _gridOfRooms[i, j].transform.position + new Vector3(0, 0.535f, 0);
        item.transform.parent = _gridOfRooms[i, j].transform;
    }


    void SpawnPortal()
    {
        int i = Random.Range(0, (int)size);
        int j = Random.Range(0, (int)size);

        var port = Instantiate(portal);
        port.transform.position = gridOfRooms[i, j].transform.position;
        port.transform.parent = gridOfRooms[i, j].transform;

    }

}
