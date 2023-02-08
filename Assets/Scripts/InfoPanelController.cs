using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SakuraStudios.FECipherPlayer
{

    public class InfoPanelController : MonoBehaviour
    {
        public UnityEngine.UI.Image displayImage;             //The location we're placing the image.

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DisplayCard(BasicCard card)
        {
            //displayImage.sprite = card.GetCardSprite();
        }
    }
}
