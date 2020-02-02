using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public static class OurGameController 
{
    public enum GameState
    {
        playerMove,
        enemyMove,
        sceneMove,
        end,
        won
    }

    public static bool playerMadeMove;
    public static bool enemyMadeMove;
    public static bool sceneMadeMove;
    public static bool restart;
}

