using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    [Header ("子彈移動速度")]
    private float m_bulletSpeed;

    void Update()
    {
        UnPause ( );
        transform.Translate (m_bulletSpeed, 0, 0);
    }

    public void Pause()
    {
        m_bulletSpeed = 0;
    }

    public void UnPause()
    {
        m_bulletSpeed = 0.05f;
    }

    void OnTriggerEnter2D( Collider2D other )
    {
        if (other.gameObject.CompareTag ("Wall"))
        {
            Destroy (gameObject);
        }
        if (other.gameObject.CompareTag ("Block"))
        {
            Destroy (other.gameObject);
            GameManager.Score += 5;
            GameManager.BrickCount--;
            Destroy (gameObject);
        }
    }
}
