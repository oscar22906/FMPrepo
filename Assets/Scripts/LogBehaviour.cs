using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBehaviour : MonoBehaviour
{
    [Header("Stump Health Stats")]
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth = 2;

    [Header("Stump stuff")]
    [SerializeField] private Mesh mesh;
    [SerializeField] private Item woodItem;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private float pushForce;
    private AudioSource AudioSource;
    [SerializeField] private AudioClip[] audioClips;
    Rigidbody rb;
    Camera cam;
    GameObject player;
    SimpleTooltip simpleTooltip;

    void Awake()
    {
        GameObject WA = GameObject.FindGameObjectWithTag("WoodAudio");
        AudioSource = WA.GetComponent<AudioSource>();
        currentHealth = maxHealth;
        meshFilter = GetComponent<MeshFilter>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        cam = player.GetComponentInChildren<Camera>();
    }
    private void Start()
    {
        simpleTooltip = GetComponent<SimpleTooltip>();
        simpleTooltip.infoRight = "^Strength: `" + currentHealth;

    }

    public void TakeDamage()
    {
        rb.isKinematic = false;
        Vector3 direction = cam.transform.forward;
        rb.AddForce(direction * pushForce);
        if (AudioSource != null)
        {
            AudioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)]);
        }
        print("Chopping Log");
        currentHealth -= 1;
        InventoryManager.Instance.Add(woodItem);
        if (currentHealth == 1)
        {
            meshFilter.mesh = mesh;
        }
        if (currentHealth <= 0)
        {
            DestroyLog();
        }
    }

    void DestroyLog()
    {
        print("Log destroyed");
        gameObject.GetComponent<SimpleTooltip>().HideTooltip();
        Destroy(gameObject);
    }
}

