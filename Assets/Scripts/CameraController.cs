using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SakuraStudios.FECipherPlayer {
    public class CameraController : MonoBehaviour
    {
        public Camera mainCamera;
        public float minCameraYPosition = -1.7f;
        public float maxCameraYPosition = 10.2f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Method called by the scrollbar in the UI to move the camera to a particular place on the field.
        // Input should only be normalized values
        public void NormalizedMoveCamera(float scrollValue)
        {
            float normalValue = Mathf.Clamp(scrollValue, 0f, 1f);

            // Get the current position of the camera.
            Vector3 newPosition = mainCamera.transform.position;

            float length = maxCameraYPosition - minCameraYPosition;

            //Set the camera's y position to a new value based on the normalized input.
            newPosition.y = minCameraYPosition + normalValue * length;

            mainCamera.transform.position = newPosition;
        }
    }
}

