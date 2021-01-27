using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.ComponentModel;
using System;
using System.Reflection;
using System.Text;
using System.Linq;

public static class Extension
{

    public static void DoWithDelay(this MonoBehaviour mono, VoidDelegate func, float delay)
    {
        if (delay <= 0)
            func();
        else
            mono?.StartCoroutine(DoWithDelayCoroutine(func, delay));
    }

    public static IEnumerator DoWithDelayCoroutine(this MonoBehaviour mono, VoidDelegate func, float delay)
    {
        yield return DoWithDelayCoroutine(func, delay);
    }

    static IEnumerator DoWithDelayCoroutine(VoidDelegate func, float delay)
    {
        yield return new WaitForSeconds(delay);
        func?.Invoke();
    }

    public static void DoAtNextFrame(this MonoBehaviour mono, VoidDelegate func)
    {
        mono.StartCoroutine(DoAtNextFrameCoroutine(mono, func));
    }

    public static IEnumerator DoAtNextFrameCoroutine(this MonoBehaviour mono, VoidDelegate func)
    {
        yield return null;
        func();
    }

    public static bool IsLayerInLayerMask(this MonoBehaviour mono, LayerMask mask, int layer)
    {
        return mask == (mask | 1 << layer);
    }

    public static string ToTimeString(this float time, bool showHours)
    {
        int second = Mathf.FloorToInt(time % 60);
        time /= 60; // witout seconds
        int minutes = Mathf.FloorToInt(time % 60);
        time /= 60; // without minutes
        int hours = Mathf.FloorToInt(time % 60);

        if (showHours)
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, second);
        else
            return string.Format("{0:00}:{1:00}", minutes, second);
    }

    public static string ToAllElementsString<T>(this IList<T> list, string title, bool isNextLine = true)
    {
        return title + $": {list.ToAllElementsString(isNextLine)}";
    }

    public static string ToAllElementsString<T>(this IList<T> list, bool isNextLine = true)
    {
        if (list == null)
            return "null";

        if (list.Count == 0)
            return "length is 0";

        var strBulder = new StringBuilder();
        for (int i = 0; i < list.Count - 1; i++)
        {
            if (isNextLine) strBulder.AppendLine($"{i} - {list[i].ToString()}");
            else strBulder.Append($"{list[i].ToString()}, ");
        }
        strBulder.Append($"{list[list.Count - 1].ToString()}");

        return strBulder.ToString();
    }

    public static string AllElementsInLineString<T>(this IEnumerable<T> elements)
    {
        var strBulder = new StringBuilder();
        foreach (var element in elements)
            strBulder.Append($"{element.ToString()},");

        if (strBulder.Length > 0)
            strBulder.Length -= 1;

        return strBulder.ToString();
    }
    public static string ToFirstLetterUpper(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return value.First().ToString().ToUpper() + value.Substring(1);
    }

    public static void Times(this int count, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            action();
        }
    }

    public static Vector2 Position2d(this Transform transform) => transform.position;

    public static string ToTimeString(this int time, bool showHours)
    {
        return ((float)time).ToTimeString(showHours);
    }

    public static string CreateWithDashesLine(this string str)
    {
        return string.Format("{0} {1} {0}", new string('-', 25), str);
    }

    public static string GetDescription<T>(this T enumerationValue) where T : struct
    {
        Type type = enumerationValue.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
        }

        //Tries to find a DescriptionAttribute for a potential friendly name
        //for the enum
        MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
        if (memberInfo != null && memberInfo.Length > 0)
        {
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                //Pull out the description value
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }
        //If we have no description attribute, just return the ToString of the enum
        return enumerationValue.ToString();
    }

    #region LookAt2D
    public static void LookAt2D(this Transform me, Vector2 target, Vector2 up)
    {
        Vector2 look = target - (Vector2)me.position;

        float angle = Vector2.Angle(up, look);

        Vector2 right = Vector3.Cross(Vector3.forward, look);

        int dir = 1;

        if (Vector2.Angle(right, up) < 90)
        {
            dir = -1;
        }

        me.rotation *= Quaternion.AngleAxis(angle * dir, Vector3.forward);
    }

    public static void LookAt2D(this Transform me, Transform target, Vector2 up)
    {
        me.LookAt2D(target.position, up);
    }

    public static void LookAt2D(this Transform me, GameObject target, Vector2 up)
    {
        me.LookAt2D(target.transform.position, up);
    }
    #endregion

}


public delegate void ObjectDelegate(object value);
public delegate void VoidDelegate();
public delegate void FloatDelegate(float value);
public delegate void IntDelegate(int value);
public delegate void StringDelegate(string value);


[System.Serializable]
public class FloatEvent : UnityEvent<float> { }
[System.Serializable]
public class CreatureEvent : UnityEvent<GameObject> { }
[System.Serializable]
public class ColliderEvent : UnityEvent<Collider> { }
[System.Serializable]
public class IntEvent : UnityEvent<int> { }




