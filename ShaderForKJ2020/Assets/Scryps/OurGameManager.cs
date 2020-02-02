using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class OurGameManager : MonoBehaviour
{
    public static OurGameController.GameState actualState = OurGameController.GameState.playerMove;


    private void Start()
    {
        StartCoroutine(StateMachine());
    }

    private  IEnumerator StateMachine()
    {
        Debug.Log(actualState);
        bool noGameEnd = true;
        while (true)
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
                    case OurGameController.GameState.end:
                        if (OurGameController.restart)
                        {
                            Debug.Log("Last Stand");
                            Time.timeScale = 1;
                            actualState = OurGameController.GameState.playerMove;
                        }
                        else
                        StopGame();
                        Debug.Log("Game STOPPED");

                       
                    break;
                   case OurGameController.GameState.won:
                    if (OurGameController.restart == false)
                        StopGame();
                       
                 
                       break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void StopGame()
    {
        Debug.Log("CheckXXSXS");
        Time.timeScale = 0;
    }

    private void ResetGame()
    {
        //reset player position, //reset enemy position. 
    }
}
