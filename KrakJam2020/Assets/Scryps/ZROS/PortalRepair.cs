using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortalRepair : MonoBehaviour
{
    [SerializeField] private GameObject[] emptyParts;
    private bool[] repairSlotsStatus= new bool[6];
    private bool areAllSlotsFilled;
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PortalPart"))
        {
            if (!areAllSlotsFilled)
            {
                AtemptToRepairSlots(GetRandomSlotNumber());
            }
            else
            {
                Debug.Log("It's time to stop");
            }
        }
    }
    
    private void AtemptToRepairSlots(int slotNumber)
    {
        if (!IsSlotRepaired(slotNumber))
        {
            RepairSlots(slotNumber);
        }
        else
        {
            AtemptToRepairSlots(GetRandomSlotNumber());
        }
    }

    private int GetRandomSlotNumber()
    {
        return Random.Range(0, 6);
    }

    private void RepairSlots(int slotNumber)
    {
        repairSlotsStatus[slotNumber] = true;
        emptyParts[slotNumber].GetComponent<Renderer>().enabled = true;
        areAllSlotsFilled = CheckIfAllSlotsAreFilled();
    }

    private bool CheckIfAllSlotsAreFilled()
    {
        foreach (var slotsStatu  in repairSlotsStatus)
        {
            if (slotsStatu.Equals(false))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsSlotRepaired(int slotNumber)
    {
        return repairSlotsStatus[slotNumber];
    }

    


}
