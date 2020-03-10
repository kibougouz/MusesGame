using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushButton : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //テキストを点滅させる処理
        text.color = new Color(1, 1, 1, (Mathf.Sin(Time.time * 2) + 1f) * 0.5f);
    }
}
