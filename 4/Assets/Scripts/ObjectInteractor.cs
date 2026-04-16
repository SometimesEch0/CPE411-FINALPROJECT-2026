using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;
    
    [Header("Zoom Settings")]
    public float minScale = 0.01f;
    public float maxScale = 0.2f;
    public float zoomSensitivity = 0.1f;

    void Update()
    {
        // --- MOUSE CONTROLS (For Testing in Editor) ---
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, -rotX, Space.Self);
            transform.Rotate(Vector3.right, rotY, Space.Self);
        }

        // --- TOUCH CONTROLS (For Phone) ---
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.Rotate(Vector3.up, -touch.deltaPosition.x * (rotationSpeed / 10f) * Time.deltaTime, Space.Self);
                transform.Rotate(Vector3.right, touch.deltaPosition.y * (rotationSpeed / 10f) * Time.deltaTime, Space.Self);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float prevMag = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
            float currMag = (t0.position - t1.position).magnitude;

            float diff = (currMag - prevMag) * zoomSensitivity * Time.deltaTime;
            float newScale = transform.localScale.x + diff;
            
            newScale = Mathf.Clamp(newScale, minScale, maxScale);
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}