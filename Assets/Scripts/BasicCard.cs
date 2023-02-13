using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.SakuraStudios.FECipherPlayer
{
    public abstract class BasicCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Private Variables

        private Sprite cardSprite;

        #endregion


        #region Properties

        public Sprite CardSprite { get { return cardSprite; } }             // Returns the cardSprite for other classes.

        #endregion


        #region Protected Functions

        // This method takes the place of the Start method as Start() is not called in abstract classes.
        // NOTE: This method must be called on all inherited instances of this class
        protected virtual void SetUpCard()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            cardSprite = renderer.sprite;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion


        #region IPointerHandler Interface Implementation

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

        // When a card is clicked it should display it's translation and sprite in the Info Panel.
        // Note: Requires a physics collider on the gameObject and a Physics Raycaster on the camera in order to be called.
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            InfoPanelController.Instance.DisplayCard(this);
        }

        #endregion


    }
}
