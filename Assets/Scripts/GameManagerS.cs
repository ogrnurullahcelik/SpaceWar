using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bu script Nurullah Çelik tarafından 02.08.2020 tarihinde açılmıştır.
/// Amacı SpaceWar oyununun kontrollerini sağlayabilmektir.
/// </summary>
/// 

public class GameManagerS : MonoBehaviour
{
    [Tooltip("Extra Power aktiflik değişkenleri")]
    public bool tripleFire, twinFire, fasterFire, fastSpeedFire, copyPlayer;

    [Space(10)]
    [Tooltip("Extra Power Butonları")]
    public Button tripleFireB, twinFireB, fasterFireB, fastSpeedFireB, copyPlayerB;

    [HideInInspector]
    [Tooltip("Ateş hızı değişkeni")]
    public float fireSpeed = 50;


    [Tooltip("Oyuncunun kontrol ettiği orjinal Player'ın Controller scripti")]
    public PlayerController pController;

    [Tooltip("Rock objelerini yöneten script")]
    public RockManager rManager;


    [Tooltip("Ekranda hazır bekleyen kopya oyuncu")]
    public GameObject copyPlayerPrefab;


    
    [Tooltip("Kullanılmış güç sayısı")]
    int usedPowerVal = 0;


    [Tooltip("Oyunun scorudur.")]
    int gameScore = 0;


    public Text GameScore;

    public bool GamePlaying = false;
   

    // Update is called once per frame
    void Update()
    {
        #region Numpad üzerinden aktifleştirme
        if (usedPowerVal < 3)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                //Triple aktif
                SetTripleFire();
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                //twin aktif
                SetTwinFire();
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                //fasterfire aktif
                SetFasterFire();
            }

            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                //fasterspeed aktif
                SetFastSpeedFire();
            }

            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                //copyplayer aktif
                EnableCopyPlayer();

            }
        }
        #endregion

    }



    /// <summary>
    /// Üçlü ateş moduna geçirme
    /// </summary>
    public void SetTripleFire()
    {
            //Triple aktif
            tripleFire = true;
            usedPowerVal++;
        if (usedPowerVal >= 3)
            ControlExtraPower();
    }


    /// <summary>
    /// İkiz ateş moduna geçirme
    /// </summary>
    public void SetTwinFire()
    {
        
            //twin aktif
            twinFire = true;
            usedPowerVal++;
        if (usedPowerVal >= 3)
            ControlExtraPower();
    }


    /// <summary>
    /// Daha hızlı ateş moduna geçirme
    /// </summary>
    public void SetFasterFire()
    {
        
            //fasterfire aktif
            fasterFire = true;
            usedPowerVal++;
        if (usedPowerVal >= 3)
            ControlExtraPower();

    }


    /// <summary>
    /// Ateşlerin daha hızlı gitmesini sağlama
    /// </summary>
    public void SetFastSpeedFire()
    {
        
            //fasterspeed aktif
            fastSpeedFire = true;
            fireSpeed = fireSpeed*2;
            usedPowerVal++;
        if (usedPowerVal >= 3)
            ControlExtraPower();
    }


    /// <summary>
    /// Kopya oyuncuyu aktifleştirme
    /// </summary>
    public void EnableCopyPlayer()
    {
       
        
            copyPlayer = true;
            copyPlayerPrefab.gameObject.SetActive(true);
            copyPlayerPrefab.GetComponent<PlayerController>().StartGame();
            pController.ModeClone();
            usedPowerVal++;
        
        if (usedPowerVal >= 3)
            ControlExtraPower();
    }


    /// <summary>
    /// Bütün power butonları de aktif olur
    /// </summary>
    void ControlExtraPower()
    {
        tripleFireB.enabled = twinFireB.enabled = fasterFireB.enabled = fastSpeedFireB.enabled = copyPlayerB.enabled = false;
    }


    

    /// <summary>
    /// Oyunun scorunu gelen veri ile günceller
    /// </summary>
    /// <param name="val"></param>
    public void UpdateGameScore(int val)
    {
        gameScore += val;

        GameScore.text = "Score = " + gameScore;
    }

    /// <summary>
    /// Oyunu başlatır.
    /// </summary>
    public void StartGame()
    {
        GamePlaying = true;        
        pController.StartGame();
        rManager.StartGame();

    }

    /// <summary>
    /// Ana menüye dönüş yaptırır
    /// </summary>
    public void GoBackMainMenu()
    {
       
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

       
    }
}
