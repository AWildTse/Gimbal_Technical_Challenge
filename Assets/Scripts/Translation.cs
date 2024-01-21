using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Translation : MonoBehaviour
{

    [SerializeField] private Transform objTransform;
    [SerializeField] private Transform xTranslation;
    [SerializeField] private Transform yTranslation;
    [SerializeField] private Transform zTranslation;

    [SerializeField] private LayerMask translationLayerMask;
    [SerializeField] [Range(1f, 10f)] private float speed = 2.5f;

    private bool isDragging = false;
    private float offset = 1f;
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

        if (xTranslation == null)
        {
            xTranslation = GameObject.Find("xTranslation").transform;
        }

        if (yTranslation == null)
        {
            yTranslation = GameObject.Find("yTranslation").transform;
        }

        if (zTranslation == null)
        {
            zTranslation = GameObject.Find("zTranslation").transform;
        }

        if (translationLayerMask == 0)
        {
            translationLayerMask = LayerMask.GetMask("Translation");
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
            Manager.DisableScaling();
            Manager.EnableTranslation();
            OnMouseDown();
        }
        else if (!Mouse.current.leftButton.isPressed && isDragging && !TryHandleTranslationSelection())
        {
            OnMouseUp();
        }

        if (isDragging)
        {
            Manager.DisableScaling();
            if (clickedGameObject == xTranslation)
            {
                xAxisTranslation();
            }
            else if (clickedGameObject == yTranslation)
            {
                yAxisTranslation();
            }
            else if (clickedGameObject == zTranslation)
            {
                zAxisTranslation();
            }
        }
        else
        {
            Manager.EnableScaling();
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
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, translationLayerMask))
        {
            clickedGameObject = raycastHit.transform;
            return true;
        }
        return false;
    }

    #region Axis Translations
    private void xAxisTranslation()
    {
        if (MouseWorld.GetPosition().x < initialMousePos.x)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position - (new Vector3(offset, 0, 0)), speed * Time.deltaTime);
        }
        if (MouseWorld.GetPosition().x > initialMousePos.x)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position + (new Vector3(offset, 0, 0)), speed * Time.deltaTime);
        }
    }

    private void yAxisTranslation()
    {
        if (MouseWorld.GetPosition().y < initialMousePos.y)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position - (new Vector3(0, offset, 0)), speed * Time.deltaTime);
        }
        if (MouseWorld.GetPosition().y > initialMousePos.y)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position + (new Vector3(0, offset, 0)), speed * Time.deltaTime);
        }
    }

    private void zAxisTranslation()
    {
        if (MouseWorld.GetPosition().z < initialMousePos.z)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position - (new Vector3(0, 0, offset)), speed * Time.deltaTime);
        }
        if (MouseWorld.GetPosition().z > initialMousePos.z)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position + (new Vector3(0, 0, offset)), speed * Time.deltaTime);
        }
    }
    #endregion
}