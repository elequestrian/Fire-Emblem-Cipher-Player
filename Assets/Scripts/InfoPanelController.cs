using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SakuraStudios.FECipherPlayer
{
    // This class controls the functions of the InfoPanel to display card information.
    public class InfoPanelController : MonoBehaviour
    {
        public static InfoPanelController Instance = null;                      // The instance reference to this script as it is a singleton pattern.
        
        [SerializeField] private UnityEngine.UI.Image displayImage;             // The location we're placing the image.


        // Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (Instance == null)

                //if not, set instance to this
                Instance = this;

            //If instance already exists and it's not this:
            else if (Instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of the InfoPanelController.
                Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Method called by a BasicCard when clicked to display the card information. 
        public void DisplayCard(BasicCard card)
        {
            displayImage.sprite = card.CardSprite;
        }
    }
}
