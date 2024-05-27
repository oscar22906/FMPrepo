using Breeze.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    Camera m_Camera;

    public Vector3 interactionRayPoint;
    public float interactionDistance;

    GameObject player;

    [Header("Leaf Destruction")]
    [SerializeField] GameObject leafPrefab;

    void Awake()
    {
        m_Camera = gameObject.transform.parent.GetComponentInChildren<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    public void Hit()
    {
        Weapon weapon = player.GetComponentInChildren<WeaponController>().Weapon;
        if (Physics.Raycast(m_Camera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if (hit.transform.GetComponent<TreeBehaviour>() != null && hit.transform.GetComponent<TreeBehaviour>().currentHealth >= 0)
            {
                HitTree(hit);
            }
            if (hit.transform.GetComponent<StumpBehaviour>() != null && hit.transform.GetComponent<StumpBehaviour>().currentHealth >= 0)
            {
                HitStump(hit);
            }
            if (hit.transform.GetComponent<LogBehaviour>() != null && hit.transform.GetComponent<LogBehaviour>().currentHealth >= 0)
            {
                HitLog(hit);
            }
            if (hit.transform.GetComponent<BreezeDamageable>() != null)
            {
                float damage = Random.Range(weapon.minDamage, weapon.maxDamage);
                hit.transform.GetComponent<BreezeDamageable>().TakeDamage(damage, this.gameObject, true);
                print("Hit " + hit.transform.name + " for " +  damage + " Damage.");
            }
        }
    }
    void HitLog(RaycastHit hit)
    {
        Vector3 pos = gameObject.transform.position; pos.y += 4;
        //Instantiate(leafPrefab, pos, leafPrefab.transform.rotation); // wood break effect
        hit.collider.gameObject.GetComponent<LogBehaviour>().TakeDamage();
    }
    void HitStump(RaycastHit hit)
    {
        Vector3 pos = gameObject.transform.position; pos.y += 4;
        //Instantiate(leafPrefab, pos, leafPrefab.transform.rotation); // wood break effect
        hit.collider.gameObject.GetComponent<StumpBehaviour>().TakeDamage();
    }

    void HitTree(RaycastHit hit)
    {
        Vector3 pos = gameObject.transform.position; pos.y += 4;
        Instantiate(leafPrefab, pos, leafPrefab.transform.rotation);
        hit.collider.gameObject.GetComponent<TreeBehaviour>().TakeDamage();
    }
}
