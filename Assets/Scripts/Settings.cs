using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System;

public class Settings : MonoBehaviour {
    
    public Dropdown framerateDropdown;
    public Toggle framerateToggle;
    public GameObject settingsPanel;
    public GameObject fpsCounter;
    public InputField colorHex;

    public Material tableMat;

    void Awake () {
        framerateDropdown.value = PlayerPrefs.GetInt("framerateDropdown", 0);
        KeyHandler3D.isFramerateCounterVisible = Convert.ToBoolean(PlayerPrefs.GetInt("isFramerateCounterVisible", 1));
        fpsCounter.SetActive(KeyHandler3D.isFramerateCounterVisible);
        framerateToggle.isOn = KeyHandler3D.isFramerateCounterVisible;
        KeyHandler3D.fpsTarget = GetFramerate(framerateDropdown.value);
        string col = PlayerPrefs.GetString("colorHex", "#373737");
        colorHex.text = col;
    }

    public void SetFramerate (int choice) {
        PlayerPrefs.SetInt("framerateDropdown", choice);
        PlayerPrefs.Save();
        switch (choice) {
            case 0:
                KeyHandler3D.fpsTarget = 15;
                break;
            case 1:
                KeyHandler3D.fpsTarget = 30;
                break;
            case 2:
                KeyHandler3D.fpsTarget = 60;
                break;
        }
    }

    public void SetFramerateCounter(bool state) {
        PlayerPrefs.SetInt("isFramerateCounterVisible", Convert.ToInt32(state));
        KeyHandler3D.isFramerateCounterVisible = state;
        fpsCounter.SetActive(state);
    }

    public int GetFramerate (int choice) {
        switch (choice) {
            case 0:
                return 15;
            case 1:
                return 30;
            case 2:
                return 60;
            default:
                return 60;
        }
    }

    public void ChangeTabletopColor () {
        try {
            Color tempColor = HexToColor(colorHex.text);
            PlayerPrefs.SetString("colorHex", colorHex.text);
            tableMat.SetColor("_Color", tempColor);
        } catch (Exception e) {
            Debug.Log("Invalid color string passed: " + e);
        }
    }

    // hex to color
    public Color HexToColor (string colorcode) {
        colorcode = colorcode.TrimStart('#');
        Color col;
        col = new Color((float)int.Parse(colorcode.Substring(0, 2), NumberStyles.HexNumber) / 255f,
                        (float)int.Parse(colorcode.Substring(2, 2), NumberStyles.HexNumber) / 255f,
                        (float)int.Parse(colorcode.Substring(4, 2), NumberStyles.HexNumber) / 255f,
                        1);
        return col;
    }

    public void SettingsUI () {
        if (settingsPanel.activeSelf == true) {
            settingsPanel.SetActive(false);
        } else {
            settingsPanel.SetActive(true);
        }
    }
}
