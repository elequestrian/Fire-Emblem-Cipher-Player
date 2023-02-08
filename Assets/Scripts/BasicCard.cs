using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.SakuraStudios.FECipherPlayer
{
    public abstract class BasicCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /*
        public Sprite GetCardSprite()
        {

        }
        */

        // 
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            Debug.Log("Cursor Entering " + name + " GameObject");
        }

        // 
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            Debug.Log("Cursor Exiting " + name + " GameObject");
        }

        // 
        // Note: Requires a physics collider on the gameObject and a Physics Raycaster on the camera in order to be called.
        public void OnPointerClick(PointerEventData pointerEventData)
        {

        }
    }
}
