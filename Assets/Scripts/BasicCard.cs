using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.SakuraStudios.FECipherPlayer
{
    public abstract class BasicCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Protected Variables

        protected Sprite cardSprite;
        [SerializeField] protected CipherCardData cardData;

        //These card stats are held locally in case they get changed by other cards.
        protected int localDeploymentCost;
        protected bool localCanPromote;
        protected int localPromotionCost;
        protected bool[] localCardColorArray;
        protected bool[] localCharGenderArray;
        protected bool[] localCharWeaponArray;
        protected bool[] localUnitTypeArray;
        protected int localBaseAttack;
        protected int localBaseSupport;
        protected bool[] localBaseRangeArray;


        #endregion

        #region Public Variables

        // Used to modify a card's attack power outside of battle (with the effects of skills, etc.)
        // Made public since card skills can adjust attack power.
        public int attackModifier = 0;                      
        //public int battleModifier = 0;                      //Used to update a card's attack power in battle (with supports, etc.)  Is reset after battle?
        public int supportModifier = 0;                     //NOTE: Not fully integrated.  Needs to have a way to reset similar to attackModifier.
                                                            //Used to modify a card's support power with the effects of skills, etc. Reset once the card leaves the field.

        // A List that holds information about the skills impacting a card.
        protected List<string> skillChangeTracker = new List<string>();

        #endregion

        // These are the properties to access the information about this card. 
        // Most Properties are made virtual to account for special cases and card abilities.
        // Most are also readonly as they cannot change the original CardData asset they refer to.
        #region Public Properties

        public Sprite CardSprite { get { return cardSprite; } }             // Returns the cardSprite for other classes.
        public CipherCardData GetCardData { get { return cardData; } }



        public virtual string CardNumber { get { return cardData.cardNumber; } }
        public virtual string CharTitle { get { return cardData.charTitle; } }
        public virtual string CharQuote { get { return cardData.charQuote; } }
        public virtual string CardIllustrator { get { return cardData.cardIllustrator; } }
        public virtual string[] CardSkills { get { return cardData.cardSkills; } }
        public virtual string CharName { get { return cardData.charName; } }
        public virtual string ClassTitle { get { return cardData.classTitle; } }
        public virtual bool[] SkillTypes { get { return cardData.skillTypes; } }


        public virtual int DeploymentCost { get { return localDeploymentCost; } }
        public virtual bool Promotable { get { return localCanPromote; } }
        public virtual int PromotionCost { get { return localPromotionCost; } }
        public virtual bool[] CardColorArray { get { return localCardColorArray; } }
        public virtual bool[] CharGenderArray { get { return localCharGenderArray; } }
        public virtual bool[] CharWeaponArray { get { return localCharWeaponArray; } }
        public virtual bool[] UnitTypeArray { get { return localUnitTypeArray; } }
        public virtual int BaseAttack { get { return localBaseAttack; } }
        public virtual int BaseSupport { get { return localBaseSupport; } }
        public virtual bool[] BaseRangeArray { get { return localBaseRangeArray; } }

        /// <summary>
        /// This property returns all current information about skills affecting the card.
        /// </summary> 
        public List<string> SkillChangeTracker { get { return skillChangeTracker; } }

        public virtual int CurrentAttackValue { get { return BaseAttack + attackModifier; } }       //Returns the current attack stat including skill modifiers.
        public virtual int CurrentSupportValue { get { return BaseSupport + supportModifier; } }    //Returns the current support stat including skill modifiers.

        // Property returns a List of the string names of the colors on this card.
        public virtual List<string> CardColorList
        {
            get
            {
                string[] cipherColorArray = Enum.GetNames(typeof(CipherData.ColorsEnum));
                List<string> colorNames = new List<string>(cipherColorArray.Length);

                //Loops through each possible color
                for (int i = 0; i < CardColorArray.Length; i++)
                {
                    //if the color is on the card then add the color name to the list
                    if (CardColorArray[i])
                    {
                        colorNames.Add(cipherColorArray[i]);
                    }
                }

                return colorNames;
            }
        }

        #endregion


        #region Protected Functions

        // This method takes the place of the Start method as Start() is not called in abstract classes.
        // NOTE: This method must be called on all inherited instances of this class
        protected virtual void SetUpCard()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            cardSprite = renderer.sprite;

            if (cardData == null)
            {
                Debug.LogError(gameObject.name + " has not had it's CipherCardData field assigned in the inspector.");
            }

            // Set local values based on standard card data.
            // We need to clone the arrays to make sure we aren't just copying a reference, but creating a new distinct array we can mess with.
            localDeploymentCost = cardData.deploymentCost;
            localPromotionCost = cardData.promotionCost;
            localCardColorArray = (bool[])cardData.cardColor.Clone();
            localCharGenderArray = (bool[])cardData.charGender.Clone();
            localCharWeaponArray = (bool[])cardData.charWeaponType.Clone();
            localUnitTypeArray = (bool[])cardData.unitTypes.Clone();
            localBaseAttack = cardData.baseAttack;
            localBaseSupport = cardData.baseSupport;
            localBaseRangeArray = (bool[])cardData.baseRange.Clone();
            localCanPromote = cardData.canPromote;
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
