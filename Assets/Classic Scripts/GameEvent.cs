using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvent : MonoBehaviour
{
    public static Action<int> Addscore;
    public static Action CheckIfShapeCanbePlaced;

    public static Action MoveShapeToStartPosition;

    public static Action RequestNewShapes;

    public static Action SetShapeInactive;

    public static Action GameOver;

    public static bool NewGame = false;

    public static bool bestScoreReached = false;

    public static Action<Config.SquareColor> UpdateSquareColor;
}
