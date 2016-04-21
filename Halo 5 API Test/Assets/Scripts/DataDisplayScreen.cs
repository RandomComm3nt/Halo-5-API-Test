using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class DataDisplayScreen : MonoBehaviour
    {
        [SerializeField] private Transform mapTransform;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GraphicRaycaster gRaycaster;
        private PhysicsRaycaster pRaycaster;
        private MouseButton mouseButtonDown;
        private Vector2 lastPosition;

        [Header("Controls")]
        [SerializeField] private float panSensitivity = 0.1f;
        [SerializeField] private float rotateSensitivity = 0.25f;
        [SerializeField] private float scrollSensitivity = 0.5f;
        
        private void Start()
        {
            pRaycaster = mainCamera.GetComponent<PhysicsRaycaster>();
            mouseButtonDown = MouseButton.None;
        }

        private void Update()
        {
            Vector2 mousePositionDelta = (Vector2)Input.mousePosition - lastPosition;

            if (mouseButtonDown == MouseButton.Left)
            {
                if (Input.GetMouseButtonUp((int)mouseButtonDown))
                {
                    mouseButtonDown = MouseButton.None;
                }
            }

            if (mouseButtonDown == MouseButton.Middle)
            {
                mainCamera.transform.localPosition -= mainCamera.transform.right * mousePositionDelta.x * panSensitivity;
                mainCamera.transform.localPosition -= mainCamera.transform.up * mousePositionDelta.y * panSensitivity;

                if (Input.GetMouseButtonUp((int)mouseButtonDown))
                {
                    mouseButtonDown = MouseButton.None;
                }
            }

            if (mouseButtonDown == MouseButton.Right)
            {
                mainCamera.transform.localEulerAngles += Vector3.up * mousePositionDelta.x * rotateSensitivity;
                mainCamera.transform.localEulerAngles += Vector3.left * mousePositionDelta.y * rotateSensitivity;

                if (Input.GetMouseButtonUp((int)mouseButtonDown))
                {
                    mouseButtonDown = MouseButton.None;
                }
            }

            if (mouseButtonDown == MouseButton.None)
            {
                if (Input.mouseScrollDelta.y != 0)
                    mainCamera.transform.localPosition += mainCamera.transform.forward * scrollSensitivity * Input.mouseScrollDelta.y;

                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                {
                    // check mouse is not over UI
                    PointerEventData ped = new PointerEventData(EventSystem.current);
                    ped.position = Input.mousePosition;
                    List<RaycastResult> results = new List<RaycastResult>();
                    gRaycaster.Raycast(ped, results);

                    if (results.Count == 0)
                    {
                        if (Input.GetMouseButtonDown(0))
                            mouseButtonDown = MouseButton.Left;
                        if (Input.GetMouseButtonDown(1))
                            mouseButtonDown = MouseButton.Right;
                        if (Input.GetMouseButtonDown(2))
                            mouseButtonDown = MouseButton.Middle;
                    }
                }
            }


            lastPosition = Input.mousePosition;
        }
    }
}

public enum MouseButton : int { Left = 0, Right = 1, Middle = 2, None = 3}