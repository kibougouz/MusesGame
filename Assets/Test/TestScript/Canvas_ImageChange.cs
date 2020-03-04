using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_ImageChange : MonoBehaviour
{

    Image my_Image;
    //Set this in the Inspector
    public Sprite[] my_Sprite;
    public string keyname = "";
    public int MaxResetCnt = 0;
    private int Sprite_No;


    void Start()
    {
        my_Image = GetComponent<Image>();
        Sprite_No = 0;
    }


    void Update()
    {
        //文字列の名前のボタンが押されたらの処理
        if (Input.GetButtonDown(""+keyname))
        {
            my_Image.sprite = my_Sprite[Sprite_No];
            Sprite_No += 1;//ここで順番に表示させる為に＋している。
            if (Sprite_No == MaxResetCnt) Sprite_No = 0;
        }
    }
}
