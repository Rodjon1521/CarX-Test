using System;
using UnityEngine;

namespace Infrastructure
{
    public interface IKeepAliveMonoBehaviourSingleton { };
    public interface IAlwaysAccessibleOnQuit { };
    
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static bool isQuitting = false;
        public static bool isInitialized = false;

        public static T instance
        {
            get
            {
                if (isQuitting)
                {
                    if (m_instance is IAlwaysAccessibleOnQuit)
                    {
                        return m_instance;
                    }
                    else
                    {
                        return null;
                    }
                }

                return m_instance ?? CreateInstance();
            }
        }

        private static T m_instance;

        protected virtual void Awake()
        {
            if (m_instance == null || m_instance == this)
            {
                m_instance = this as T;

                if (this is IKeepAliveMonoBehaviourSingleton)
                {
                    DontDestroyOnLoad(gameObject);
                }

                try
                {
                    if (!isInitialized)
                    {
                        Initialization();
                    }
                }
                catch (Exception E)
                {
                    Debug.LogException(E);
                }
                finally
                {
                    isInitialized = true;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            try
            {
                if (this == m_instance)
                {
                    try
                    {
                        Finalization();
                    }
                    catch (Exception E)
                    {
                        Debug.LogException(E);
                    }
                    finally
                    {
                        isInitialized = false;
                    }
                }
            }
            catch (Exception E)
            {
                Debug.LogException(E);
            }

            if (this == m_instance)
            {
                m_instance = null;
            }
        }

        private void OnApplicationQuit()
        {
            isQuitting = true;
        }

        protected virtual void Initialization()
        {
        }

        protected virtual void Finalization()
        {
        }

        public static T CreateInstance()
        {
            if (m_instance == null)
            {
                try
                {
                    m_instance = FindObjectOfType<T>() as T;
                }
                catch (Exception E)
                {
                    Debug.Log(E);
                }
                finally
                {
                    if (Application.isPlaying)
                    {
                        if (m_instance == null)
                        {
#if DEV_BUILD
						Debug.Log("Singleton -> Creating: " + typeof(T).Name);
#endif
                        }
                        else
                        {
                            // кейс, когда мы запрашиваем GameObject, который реально уже существует на сцене, но для которого еще не был вызван Awake:
                            // нам придется его удалить, и создать новый с насильным запуском Awake, чтобы все процедуры отработали штатно прямо сейчас
                            m_instance.gameObject.SetActive(false);
                            Destroy(m_instance.gameObject);
                            m_instance = null;

                            Debug.LogWarning("Singleton -> Force creating: " + typeof(T).Name);
                        }

                        var go = new GameObject(typeof(T).ToString());

                        // хукаемся так, чтобы m_instance заполнился ДО вызова Awake,
                        // тем самым делая неважным порядок вызова Awake в наследовании
                        go.SetActive(false);
                        m_instance = go.AddComponent<T>();
                        go.SetActive(true);
                    }
                }
            }

            return m_instance;
        }

        public static bool hasInstance
        {
            get { return (!isQuitting || (m_instance is IAlwaysAccessibleOnQuit)) && m_instance != null; }
        }
    }
}