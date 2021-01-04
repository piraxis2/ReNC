using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerComparer : IComparer<BaseChar>
{
    public int Compare(BaseChar x, BaseChar y)
    {
        if (x == null || y == null)
            return 0;

        if (x.MyStatus.Priority < y.MyStatus.Priority)
            return -1;
        else if (x.MyStatus.Priority > y.MyStatus.Priority)
            return 1;

        return 0;


    }
}
