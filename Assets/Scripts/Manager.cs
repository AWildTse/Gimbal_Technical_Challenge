using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    [SerializeField] private Transform translation;
    [SerializeField] private Transform scaling;
    [SerializeField] private Transform rotation;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        if(translation == null)
        {
            translation = GameObject.Find("Translation").transform;
        }

        if(scaling == null)
        {
            scaling = GameObject.Find("Scaling").transform;
        }
    }

    public static Transform GetScaling()
    {
        return instance.scaling;
    }

    public static void EnableTranslation()
    {
        foreach(Transform obj in instance.translation)
        {
            obj.gameObject.GetComponent<MeshCollider>().enabled = true;
        }
    }

    public static void DisableTranslation()
    {
        foreach (Transform obj in instance.translation)
        {
            obj.gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }

    public static void EnableScaling()
    {
        foreach (Transform obj in instance.scaling)
        {
            obj.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

    public static void DisableScaling()
    {
        foreach (Transform obj in instance.scaling)
        {
            obj.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
