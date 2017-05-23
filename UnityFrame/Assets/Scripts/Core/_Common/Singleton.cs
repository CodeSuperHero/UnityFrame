//CodeSuperHero 20150602

using UnityEngine;

namespace CodeSuperHero.UF
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        protected static T _instance;

        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = singletonObjec.GetComponent<T>();
                    if (_instance == null)
                    {
                        _instance = singletonObjec.AddComponent<T>();
                        _instance.Init();
                    }
                }
                return _instance;
            }
        }

        private static GameObject _singletonObject;

        protected static GameObject singletonObjec
        {
            get
            {
                if (_singletonObject == null)
                {
                    _singletonObject = new GameObject("Singletion");
                }

                return _singletonObject;
            }
        }

        /// <summary>
        /// 初始化单例.
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// 释放单例.
        /// </summary>
        public virtual void Free() { }
    }

    /// <summary>
    /// 双检查加锁单例.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static readonly object lockObj = new object();

        protected static T _instance;

        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                            _instance = new T();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 释放单例.
        /// </summary>
        public virtual void Free() { }
    }
}

