using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static bool BooleanThatLogsIfTrue(bool returnValue, string message)
    {
        if (returnValue)
        {
            Debug.Log(message);
        }
        return returnValue;
    }
}
