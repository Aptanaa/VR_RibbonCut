using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalStateController
{
    public static bool firstCut = true;

    public static float particleDelay = 1f;

    public static float ballonAscendingRate = 100f;
    public static Vector2 ballonRandomRange = new Vector2(-50f,50f);
}
