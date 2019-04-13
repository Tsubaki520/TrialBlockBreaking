using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables 変数
    [Header ("可打破的磚塊初始數量")]
    public static int BrickCount;

    public GameObject ScoreObject;
    public GameObject HightScoreObject;
    public static int Score;
    public static int HightScore;
    public static bool PauseEnable;
    public GameObject PauseImage;
    public GameObject ReturnButton;
    public GameObject QuitButton;
    public GameObject [ ] IconList;
    public GameObject [ ] ItemList;
    public GameObject Ball;
    public GameObject Player;
    public GameObject Bullet;
    public GameObject DIO;
    public AudioClip TheWorld;
    public bool TouchItem1, TouchItem2, TouchItem3, TouchItem4;
    #endregion

    void Start()
    {
        PauseEnable = false;
        Input.multiTouchEnabled = true;
        Time.timeScale = 1;
        Ball = GameObject.FindGameObjectWithTag ("Ball");
        GameObject [ ] Bricks = GameObject.FindGameObjectsWithTag ("Block");
        BrickCount = Bricks.Length;
        //Debug.Log("一開始有 " + BrickCount + " 個可打破的磚塊");

        TouchItem1 = false;
        TouchItem2 = false;
        TouchItem3 = false;
        TouchItem4 = false;

        if (SceneManager.GetActiveScene ( ).buildIndex == 1)
        {
            Score = 0;
        }
        if (SceneManager.GetActiveScene ( ).buildIndex == 3)
        {
            HightScoreObject.GetComponent<ShowNumber> ( ).Number = HightScore;
        }
    }

    public static bool LevelClear
    {
        get
        {
            if (SceneManager.GetActiveScene ( ).buildIndex == 3)
            {
                return false;
            }
            if (BrickCount <= 0)
            {
                return true;
            }
            return false;
        }
    }

    public static void ReloadThisScene()
    {
        Scene Current = SceneManager.GetActiveScene ( );
        SceneManager.LoadScene (Current.name);
        Death.LifeReturn ( );
    }

    void Update()
    {
        ScoreObject.GetComponent<ShowNumber> ( ).Number = Score;
        CheckLevelClearOrNot ( );

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            PauseEnable = !PauseEnable;
            if (PauseEnable)
            {
                PauseEnable = false;
                Time.timeScale = 0;
                PauseImage.SetActive (true);
                PauseAll ( );
            }
            else
            {
                PauseEnable = true;
                Time.timeScale = 1;
                PauseImage.SetActive (false);
                UnPauseAll ( );
            }
        }
        SetItemMode ( );
        CheckTouchItem ( );

        #region cheat system PC用
        if (Input.GetKeyDown (KeyCode.Alpha1))
        {

        }
        if (Input.GetKeyDown (KeyCode.Alpha2))
        {

        }
        if (Input.GetKeyDown (KeyCode.Alpha3))
        {

        }
        if (Input.GetKeyDown (KeyCode.Alpha4))
        {

        }
        if (Input.GetKeyDown (KeyCode.Alpha5))
        {

        }
        if (Input.GetKeyDown (KeyCode.B))
        {

        }
        #endregion
    }

    public static void CheckLevelClearOrNot()
    {
        if (LevelClear)
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene ( ).buildIndex + 1);
            if (Score > HightScore)
                HightScore = Score;
        }
    }

    void PauseAll()
    {
        Ball.GetComponent<BallControl> ( ).Pause ( );
        Bullet.GetComponent<bullet> ( ).Pause ( );
        Player.GetComponent<Player> ( ).Pause ( );
    }

    void UnPauseAll()
    {
        Ball.GetComponent<BallControl> ( ).UnPause ( );
        Bullet.GetComponent<bullet> ( ).UnPause ( );
        Player.GetComponent<Player> ( ).UnPause ( );
    }

    void SetItemMode()
    {
        if (SceneManager.GetActiveScene ( ).buildIndex != 3)
        {
            if (ItemManager.EatNum == 1)
            {
                IconList [0].GetComponent<SpriteRenderer> ( ).color = Color.white;
            }
            else if (ItemManager.EatNum == 2)
            {
                IconList [1].GetComponent<SpriteRenderer> ( ).color = Color.white;
            }
            else if (ItemManager.EatNum == 3)
            {
                if (Player.transform.localScale.x <= 1.75f)
                {
                    Player.transform.localScale += new Vector3 (0.25f, 0, 0);
                }
                ItemManager.EatNum = 0;
                BallControl.ItemNum = 0;
            }
            else if (ItemManager.EatNum == 4)
            {
                if (Player.transform.localScale.x >= 0.5f)
                {
                    Player.transform.localScale += new Vector3 (-0.25f, 0, 0);
                }
                ItemManager.EatNum = 0;
                BallControl.ItemNum = 0;
            }
            else if (ItemManager.EatNum == 5)
            {
                IconList [3].GetComponent<SpriteRenderer> ( ).color = Color.white;
            }
        }
    }
    void CheckTouchItem()
    {
        if (ItemManager.EatNum == 1 && TouchItem1)
        {
            Player.GetComponent<Player> ( ).AddBullet (10);
            IconList [0].GetComponent<SpriteRenderer> ( ).color = Color.gray;
            BallControl.ItemNum = 0;
            TouchItem1 = false;
        }
        else if (ItemManager.EatNum == 2 && TouchItem2)
        {
            Vector3 pos = Ball.transform.position;
            Instantiate (Ball, pos + new Vector3 (0, 0.2f, 0), Quaternion.identity);
            IconList [1].GetComponent<SpriteRenderer> ( ).color = Color.gray;
            BallControl.ItemNum = 0;
            TouchItem2 = false;
        }
        else if (ItemManager.EatNum == 3 && TouchItem3)
        {
            Player.transform.localScale += new Vector3 (0.25f, 0, 0);
            IconList [2].GetComponent<SpriteRenderer> ( ).color = Color.gray;
            BallControl.ItemNum = 0;
            TouchItem3 = false;
        }
        else if (ItemManager.EatNum == 5 && TouchItem4)
            StartCoroutine (StartTheWorld ( ));
    }

    IEnumerator StartTheWorld()
    {
        Time.timeScale = 0.1f;
        GetComponent<AudioSource> ( ).clip = TheWorld;
        GetComponent<AudioSource> ( ).Play ( );
        for (float alpha = 0; alpha < 1; alpha += 0.05f)
        {
            DIO.GetComponent<SpriteRenderer> ( ).color += new Color (0, 0, 0, alpha);
            yield return null;
        }
        DIO.GetComponent<SpriteRenderer> ( ).color = new Color (1, 1, 1, 0);
        for (int i = 0; i < 1; i++)
        {
            Instantiate (Ball, new Vector3 (Random.Range (-0.1f, 0.1f), 2, 0), Quaternion.identity);
        }
        yield return null;
        Time.timeScale = 1;
        BallControl.ItemNum = 0;
        TouchItem4 = false;
    }

    public void TouchIcon( int TouchItemNum )
    {
        if (TouchItemNum == 1)
            TouchItem1 = true;
        if (TouchItemNum == 2)
            TouchItem2 = true;
        if (TouchItemNum == 3)
            TouchItem3 = true;
        if (TouchItemNum == 4)
            TouchItem4 = true;
    }
}
