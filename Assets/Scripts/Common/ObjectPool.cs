using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Microsoft.Extensions.ObjectPool
{
    public abstract class ObjectPool<T> where T : class
    {
        public abstract T Get();

        public abstract void Return(T obj);
    }
    public interface IPooledObjectPolicy<T>
    {
        T Create();

        bool Return(T obj);
    }
    public abstract class PooledObjectPolicy<T> : IPooledObjectPolicy<T>
    {
        public abstract T Create();

        public abstract bool Return(T obj);
    }
    public class DefaultPooledObjectPolicy<T> : PooledObjectPolicy<T> where T : class, new()
    {
        public override T Create()
        {
            return new T();
        }

        public override bool Return(T obj)
        {
            return true;
        }
    }
    public class DefaultObjectPool<T> : ObjectPool<T> where T : class
    {
        private struct ObjectWrapper
        {
            public T Element;
        }

        private readonly ObjectWrapper[] _items;

        private readonly IPooledObjectPolicy<T> _policy;

        private readonly bool _isDefaultPolicy;

        private T _firstItem;

        private readonly PooledObjectPolicy<T> _fastPolicy;

        public DefaultObjectPool(IPooledObjectPolicy<T> policy)
            : this(policy, Environment.ProcessorCount * 2)
        {
        }

        public DefaultObjectPool(IPooledObjectPolicy<T> policy, int maximumRetained)
        {
            _policy = policy ?? throw new ArgumentNullException("policy");
            _fastPolicy = policy as PooledObjectPolicy<T>;
            _isDefaultPolicy = IsDefaultPolicy();
            _items = new ObjectWrapper[maximumRetained - 1];
            bool IsDefaultPolicy()
            {
                Type type = policy.GetType();
                if (type.IsGenericType)
                {
                    return type.GetGenericTypeDefinition() == typeof(DefaultPooledObjectPolicy<>);
                }
                return false;
            }
        }

        public override T Get()
        {
            T val = _firstItem;
            if (val == null || Interlocked.CompareExchange(ref _firstItem, null, val) != val)
            {
                val = GetViaScan();
            }
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T GetViaScan()
        {
            ObjectWrapper[] items = _items;
            for (int i = 0; i < items.Length; i++)
            {
                T element = items[i].Element;
                if (element != null && Interlocked.CompareExchange(ref items[i].Element, null, element) == element)
                {
                    return element;
                }
            }
            return Create();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private T Create()
        {
            PooledObjectPolicy<T> fastPolicy = _fastPolicy;
            return ((fastPolicy != null) ? fastPolicy.Create() : null) ?? _policy.Create();
        }

        public override void Return(T obj)
        {
            if ((_isDefaultPolicy || (_fastPolicy?.Return(obj) ?? _policy.Return(obj))) && (_firstItem != null || Interlocked.CompareExchange(ref _firstItem, obj, null) != null))
            {
                ReturnViaScan(obj);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnViaScan(T obj)
        {
            ObjectWrapper[] items = _items;
            for (int i = 0; i < items.Length && Interlocked.CompareExchange(ref items[i].Element, obj, null) != null; i++)
            {
            }
        }
    }
}
