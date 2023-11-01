using System;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : ISingleton where T : class, new()
{
    #region [Fields]
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                CreateInstance();
            }
            return _instance;
        }
    }
    #endregion

    #region [Construct]
    protected Singleton() { }
    #endregion

    #region [Business]
    protected virtual void Init() { }
    #endregion

    #region [API]
    public virtual void UnInit() { }

    public static bool HasInstance()
    {
        return _instance != null;
    }
    public static T GetInstance()
    {
        if (_instance == null)
        {
            CreateInstance();
        }
        return _instance;
    }
    public static void CreateInstance()
    {
        if (_instance == null)
        {
            _instance = Activator.CreateInstance<T>();
            (_instance as ISingleton).OnConstruct();
            (_instance as Singleton<T>).Init();
        }
    }
    public static void DestroyInstance()
    {
        if (_instance != null)
        {
            (_instance as Singleton<T>).UnInit();
            (_instance as ISingleton).OnDestroy();
            _instance = null;
        }
    }
    #endregion


    #region [ISingleton]
    public void OnDestroy()
    {
        UnityEngine.Debug.Log($"onDestroy:{typeof(T)}");
        SingletonPool.RemoveInstance(_instance);
    }

    public void OnConstruct()
    {
        UnityEngine.Debug.Log($"onConstruct:{typeof(T)}");
        SingletonPool.AddInstance(_instance);
    }

    public void Purge()
    {
        UnityEngine.Debug.Log($"Purge:{typeof(T)}");
        Singleton<T>.DestroyInstance();
    }
    #endregion

}

public interface ISingleton
{
    public void Purge();
    public void OnConstruct();
    public void OnDestroy();
}

public class SingletonPool
{
    private static List<object> _instancePools = new List<object>();

    public static void AddInstance(object singleton)
    {
        _instancePools.Add(singleton);
        DumpAllInstances();
    }

    public static void RemoveInstance(object singleton)
    {
        _instancePools.Remove(singleton);
    }

    public static void DumpAllInstances()
    {
        foreach (var obj in _instancePools)
        {
            var instance = obj as ISingleton;
        }
    }

    public static void PurgeAllInstances()
    {
        UnityEngine.Debug.Log("SingletonPool.PurgeAllInstances");
        while (_instancePools.Count>0)
        {
            var instance = _instancePools[_instancePools.Count-1] as ISingleton;
            UnityEngine.Debug.Log(instance.GetType());
            instance.Purge();
        }
    }

}
