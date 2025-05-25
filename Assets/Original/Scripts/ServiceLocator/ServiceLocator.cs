using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static readonly Dictionary<Type, object> _services = new();

    public static void Register<TInterface>(TInterface implementation)
    {
        _services[typeof(TInterface)] = implementation;
    }

    public static TInterface Get<TInterface>()
    {
        if (_services.TryGetValue(typeof(TInterface), out var service))
        {
            return (TInterface)service;
        }

        throw new Exception($"Service {typeof(TInterface)} not registered.");
    }

    public static void Clear() => _services.Clear();
}
