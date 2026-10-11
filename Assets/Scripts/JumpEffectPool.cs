using System.Collections.Generic;
using UnityEngine;

public class JumpEffectPool : MonoBehaviour
{
    [SerializeField] private GameObject jumpEffectPrefab;
    [SerializeField] private int poolSize = 3;

    private List<JumpEffect> pool = new List<JumpEffect>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(jumpEffectPrefab);
            obj.SetActive(false);
            pool.Add(obj.GetComponent<JumpEffect>());
        }
    }

    public void PlayAt(Vector3 position)
    {
        foreach (JumpEffect effect in pool)
        {
            if (!effect.gameObject.activeSelf)
            {
                effect.Play(position);
                return;
            }
        }
    }
}