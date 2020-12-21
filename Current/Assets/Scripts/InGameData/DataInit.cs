using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInit
{

    private static DataInit s_dataInit;
    public static DataInit instance
    {
        get
        {
            if (s_dataInit == null)
                s_dataInit = new DataInit();
            return s_dataInit;
        }
    }

    DataInit()
    {


    }
        

   
}
