﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class TextProcessing : MonoBehaviour
{
    //ここから下をシリアライゼーションする
    [SerializeField]
#pragma warning disable 649
    Text scenarioMessage;
#pragma warning disable 649

    //C#のクラスの宣言
    Scenario currentScenario;

    int index = 0;
    public string keyname = "";

    //読み込んだテキストを格納するリストの準備
    List<string> textFiles = new List<string>();

    //リストクラスを使う為の準備
    List<Scenario> scenarios = new List<Scenario>();

    //===C+のクラス===//
    public class Scenario
    {
        public int ScenarioID;
        public List<string> Texts;
        public List<string> Options;
        public int NextScenarioID;
    }
    //===============//

    //C+のコンストラクト
    void Start()
    {
        //ReadFileText()で各読み込みたいテキストを指定する。
        ReadFileText("tesuto.txt",0);
        ReadFileText("tesuto01.txt",1);
        ReadFileText("tesuto02.txt", 2);
        SetScenario(scenarios[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScenario != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetNextMessage();
            }
            if (Input.GetButtonDown("" + keyname))
            {
                SetNextMessage();
            }
        }
    }

    //====================================ここからC+の関数(メソッド)=======================================//

    //テキストデータを読み込んで使用する為の関数
    void ReadFileText(string fileName,int no)
    {
        //使いたいテキストデータがあるアドレスを指定する
        string path = Application.dataPath + @"\Test\StoryText\" + fileName; 
        //====もしﾌｧｲﾙが存在していなければここで作成====//
        if (!File.Exists(path))
        {
            using (File.Create(path)) { }
        }
        textFiles.Add(path);
        //==========================================//

        //型推論(var)scenarioを使いシナリオクラスのテキストを初期化・・・C+やったらautoになる
        var scenario = new Scenario()
        {
            ScenarioID = no,
            Texts = new List<string>(),
            Options = new List<string>(),
            NextScenarioID = no + 1
        };

        //usingはプログラムがアクセスする場所を定義する
        using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
        {
            //EndOfStreamプロパティを使用してﾌｧｲﾙの読み込みが完了したか判定する
            while (!streamReader.EndOfStream)
            {
                scenario.Texts.Add(streamReader.ReadLine());
            }
        }
        scenarios.Add(scenario);
    }

    //最初に読み込みたいテキストを設定する為の関数
    void SetScenario(Scenario scenarios)
    {
        currentScenario = scenarios;
        scenarioMessage.text = currentScenario.Texts[0];
    }

    /* 1.テキストの中の文を順番に表示させる処理
     * 2.テキスト文を全部表示させたらExitScenario();を読み込む*/
    void SetNextMessage()
    {
        if(currentScenario.Texts.Count > index + 1)
        {
            index++;
            scenarioMessage.text = currentScenario.Texts[index];
        }
        else
        {
            ExitScenario();
        }
    }

    //読み込んだテキストを全て表示させたら次のテキストを読み込む処理
    void ExitScenario()
    {
        scenarioMessage.text = "";
        index = 0;
        if (currentScenario.NextScenarioID == scenarios.Count)
        {
            currentScenario = null;
        }
        else
        {
            var nextScenario = scenarios.Find(s => s.ScenarioID == currentScenario.NextScenarioID);
            currentScenario = nextScenario;
            scenarioMessage.text = currentScenario.Texts[0];
        }
    }

    //==================================================================================================//
}