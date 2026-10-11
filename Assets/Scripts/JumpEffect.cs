using UnityEngine;

public class JumpEffect : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.4f;
    private float timer;
    private bool active = false;

    private void Update()
    {
        if (!active) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            gameObject.SetActive(false);
            active = false;
        }
    }

    public void Play(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        active = true;
        timer = lifetime;
    }
}