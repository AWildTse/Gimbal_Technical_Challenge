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
    [SerializeField] [Range(1f, 10f)] private float speed = 1f;

    private bool isDragging = false;
    private float offset = 1f;
    private Vector3 initialMousePos;
    private string clickedGameObject;

    private Camera mainCamera;

    private void Start()
    {
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
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            initialMousePos = GetMouseWorldPos();
        }
        if (Mouse.current.leftButton.isPressed && !isDragging && TryHandleTranslationSelection())
        {
            OnMouseDown();
        }
        else if (!Mouse.current.leftButton.isPressed && isDragging && !TryHandleTranslationSelection())
        {
            OnMouseUp();
        }

        if (isDragging)
        {
            if (clickedGameObject == "xTranslation")
            {
                xAxisTranslation();
            }
            else if (clickedGameObject == "yTranslation")
            {
                yAxisTranslation();
            }
            else if (clickedGameObject == "zTranslation")
            {
                zAxisTranslation();
            }
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
            Debug.Log(raycastHit.transform.name);
            if (raycastHit.transform.parent.name == "Translation")
            {
                clickedGameObject = GetTransform(raycastHit);
                return true;
            }
        }
        return false;
    }

    private void xAxisTranslation()
    {
        if (GetMouseWorldPos().x < initialMousePos.x)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position - (new Vector3(offset, 0, 0)), speed * Time.deltaTime);
        }
        if (GetMouseWorldPos().x > initialMousePos.x)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position + (new Vector3(offset, 0, 0)), speed * Time.deltaTime);
        }
    }

    private void yAxisTranslation()
    {
        if (GetMouseWorldPos().y < initialMousePos.y)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position - (new Vector3(0, offset, 0)), speed * Time.deltaTime);
        }
        if (GetMouseWorldPos().y > initialMousePos.y)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position + (new Vector3(0, offset, 0)), speed * Time.deltaTime);
        }
    }

    private void zAxisTranslation()
    {
        if (GetMouseWorldPos().z < initialMousePos.z)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position - (new Vector3(0, 0, offset)), speed * Time.deltaTime);
        }
        if (GetMouseWorldPos().z > initialMousePos.z)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, objTransform.position + (new Vector3(0, 0, offset)), speed * Time.deltaTime);
        }
    }

    private string GetTransform(RaycastHit raycastHit)
    {
        string direction = raycastHit.transform.name;
        return direction;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}