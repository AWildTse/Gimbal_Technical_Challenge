using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scaling : MonoBehaviour
{
    [SerializeField] private Transform obj;

    [SerializeField] private LayerMask rotationLayerMask;

    private Vector2 mousePosition;
    private Vector2 newMousePosition;

    // Start is called before the first frame update
    private void Start()
    {
        if (obj == null)
        {
            obj = GameObject.Find("Object").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            mousePosition = GetPosition();
            
            StartCoroutine(GrabPosition());

            if (newMousePosition.x < mousePosition.x)
            {
                Shrink();
            }
            else if(newMousePosition.x > mousePosition.x)
            {
                Grow();
            }
        }
    }

    IEnumerator GrabPosition()
    {
        yield return new WaitForSecondsRealtime(.1f);
        newMousePosition = GetPosition();
    }

    private bool TryHandleRotationSelection()
    {
        return false;
    }

    private static Vector2 GetPosition()
    {
        return Mouse.current.position.ReadValue();
    }

    private void Shrink()
    {
        obj.transform.localScale += new Vector3 (-0.01f, -0.01f, -0.01f);
    }

    private void Grow()
    {
        obj.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
    }
}
