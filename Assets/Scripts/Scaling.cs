using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scaling : MonoBehaviour
{
    [SerializeField] private Transform objTransform;
    [SerializeField] private Transform xScaling;
    [SerializeField] private Transform yScaling;
    [SerializeField] private Transform zScaling;

    [SerializeField] private LayerMask ScalingLayerMask;
    [SerializeField] [Range(0.001f, 0.01f)] private float speed = 0.001f;
    [SerializeField] [Range(1f, 5f)] private float min = 0.1f;
    [SerializeField] [Range(5f, 10f)] private float max = 5f;

    private bool isDragging = false;
    private Vector3 initialMousePos;
    private Transform clickedGameObject;

    private Camera mainCamera;

    private void Start()
    {
        #region initializing if empty
        //If for some reason a reference is empty within the editor, We'll look for the missing components
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (objTransform == null)
        {
            objTransform = GameObject.Find("Object").transform;
        }

        if (xScaling == null)
        {
            xScaling = GameObject.Find("xScaling").transform;
        }

        if (yScaling == null)
        {
            yScaling = GameObject.Find("yScaling").transform;
        }

        if (zScaling == null)
        {
            zScaling = GameObject.Find("zScaling").transform;
        }

        if (ScalingLayerMask == 0)
        {
            ScalingLayerMask = LayerMask.GetMask("Scaling");
        }
        #endregion
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            initialMousePos = MouseWorld.GetPosition();
        }
        if (Mouse.current.leftButton.isPressed && !isDragging && TryHandleTranslationSelection())
        {
            Manager.DisableTranslation();
            Manager.EnableScaling();
            OnMouseDown();
        }
        else if (!Mouse.current.leftButton.isPressed && isDragging && !TryHandleTranslationSelection())
        {
            OnMouseUp();
        }

        if (isDragging)
        {
            Manager.DisableTranslation();
            if (clickedGameObject == xScaling || clickedGameObject == yScaling || clickedGameObject == zScaling)
            {
                ObjectScaling();
            }
        }
        else
        {
            Manager.EnableTranslation();
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private bool TryHandleTranslationSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ScalingLayerMask))
        {
            clickedGameObject = raycastHit.transform;
            return true;
        }
        return false;
    }

    private void ObjectScaling()
    {
        if (MouseWorld.GetPosition().x < initialMousePos.x)
        {
            objTransform.localScale -= new Vector3(speed, speed, speed);
        }
        if (MouseWorld.GetPosition().x > initialMousePos.x)
        {
            objTransform.localScale += new Vector3(speed, speed, speed);
        }
    }
}
