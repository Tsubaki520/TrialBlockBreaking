using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [Header ("被撞到時候的位移")]
    public Vector3 offset;

    public static int MAX_LIFE = 9;
    public static int Life;
    public GameObject LifeObject;

    void Start()
    {
        if (SceneManager.GetActiveScene ( ).buildIndex == 1)
        {
            LifeReturn ( );
        }
        LifeObject.GetComponent<ShowNumber> ( ).Number = MAX_LIFE;
    }

    public static void LifeReturn()
    {
        Life = MAX_LIFE;
    }

    private void OnCollisionEnter2D( Collision2D other )
    {
        if (other.gameObject.CompareTag ("Ball"))
        {
            Life--;
            LifeObject.GetComponent<ShowNumber> ( ).Number = Life;
            //gameObject.transform.position += offset;
        }
        if (Life <= 0 && !GameManager.LevelClear)
        {
            GameManager.ReloadThisScene ( );
        }
    }
}
