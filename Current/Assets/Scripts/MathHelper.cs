using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper : MonoBehaviour
{

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = to - from;
        return Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
    }

    public static Vector3 AngleDistance(Vector3 oripoint, float angle, float distance)
    {
        Vector3 v3distance = new Vector3(distance, 0 , 0);
        Quaternion rota = Quaternion.Euler(0, -angle, 0);
        Vector3 temp = rota * v3distance;
        return oripoint + temp;
    }

    public static float EaseOutExpo(float start, float end, float delta)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * delta / 1) + 1) + start;
    }

    public static Vector3 EaseOutExpo(Vector3 start, Vector3 end, float delta)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * delta / 1) + 1) + start;
    }

    public static Vector3 BezierCurve(Vector3 start, Vector3 p1, Vector3 p2, Vector3 end, float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector3 result =
            (u3) * start +
            (3f * u2 * t) * p1 +
            (3f * u * t2) * p2 +
            (t3) * end;

        return result;
    }

    public static int SplitString(string text)
    {
        int num = 0;
        int length = text.Length - 2;
        for (int i = 1; i < text.Length; i++)
        {
            num += (text[i] - '0') * (int)Mathf.Pow(10, length);
            length--;
        }
        return num;

    }


    public static Bounds CalculateBounds(RectTransform transform, float uiScaleFactor)
    {
        Bounds bounds = new Bounds(transform.position, new Vector3(transform.rect.width, transform.rect.height, 0.0f) * uiScaleFactor);

        if (transform.childCount > 0)
        {
            foreach (RectTransform child in transform)
            {
                Bounds childBounds = new Bounds(child.position, new Vector3(child.rect.width, child.rect.height, 0.0f) * uiScaleFactor);
                bounds.Encapsulate(childBounds);
            }
        }

        return bounds;
    }
}
