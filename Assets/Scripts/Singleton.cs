using System.Collections;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // NOTE: Bare minimum singleton implementation
    public T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}
