using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyart
{
    public class TestInteractable : Interactable_Dreamscape
    {
        // Bool to track if the item is equipped
        private Rigidbody rd;
        private void Awake()
        {
            rd = GetComponent<Rigidbody>();
        }
        void Start()
        {
            // Start with the current item's transform
            Transform current = transform;


            // check if "hand" is a parent
            while (current != null)
            {
                if (current.name == "Hand")
                {
                    Equipment.slotFull = true;
                    rd.isKinematic = true;
                    return;
                }
                current = current.parent;
            }

            // If we reached here, the item is not a child of "Hand"
            gameObject.layer = LayerMask.NameToLayer("Interactable");

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                gameObject.GetComponent<SimpleTooltip>().HideTooltip();
            }
            
        }


        public override void OnFocus()
        {
            if (gameObject.GetComponent<SimpleTooltip>() != null)
            {
                gameObject.GetComponent<SimpleTooltip>().ShowTooltip();
            }

            if (!CheckWeapon())
            {
                print("Looking at " + gameObject.name);

            }
        }

        public override void OnInteract() // press e on object
        {
            if (!CheckWeapon()) // item is not weapon
            {
                if (gameObject.GetComponent<SimpleTooltip>() != null)
                {
                    gameObject.GetComponent<SimpleTooltip>().HideTooltip();
                }
                print("Interacted with " + gameObject.name);
                gameObject.GetComponent<ItemPickup>().Pickup();

            }
        }

        public override void OnLoseFocus()
        {
            if (gameObject.GetComponent<SimpleTooltip>() != null)
            {
                gameObject.GetComponent<SimpleTooltip>().HideTooltip();
            }

            if (!CheckWeapon())
            {
                print("Stopped Looking at " + gameObject.name);

            }
        }

        bool CheckWeapon() // if true item is weapon
        {
            if (gameObject.GetComponent<WeaponController>() != null && Equipment.slotFull == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}