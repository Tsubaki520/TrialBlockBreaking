using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Header ("水平移動速度")]
    private float m_speedX;

    [SerializeField]
    [Header ("Bullet生成速度")]
    private float m_bulletInterval;

    private float m_time;
    private int m_shots;
    private Rigidbody2D m_playerRigidbody2D;
    private Vector2 m_TouchPosition = Vector2.zero;

    public GameObject Bullet;
    public GameObject GameManager;

    void Start()
    {
        m_playerRigidbody2D = GetComponent<Rigidbody2D> ( );
    }

    private void FixedUpdate()
    {
#if ( UNITY_IOS || UNITY_ANDROID )
        MobileInput ( );  // 觸碰偵測
#endif
    }

    /*
    private static TouchPhase GetTouch()
    {
        #if (UNITY_IOS || UNITY_ANDROID)
                if (Input.touchCount > 0)
                    {
                        return (TouchPhase) ((int)Input.GetTouch(0).phase);
                    }
        #elif (UNITY_EDITOR || UNITY_STANDALONE)
                if (Input.GetMouseButtonDown(0))
                    {
                         return TouchPhase.Began;
                    }
                    if (Input.GetMouseButton(0))
                    {
                        return TouchPhase.Moved;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        return TouchPhase.Ended;
                    }
        #endif
    }*/ //舊切換輸入

    private void OnMouseDrag()
    {
        m_TouchPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        if (m_TouchPosition.x > -2.2f && m_TouchPosition.x < 2.2f)
        {
            transform.position = new Vector2 (m_TouchPosition.x, -3.35f);
        }
    }
    /*
    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Input.touches[0].phase = TouchPhase.Began;
        }
        if (Input.GetMouseButton(0))
        {
            Input.touches[0].phase = TouchPhase.Moved;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Input.touches[0].phase = TouchPhase.Ended;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }*/

    void MobileInput()
    {
        if (Input.touchCount > 0) //玩家正在觸控裝置
        {
            //取得玩家第一個觸控點
            Touch touch = Input.GetTouch (0);
            //判斷玩家的觸控狀態
            if (touch.phase == TouchPhase.Moved)
            {
                m_TouchPosition = Camera.main.ScreenToWorldPoint (Input.touches [0].position);
                //如果玩家觸控點在螢幕範圍內
                if (m_TouchPosition.x > -2.2f && m_TouchPosition.x < 2.2f)
                {
                    transform.position = new Vector2 (m_TouchPosition.x, -3.35f);
                }
            }
        }
        else
        {
            m_playerRigidbody2D.velocity = Vector2.zero;
        }
    }

    void Update()
    {
        m_time += Time.deltaTime;
        if (GameManager.GetComponent<GameManager> ( ).TouchItem1 == true && m_time > m_bulletInterval)
        {
            if (m_shots > 0)
            {
                float PlayerWidth = GetComponent<Renderer> ( ).bounds.size.x - 0.2f;
                float PlayerHeight = GetComponent<Renderer> ( ).bounds.size.y;

                Vector3 m_pos = new Vector3 (transform.position.x + ( PlayerWidth / 2 ), transform.position.y + PlayerHeight, 0);

                Quaternion m_rot = Bullet.transform.rotation;

                GameObject Bullet1 = Instantiate (Bullet, m_pos, m_rot);

                m_pos.x -= PlayerWidth;

                GameObject Bullet2 = Instantiate (Bullet, m_pos, m_rot);

                Bullet1.GetComponent<Rigidbody2D> ( ).velocity = new Vector2 (0f, 10f);
                Bullet2.GetComponent<Rigidbody2D> ( ).velocity = new Vector2 (0f, 10f);
                m_shots--;
                m_time = 0;
            }
        }
    }

    public void AddBullet( int Amount )
    {
        m_shots += Amount;
    }

    public void Pause()
    {
        m_playerRigidbody2D.velocity = Vector2.zero;
    }

    public void UnPause()
    {
        m_playerRigidbody2D.velocity = new Vector2 (m_speedX, 0);
    }

    /*
     
    float LeftOrRight()
    {
        return (Input.GetAxis("Horizontal") );
    }

    private void moveLeftOrRight(float m_SpeedX)
    {
        m_PlayerRigidbody2D.velocity = LeftOrRight() * new Vector2(m_SpeedX, 0);
        m_PlayerRigidbody2D.velocity = movement * new Vector2( m_SpeedX , 0);
    }*/ //舊版加速度移動
}
