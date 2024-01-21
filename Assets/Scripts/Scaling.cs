using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scaling : MonoBehaviour
{
    /*
    [SerializeField] private Transform obj;

    [SerializeField] private LayerMask scalingLayerMask;

    private Vector2 mousePosition;
    private Vector2 newMousePosition;
    private bool isScaling = false;

    [SerializeField] [Range(0.001f, 0.1f)] private float scalingSpeed = 0.001f;
    [SerializeField] [Range(1f, 5f)] private float min = 0.1f;
    [SerializeField] [Range(5f, 10f)] private float max = 5f;

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
        if (TryHandleScalingSelection())
        {
            Manager.Instance.DisableTranslation();
            isScaling = true;
        }
        if (Mouse.current.leftButton.isPressed && isScaling == true)
        {
            mousePosition = GetPosition();

            StartCoroutine(GrabPosition());

            if (newMousePosition.x < mousePosition.x && isScaling == true)
            {
                Grow();
            }
            else if (newMousePosition.x > mousePosition.x && isScaling == true)
            {
                Shrink();
            }
            mousePosition = newMousePosition;
        }
        else
        {
            Manager.Instance.EnableTranslation();
            isScaling = false;
        }
    }

    IEnumerator GrabPosition()
    {
        yield return new WaitForSecondsRealtime(.1f);
        newMousePosition = GetPosition();
    }

    private bool TryHandleScalingSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, scalingLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Scaling>(out Scaling scaling))
            {
                return true;
            }
        }
        return false;
    }

    private static Vector2 GetPosition()
    {
        return Mouse.current.position.ReadValue();
    }

    private void Shrink()
    {
        if(obj.transform.localScale.x > min)
        {
            obj.transform.localScale -= new Vector3(scalingSpeed, scalingSpeed, scalingSpeed);
        }
    }

    private void Grow()
    {
        if(obj.transform.localScale.x < max)
        {
            obj.transform.localScale += new Vector3(scalingSpeed, scalingSpeed, scalingSpeed);
        }
    }
    */
}
