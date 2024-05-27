using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    [Tooltip("Takes 1 damage per hit")]
    [Header("Tree Health Stats")]
    [SerializeField] public int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int minHealth;

    [Header("Tree stuff")]
    [Tooltip("Force when tree falls")]
    [SerializeField] private float pushForce;
    [Tooltip("Time for tree to drop logs")]
    [SerializeField] private float decayTime;
    [SerializeField] private GameObject choppedTreePrefab;
    [SerializeField] private float offsetHeight;
    private AudioSource AudioSource;
    [SerializeField] private AudioClip[] hitAudioClips;
    [SerializeField] private AudioClip[] fallAudioClips;
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
        simpleTooltip = GetComponent<SimpleTooltip>();
        simpleTooltip.infoRight = "^Strength: `" + currentHealth;
    }

    public void TakeDamage()
    {
        print("Chopping Tree");
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            TreeFall();
        }
        if (AudioSource != null)
        {
            AudioSource.PlayOneShot(hitAudioClips[Random.Range(0, hitAudioClips.Length - 1)]);
        }

    }
    void TreeFall()
    {
        if (AudioSource != null)
        {
            AudioSource.PlayOneShot(fallAudioClips[Random.Range(0, fallAudioClips.Length - 1)]);
        }
        print("Tree Fall");
        rb.isKinematic = false;
        Vector3 direction = cam.transform.forward;
        rb.AddForce(direction * pushForce);
        StartCoroutine(TreeDecay());
    }
    IEnumerator TreeDecay()
    {
        yield return new WaitForSeconds(decayTime);
        print("Kill tree");
        Vector3 pos = gameObject.transform.position;
        pos += Vector3.up * offsetHeight; pos.y += 1;
        Instantiate(choppedTreePrefab, pos, gameObject.transform.rotation);
        gameObject.GetComponent<SimpleTooltip>().HideTooltip();
        if (AudioSource != null)
        {
            AudioSource.PlayOneShot(hitAudioClips[Random.Range(0, hitAudioClips.Length - 1)]);
        }
        Destroy(gameObject);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
