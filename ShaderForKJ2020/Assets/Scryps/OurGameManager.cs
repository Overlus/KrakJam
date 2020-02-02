using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OurGameManager : MonoBehaviour
{
    public static OurGameController.GameState actualState = OurGameController.GameState.playerMove;
    [SerializeField] private GameObject won;
    [SerializeField] private GameObject los;

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
                        los.SetActive(false);
                        won.SetActive(false);
                        OurGameController.playerMadeMove = false;
                        actualState = OurGameController.GameState.enemyMove;
                    }
                    break;
                case OurGameController.GameState.enemyMove:
                    if (OurGameController.enemyMadeMove)
                    {
                        los.SetActive(false);
                        won.SetActive(false);
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
                            Time.timeScale = 1;
                            los.SetActive(false);
                            actualState = OurGameController.GameState.playerMove;
                        }
                        else
                        {
                            Debug.Log("Player Had collision");
                        los.SetActive(true);
                        StopGame();
                        OurGameController.playerMadeMove = false;
                        OurGameController.restart = false;

                        }

                       
                    break;
                   case OurGameController.GameState.won:
                    if (OurGameController.restart)
                    {
                        Time.timeScale = 1;
                        won.SetActive(false);
                        actualState = OurGameController.GameState.playerMove;
                        OurGameController.restart = false;
                        ScoreManager.Instance.Scores = 0;
                    }
                    else
                    {
                        won.SetActive(true);
                        StopGame();
                        OurGameController.playerMadeMove = false;
                    }

                 
                       break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void StopGame()
    {
        Debug.Log("CheckXXSXS");
        Time.timeScale = 0f;
    }

    private void ResetGame()
    {
        //reset player position, //reset enemy position. 
    }
}
