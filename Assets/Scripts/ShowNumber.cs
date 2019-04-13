using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNumber : MonoBehaviour
{
    public int Number;
    public GameObject [ ] Nums;
    public Sprite [ ] NumSprites;

    void Update()
    {
        ChangeNumbers ( );
    }

    void ChangeNumbers()
    {
        Nums [0].GetComponent<SpriteRenderer> ( ).sprite = NumSprites [Number % 10];
        Nums [1].GetComponent<SpriteRenderer> ( ).sprite = NumSprites [Number / 10 % 10];
        if (Nums.Length > 2)
        {
            Nums [2].GetComponent<SpriteRenderer> ( ).sprite = NumSprites [Number / 100 % 10];
            Nums [3].GetComponent<SpriteRenderer> ( ).sprite = NumSprites [Number / 1000];
        }
    }


}
