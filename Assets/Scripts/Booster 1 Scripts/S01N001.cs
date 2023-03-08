using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SakuraStudios.FECipherPlayer
{
    public class S01N001 : BasicCard
    {
        // NOTE: Because inheriting classes don't call their parents' Start method, all classes inheriting from BasicCard need to call the SetUpCard method to perform
        // setup actions for the card and gameObject like pulling references. This is called in Awake because these component references need to all be set up before
        // Start functions get called. 
        void Awake()
        {
            SetUpCard();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

