using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField]
    private float m_speedX;

    [SerializeField]
    private float m_speedY;

    [SerializeField]
    private AudioClip [ ] Clips;

    [SerializeField]
    private GameObject [ ] ItemList;

    private Rigidbody2D m_ballRigidbody2D;
    private CircleCollider2D m_ballCircleCollider2D;
    public static int ItemNum;

    void Start()
    {
        m_ballRigidbody2D = GetComponent<Rigidbody2D> ( );
        m_ballCircleCollider2D = GetComponent<CircleCollider2D> ( );
        ItemNum = 0;
    }

    void Update()
    {
        if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0))
        {
            BallStart ( );
        }
    }

    void BallStart()
    {
        if (IsStop ( ))
        {
            m_ballRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            m_ballCircleCollider2D.enabled = true;
            transform.SetParent (null);
            m_ballRigidbody2D.velocity = new Vector2 (m_speedX, m_speedY);
        }
    }
    bool IsStop()
    {
        return m_ballRigidbody2D.velocity == Vector2.zero;
    }

    public void Pause()
    {
        IsStop ( );
    }

    public void UnPause()
    {
        BallStart ( );
    }

    void OnCollisionEnter2D( Collision2D other )
    {
        Lockspeed ( );
        if (other.gameObject.CompareTag ("Block"))
        {
            GetComponent<AudioSource> ( ).clip = Clips [0];
            GetComponent<AudioSource> ( ).Play ( );
            other.gameObject.transform.GetChild (0).gameObject.SetActive (true);
            GameManager.Score += 10;
            GameManager.BrickCount--;
            IEnumerator HitBlock = DestoryBlock (other);
            StartCoroutine (HitBlock);
            Vector3 pos = other.transform.position;
            if (GameManager.BrickCount == 50)
            {
                GameObject originalItem = ItemList [4];
                GameObject clone = Instantiate (originalItem, pos, Quaternion.identity);
                clone.name = originalItem.name;
                ItemNum = 5;
            }
            else
            {
                GameObject originalItem = ItemList [Random.Range (0, 4)];
                GameObject clone = Instantiate (originalItem, pos, Quaternion.identity);
                clone.name = originalItem.name;

                for (int i = 1; i < 5; i++)
                {
                    if (clone.name == ItemList [i - 1].name)
                    {
                        ItemNum = i;
                    }
                }
            }
        }

        if (other.gameObject.CompareTag ("Player") && !IsStop ( ))
        {
            other.gameObject.transform.GetChild (0).gameObject.SetActive (true);
            IEnumerator Effect = BallEffect (other);
            StartCoroutine (Effect);
            GetComponent<AudioSource> ( ).clip = Clips [1];
            GetComponent<AudioSource> ( ).Play ( );
        }
    }

    IEnumerator DestoryBlock( Collision2D other )
    {
        Destroy (other.gameObject);
        yield return null;
    }

    IEnumerator BallEffect( Collision2D other )
    {
        yield return null;
        other.gameObject.transform.GetChild (0).gameObject.SetActive (false);
    }

    void Lockspeed()
    {
        Vector2 lockSpeed = new Vector2 (ResetspeedX ( ), ResetspeedY ( ));
        m_ballRigidbody2D.velocity = lockSpeed;
    }

    float ResetspeedX()
    {
        float currentSpeedX = m_ballRigidbody2D.velocity.x;
        if (currentSpeedX < 0)
        {
            return -m_speedX;
        }
        else
        {
            return m_speedX;
        }
    }

    float ResetspeedY()
    {
        float currentSpeedY = m_ballRigidbody2D.velocity.y;
        if (currentSpeedY < 0)
        {
            return -m_speedY;
        }
        else
        {
            return m_speedY;
        }
    }
}
