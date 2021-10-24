using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHandler3D : MonoBehaviour {

    public static float heightChangeVlaue = 0.5f;
    //passed as argument in function
    public GameObject[] fnRowSetter;
    public GameObject[] numRowSetter;
    public GameObject[] tabRowSetter;
    public GameObject[] capsRowSetter;
    public GameObject[] shiftRowSetter;
    public GameObject[] spaceRowSetter;
    public GameObject[] arrowsSetter;
    //setter for above array
    public static GameObject[] fnRowKeys;
    public static GameObject[] numRowKeys;
    public static GameObject[] tabRowKeys;
    public static GameObject[] capsRowKeys;
    public static GameObject[] shiftRowKeys;
    public static GameObject[] spaceRowKeys;
    public static GameObject[] arrowsKeys;


    //string names for the keys
    private static string[] keyNames = {"Oem3", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0", "OemMinus", "Oemplus", "Back",
                                        "Tab", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "Oem4", "Oem6",
                                        "CapsLock", "A", "S", "D", "F", "G", "H", "J", "K", "L", "OemSemicolon", "Oem7", "OemPipe", "Return",
                                        "LShiftKey", "Z", "X", "C", "V", "B", "N", "M", "Oemcomma", "OemPeriod", "Oem2", "RShiftKey",
                                        "LControlKey", "LWin", "LMenu", "Space", "RMenu", "RWin", "Apps", "RControlKey",
                                        "Up", "Left", "Down", "Right",
                                        "Insert", "Home", "Prior", "Delete", "End", "Next" };

    public static string[] fnRowNames = { "Escape", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0", "OemMinus", "Oemplus", "Back" };
    public static string[] numRowNames = { "Escape", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0", "OemMinus", "Oemplus", "Back" };
    public static string[] tabRowNames = { "Tab", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "Oem4", "Oem6", "OemPipe" };
    public static string[] capsRowNames = { "Capital", "A", "S", "D", "F", "G", "H", "J", "K", "L", "OemSemicolon", "Oem7", "Enter" };
    public static string[] shiftRowNames = { "LShiftKey", "Z", "X", "C", "V", "B", "N", "M", "Oemcomma", "OemPeriod", "Oem2", "RShiftKey" };
    public static string[] spaceRowNames = { "LControlKey", "LWin", "LMenu", "Space", "RMenu", "RWin", "Apps", "RControlKey" };
    public static string[] arrowsNames = { "Up", "Left", "Down", "Right" };

    private static Dictionary<string, string> keyRows = new Dictionary<string, string>() {
        {"Escape", "numRow"}, {"D1", "numRow"}, {"D2", "numRow"}, {"D3", "numRow"}, {"D4", "numRow"}, {"D5", "numRow"}, {"D6", "numRow"}, {"D7", "numRow"}, {"D8", "numRow"}, {"D9", "numRow"}, {"D0", "numRow"}, {"OemMinus", "numRow"}, {"Oemplus", "numRow"}, {"Back", "numRow"},
        {"Tab", "tabRow"}, {"Q", "tabRow"}, {"W", "tabRow"}, {"E", "tabRow"}, {"R", "tabRow"}, {"T", "tabRow"}, {"Y", "tabRow"}, {"U", "tabRow"}, {"I", "tabRow"}, {"O", "tabRow"}, {"P", "tabRow"}, {"Oem4", "tabRow"}, {"Oem6", "tabRow"}, {"OemPipe", "tabRow"},
        {"Capital", "capsRow"}, {"A", "capsRow"}, {"S", "capsRow"}, {"D", "capsRow"}, {"F", "capsRow"}, {"G", "capsRow"}, {"H", "capsRow"}, {"J", "capsRow"}, {"K", "capsRow"}, {"L", "capsRow"}, {"OemSemicolon", "capsRow"}, {"Oem7", "capsRow"}, {"Enter", "capsRow"},
        {"LShiftKey", "shiftRow"}, {"Z", "shiftRow"}, {"X", "shiftRow"}, {"C", "shiftRow"}, {"V", "shiftRow"}, {"B", "shiftRow"}, {"N", "shiftRow"}, {"M", "shiftRow"}, {"Oemcomma", "shiftRow"}, {"OemPeriod", "shiftRow"}, {"Oem2", "shiftRow"}, {"RShiftKey", "shiftRow"},
        {"LControlKey", "spaceRow"}, {"LWin", "spaceRow"}, {"LMenu", "spaceRow"}, {"Space", "spaceRow"}, {"RMenu", "spaceRow"}, {"RWin", "spaceRow"}, {"Apps", "spaceRow"}, {"RControlKey", "spaceRow"},
        {"Up", "arrows"}, {"Left", "arrows"}, {"Down", "arrows"}, {"Right", "arrows"}
    };

    // Use this for initialization
    void Awake () {
        //initialize static gameobject array from inspector values
        fnRowKeys = fnRowSetter;
        numRowKeys = numRowSetter;
        tabRowKeys = tabRowSetter;
        capsRowKeys = capsRowSetter;
        shiftRowKeys = shiftRowSetter;
        spaceRowKeys = spaceRowSetter;
        arrowsKeys = arrowsSetter;
    }

    private void Update () {
        
    }

    /// <summary>
    /// Change color of key in scene based on key press
    /// </summary>
    /// <param name="key">gameObject - reference to key sprite in scene</param>
    /// <param name="pressed">bool - whether key pressed or released</param>
    static void ChangeKeyHeight (GameObject key, bool pressed) {
        try {
            Vector3 pos = key.gameObject.GetComponent<Transform>().position;
            if (pos.y > heightChangeVlaue*3) {
                if (pressed) {
                    key.GetComponent<Transform>().position = new Vector3(pos.x, pos.y - heightChangeVlaue, pos.z);
                }
            } 
            if (!pressed) {
                key.GetComponent<Transform>().position = new Vector3(pos.x, pos.y + heightChangeVlaue, pos.z);
            }
        } catch (Exception ex) {
            Debug.Log(ex);
        }
    }

    /// <summary>
    /// Takes string argument and calls ChangeKeyColor acting as a wrapper
    /// </summary>
    /// <param name="searchString">string - name of key</param>
    /// <param name="pressed">bool - whether key pressed or released</param>
    public static void PressKey (string searchString, bool pressed) {
        int index;
        try {
            string rowName = keyRows[searchString];
            if (rowName == "numRow") {
                index = Array.IndexOf(numRowNames, searchString);
                ChangeKeyHeight(numRowKeys[index], pressed);
            } else if (rowName == "tabRow") {
                index = Array.IndexOf(tabRowNames, searchString);
                ChangeKeyHeight(tabRowKeys[index], pressed);
            } else if (rowName == "capsRow") {
                index = Array.IndexOf(capsRowNames, searchString);
                ChangeKeyHeight(capsRowKeys[index], pressed);
            } else if (rowName == "shiftRow") {
                index = Array.IndexOf(shiftRowNames, searchString);
                ChangeKeyHeight(shiftRowKeys[index], pressed);
            } else if (rowName == "spaceRow") {
                index = Array.IndexOf(spaceRowNames, searchString);
                ChangeKeyHeight(spaceRowKeys[index], pressed);
            } else if (rowName == "arrows") {
                index = Array.IndexOf(arrowsNames, searchString);
                ChangeKeyHeight(arrowsKeys[index], pressed);
            }
        } catch (Exception ex) {
            Debug.Log(ex);
        }
    }
}
