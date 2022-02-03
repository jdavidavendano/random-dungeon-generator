using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static int _RoomSize = 10;
    public static int _DungeonSize = 70;
    public static int _Seed = 0;

    //[SerializeField] GameObject OptionsMenu;

    public void BackButton()
    {
        GameObject SeedObject = GameObject.Find("Seed");
        GameObject RoomObject = GameObject.Find("RoomSize");
        GameObject DungeonObject = GameObject.Find("DungeonSize");

        //string Test = OptionsMenu.GetComponentInChildren<TMPro.TMP_InputField>().text;

        _Seed = int.Parse(SeedObject.GetComponentInChildren<TMPro.TMP_InputField>().text);
        _RoomSize = (int)(RoomObject.GetComponentInChildren<Slider>().value);
        _DungeonSize = (int)(DungeonObject.GetComponentInChildren<Slider>().value);

        Debug.Log(_Seed + " " + _RoomSize + " " + _DungeonSize);

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
