using System;
using System.Collections;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static Equipment equipment;

    public static bool slotFull;
    public static Item item;

    public float throwForce = 300f;

    private GameObject hand;
    private Camera cam;
    private GameObject player;
    private GameObject dropPosition;
    Animator _animator;
    [SerializeField] private AudioClip[] equipSound;
    [SerializeField] private AudioSource _audioSource;

    [Header("Attack")]
    bool canAttack = true;
    public float cooldown = 5.0f;

    void Awake() // Singleton pattern
    {
        if (equipment != null && equipment != this) 
        {
            Destroy(this);
        }
        else
        {
            equipment = this;
        }
    }
    void Start()
    {
        dropPosition = GameObject.FindGameObjectWithTag("DropPosition");
        hand = GameObject.FindGameObjectWithTag("Hand");
        player = GameObject.FindGameObjectWithTag("Player");
        cam = player.GetComponentInChildren<Camera>();
        _animator = player.GetComponentInChildren<Animator>();

        slotFull = false;
        item = null;
    }

    private void LateUpdate()
    {
        if (slotFull && item != null) // item is equiped 
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropItem();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (item.itemType == Item.ItemType.Weapon && canAttack) // use Item
                {
                    UseWeapon();
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Slot full and item valid. Item name = " + item.name);
            }
        }
    }

    public void EquipWeapon(Item item)
    {
        SetupWeapon(true, item);
        GameObject equipmentObj = Instantiate(item.prefab, hand.transform.position, hand.transform.rotation, hand.transform);
        equipmentObj.layer = LayerMask.NameToLayer("Equipment");
        Debug.Log("Equipped Weapon " + item.name);
    }

    public void DropItem()
    {
        Debug.Log("Dropped " + item.name);
        Rigidbody rb = hand.GetComponentInChildren<Rigidbody>();
        rb.isKinematic = false;
        rb.transform.parent = null;
        rb.gameObject.layer = LayerMask.NameToLayer("Interactable");
        Vector3 direction = cam.transform.forward;
        rb.AddForce(direction * 300);

        slotFull = false;
        item = null;
    }


    public void UseWeapon()
    {
        Debug.Log("Used " + item.name);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        canAttack = false;
        _animator.SetTrigger("Swing");
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }


    public void SetupWeapon(bool slot, Item newItem)
    {
        slotFull = true;
        item = newItem;
        _audioSource.PlayOneShot(equipSound[UnityEngine.Random.Range(0, equipSound.Length - 1)]);
    }
}

