using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Opcion 1 - Apagar el monton de cosas y cargar el resto
        // Opcion 2 - Cargar la otra escena
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
