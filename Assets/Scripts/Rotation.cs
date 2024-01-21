using UnityEngine;
using UnityEngine.InputSystem;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 25f;

    private bool isDragging = false;
    private Vector3 initialMousePos;
    private Vector3 initialObjectPos;

    void Update()
    {
        if (Mouse.current.leftButton.isPressed && !isDragging)
        {
            OnMouseDown();
        }
        else if (Mouse.current.leftButton.isPressed && isDragging)
        {
            OnMouseDrag();
        }
        else if (!Mouse.current.leftButton.isPressed && isDragging)
        {
            OnMouseUp();
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        initialMousePos = Mouse.current.position.ReadValue();
        initialObjectPos = transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 currentMousePos = Mouse.current.position.ReadValue();
        Vector3 mouseDelta = currentMousePos - initialMousePos;

        // Calculate rotation based on movement along the x-axis
        float rotationAmount = (mouseDelta.y * rotationSpeed * Time.deltaTime) * 10f;

        // Apply rotation to the object
        transform.Rotate(Vector3.right, rotationAmount);

        // Update initial mouse position for the next frame
        initialMousePos = currentMousePos;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}