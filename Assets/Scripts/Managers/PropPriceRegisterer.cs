using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PropPriceRegisterer
{
    private static readonly List<PropInfos> _allProps = new();

    public static void Register(PropInfos prop)
    {
        if (!_allProps.Contains(prop))
            _allProps.Add(prop);
    }

    public static void Unregister(PropInfos prop)
    {
        _allProps.Remove(prop);
    }

    public static List<PropInfos> GetPropsInBounds(Bounds bounds)
    {
        List<PropInfos> inside = new();

        foreach (var prop in _allProps)
        {
            if (prop == null) continue;

            if (bounds.Contains(prop.transform.position))
                inside.Add(prop);
        }

        return inside;
    }
}
