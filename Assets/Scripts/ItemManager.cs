using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    [Header ("生成したItem")]
    private GameObject m_item;

    public List<Sprite> List_Item = new List<Sprite> ( );
    public static bool EatItem;
    public static int EatNum;

    void Start()
    {
        m_item = this.gameObject;
    }

    void Update()
    {
        if (!GameManager.PauseEnable)
        {
            UnPause ( );
        }
        // fix BUG用
        if (this.transform.position.y <= -4)
        {
            Destroy (this.gameObject);
        }
    }

    public void Pause()
    {
        m_item.transform.position += new Vector3 (0, 0, 0);
    }

    public void UnPause()
    {
        m_item.transform.position += new Vector3 (0, -0.05f, 0);
    }

    private void OnTriggerEnter2D( Collider2D other )
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            CheckItem ( );
            Destroy (this.gameObject);
        }
        if (other.gameObject.CompareTag ("Wall"))
        {
            Destroy (this.gameObject);
        }
    }

    void CheckItem()
    {
        for (int i = 1; i < 6; i++)
        {
            if (BallControl.ItemNum == i)
            {
                EatNum = i;
            }
        }
    }
}
