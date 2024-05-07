using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if INVECTOR_MELEE
using Invector;
using Invector.vCharacterController;
#endif

#if FIRST_PERSON_CONTROLLER || THIRD_PERSON_CONTROLLER
using Opsive.UltimateCharacterController.Traits;
#endif

#if NEOFPS
using NeoFPS;
#endif

#if HQ_FPS_TEMPLATE
using HQFPSTemplate;
using DamageType = HQFPSTemplate.DamageType;
#endif

#if SURVIVAL_TEMPLATE_PRO
using PolymindGames;
#endif

namespace Breeze.Core
{
    public class BreezePlayer : MonoBehaviour
    {
        //Settings
        public float CurrentHealth = 100f;
        public Transform HitPosition;

        [HideInInspector] public UnityEvent<GameObject> gotAttackedEvent = new UnityEvent<GameObject>();

        private void OnValidate()
        {
            if (Application.isPlaying)
                return;

            if (HitPosition == null)
            {
                GameObject hitPos = new GameObject("Hit Position");
                hitPos.transform.SetParent(transform);
                hitPos.transform.localPosition = new Vector3(0, 0.75f, 0);
                hitPos.transform.localRotation.eulerAngles.Set(0, 0, 0);
                hitPos.transform.localScale = new Vector3(1, 1, 1);
                hitPos.AddComponent<BreezeHitPosition>();

                HitPosition = hitPos.transform;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                TakeDamage(10, null);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                PlayerHeal(10);
            }
        }


        private void LateUpdate()
        {
            CurrentHealth = GameManager.gameManager.playerHealth.Health;
        }


        public void TakeDamage(float damage, GameObject sender)
        {
            gotAttackedEvent.Invoke(sender);
            GameManager.gameManager.playerHealth.DamageUnit(damage);
            Debug.Log("Player Damage Taken: " + damage + ". Player Health = " + GameManager.gameManager.playerHealth.Health);
        }

        public void PlayerHeal(float heal)
        {
            GameManager.gameManager.playerHealth.HealUnit(heal);
            Debug.Log("Player Healed: " + heal + ". Player Health = " + GameManager.gameManager.playerHealth.Health);
        }
    }
}