using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    [SerializeField] private Translation[] translationObjects;
    [SerializeField] private Scaling[] scalingObjects;

    public static Manager Instance
    {
        get;
        private set;
    }
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void EnableTranslation()
    {
        Debug.Log("enabling translation");
        foreach(var obj in translationObjects)
        {
            obj.GetComponent<Translation>().enabled = true;
        }
    }

    public void DisableTranslation()
    {
        Debug.Log("disabling translation");
        foreach (var obj in translationObjects)
        {
            obj.GetComponent<Translation>().enabled = false;
        }
    }

    public void EnableScaling()
    {
        Debug.Log("enabling scaling");
        foreach (var obj in scalingObjects)
        {
            obj.GetComponent<Scaling>().enabled = true;
        }
    }

    public void DisableScaling()
    {
        Debug.Log("disabling scaling");
        foreach (var obj in scalingObjects)
        {
            obj.GetComponent<Scaling>().enabled = false;
        }
    }
}
