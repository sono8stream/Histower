using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class UserData
{
    public Dictionary<string, int> variableDict;
    public  List<Samurai> samuraiList;

    public static UserData instance = new UserData();

    private UserData()
    {
        samuraiList = new List<Samurai>();
        samuraiList.Add(new Samurai("宮本", "武蔵", 175, 100, 80, 50, 50,10000));
        samuraiList.Add(new Samurai("佐々木", "小次郎", 170, 100, 60, 60, 40,10000));

        variableDict = SaveManager.LoadVariableDict();
        if (variableDict == null) variableDict = new Dictionary<string, int>();
    }
}
