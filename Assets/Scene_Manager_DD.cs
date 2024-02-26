using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Manager_DD : MonoBehaviour
{
    //use this script to switch between Menu and Game scene.

    public static int mainScreenTextOption = 0;
    [SerializeField] private Text successText;
    [SerializeField] private Text timeFailText;
    [SerializeField] private Text fuelFailText;

    public void BeginGame()
    {
         SceneManager.LoadScene("DoomsdayDelivery_Game");
    }

    public void ToMainMenu()
    {
        if (mainScreenTextOption == 1)
        {
            successText.gameObject.SetActive(true);

            timeFailText.gameObject.SetActive(false);
            fuelFailText.gameObject.SetActive(false);
        }
        else if (mainScreenTextOption == 2)
        {
            timeFailText.gameObject.SetActive(true);

            successText.gameObject.SetActive(false);
            fuelFailText.gameObject.SetActive(false);
        }
        else if (mainScreenTextOption == 3)
        {
            fuelFailText.gameObject.SetActive(true);

            successText.gameObject.SetActive(false);
            timeFailText.gameObject.SetActive(false);
        }
        
    }

}
