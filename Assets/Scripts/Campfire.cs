using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Campfire : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject lightObject;
    [SerializeField] private AudioSource campfireAudio;
    private bool isOn = false;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Collided with " + collision);
        if (isOn)
        {
            return;
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                animator.SetBool("isLit", true);
                Invoke("setOn", 0.5f);
                isOn = true;
            }
        }
    }

    private void setOn()
    {
        lightObject.SetActive(true);
        campfireAudio.Play();
    }
}
