using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Manager_DD : MonoBehaviour
{
    //use this script to switch between Menu and Game scene.

    public static int mainScreenTextOption = 0;
    [SerializeField] private TextMeshProUGUI successText;
    [SerializeField] private TextMeshProUGUI timeFailText;
    [SerializeField] private TextMeshProUGUI fuelFailText;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "DoomsdayDelivery_Menu")
        {
            if (mainScreenTextOption == 0)
            {
                successText.gameObject.SetActive(false);
                timeFailText.gameObject.SetActive(false);
                fuelFailText.gameObject.SetActive(false);
            }
            else if (mainScreenTextOption == 1)
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

    public void BeginGame()
    {
        StartCoroutine(BeginGameCoroutine());
    }

    IEnumerator BeginGameCoroutine()
    {
        yield return new WaitForSeconds(3);
        string menuScene = "DoomsdayDelivery_Game";
        SceneManager.LoadScene(menuScene);
    }

}
