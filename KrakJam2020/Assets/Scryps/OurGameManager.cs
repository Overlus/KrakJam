using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OurGameManager : MonoBehaviour
{
    public static OurGameController.GameState actualState = OurGameController.GameState.playerMove;


    private void Start()
    {
        StartCoroutine(StateMachine());
    }


   

    private  IEnumerator StateMachine()
    {
        bool noGameEnd = true;
        while (noGameEnd)
        {
            switch (actualState)
            {
                case OurGameController.GameState.playerMove:
                    if (OurGameController.playerMadeMove)
                    {
                        OurGameController.playerMadeMove = false;
                        actualState = OurGameController.GameState.enemyMove;
                    }
                    break;
                case OurGameController.GameState.enemyMove:
                    if (OurGameController.enemyMadeMove)
                    {
                        OurGameController.enemyMadeMove = false;
                        actualState = OurGameController.GameState.sceneMove;
                    }
                    break;
                case OurGameController.GameState.sceneMove:
                    if (OurGameController.sceneMadeMove)
                    {
                        OurGameController.sceneMadeMove = false;
                        actualState = OurGameController.GameState.playerMove;
                    }
                    break;
                default:
                    noGameEnd = false;
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
