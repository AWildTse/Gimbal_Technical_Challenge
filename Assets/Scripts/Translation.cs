using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Translation : MonoBehaviour
{
    [SerializeField] private Transform obj;

    [SerializeField] private LayerMask translationLayerMask;
  
    [SerializeField] [Range(0.01f, 0.1f)] private float speed = 0.01f;

    private string direction = "";

    private void Start()
    {
        if(obj == null)
        {
            obj = GameObject.Find("Object").transform;
        }
    }
    private void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            if(TryHandleTranslationSelection())
            {
                Translate(direction);
            }
        }
    }

    private bool TryHandleTranslationSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, translationLayerMask))
        {
            if(raycastHit.transform.TryGetComponent<Translation>(out Translation translation))
            {
                AssignDirection(raycastHit.collider.gameObject.name);
                return true;
            }
        }
        return false;
    }

    private void Translate(string dir)
    {
        Debug.Log("choosing dir");
        switch(dir)
        {
            case "Left":
                Debug.Log("Left");
                obj.transform.position += new Vector3(-speed, 0, 0);
                break;
            case "Right":
                Debug.Log("Right");
                obj.transform.position += new Vector3(speed, 0, 0);
                break;
            case "Up":
                Debug.Log("Up");
                obj.transform.position += new Vector3(0, speed, 0);
                break;
            case "Down":
                Debug.Log("Down");
                obj.transform.position += new Vector3(0, -speed, 0);
                break;
            case "Forward":
                Debug.Log("Forward");
                obj.transform.position += new Vector3(0, 0, speed);
                break;
            case "Backward":
                Debug.Log("Backward");
                obj.transform.position += new Vector3(0, 0, -speed);
                break;
        }
    }

    private void AssignDirection(string direction)
    {
        this.direction = direction;
    }
}
