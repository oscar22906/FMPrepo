using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumpBehaviour : MonoBehaviour
{
    [Header("Stump Health Stats")]
    [SerializeField] public int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int minHealth;

    [Header("Stump stuff")]
    [SerializeField] private float pushForce;
    private AudioSource AudioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] public float offsetHeight;
    [SerializeField] public float offsetLength;
    [SerializeField] private GameObject logsPrefab;
    Rigidbody rb;
    Camera cam;
    GameObject player;
    SimpleTooltip simpleTooltip;

    void Awake()
    {
        GameObject WA = GameObject.FindGameObjectWithTag("WoodAudio");
        AudioSource = WA.GetComponent<AudioSource>();
        currentHealth = Random.Range(minHealth, maxHealth);
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
        print("Chopping Stump");
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            DestroyStump();
        }
    }

    void DestroyStump()
    {
        print("Stump destroyed");

        Vector3 pos = gameObject.transform.position;
        pos += Vector3.up * offsetHeight; pos += gameObject.transform.forward * offsetLength;
        Instantiate(logsPrefab, pos, gameObject.transform.rotation);
        gameObject.GetComponent<SimpleTooltip>().HideTooltip();
        Destroy(gameObject);
    }
}
