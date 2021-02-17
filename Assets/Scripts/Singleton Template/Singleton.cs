using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance = null;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<T>();

                if (m_instance == null)
                    m_instance = new GameObject("Instance of" + typeof(T)).AddComponent<T>();
            }

            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance != null)
            Destroy( gameObject );
    }
}
