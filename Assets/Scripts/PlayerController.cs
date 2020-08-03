using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Bu Script Nurullah Çelik tarafından 02.08.2020 tarafında açılmıştır. Amaç Oyuncunun kontrollerini sağlayabilmektir.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Player Değişkenleri")]
    /// <summary>
    ///Player objesinin rigidbody componentini kontrol eder.
    /// </summary>


    Rigidbody playerRigid;

    #region Oyuncu değişkenleri
    public bool PlayerorClone = false;
    public float speed = 10;
    public Vector2 xClamp = Vector2.zero;
    public Vector2 zClamp = Vector2.zero;
    public float tilt = 10;
    public Vector2 xClampCl = Vector2.zero;
    public Vector2 zClampCl = Vector2.zero;

    #endregion
    #region Fire Değişkenleri
    [Header("Fire değişkenleri")]
    public float firetime = 2 ;
    public bool firing = true;
    public GameObject fireParent;
    public GameObject firePrefab;
    public GameObject firePoint;
    #endregion


    [Header("Game Manager")]
    public GameManagerS gm;




    /// <summary>
    /// Player için oyunu başlatır.
    /// </summary>
    public void StartGame()
    {
        if (this.tag.Equals("Player"))
        {
            //Bu gerçek oyuncudur.
            playerRigid = this.GetComponent<Rigidbody>();
        }
        else
        {
            //Bu klon oyuncudur.
        }
        StartCoroutine(firecontroller());
    }

    // Update is called once per frame
    void Update()
    {
        #region Player Movements

        //Oyun başladıysa haraketi sağlar
        if (gm.GamePlaying)
        {
            if (PlayerorClone)
            {
                /*Standart iler geri haraket ettirme fonksiyonu*/
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                playerRigid.velocity = movement * speed;


                playerRigid.position = new Vector3
                (
                    Mathf.Clamp(GetComponent<Rigidbody>().position.x, xClamp.x, xClamp.y),
                    0.0f,
                    Mathf.Clamp(GetComponent<Rigidbody>().position.z, zClamp.x, zClamp.y)
                );

                playerRigid.rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
            }
            else
            {
                /*Standart iler geri haraket ettirme fonksiyonu*/
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                playerRigid.velocity = movement * speed;


                playerRigid.position = new Vector3
                (
                    Mathf.Clamp(GetComponent<Rigidbody>().position.x, xClampCl.x, xClamp.y),
                    0.0f,
                    Mathf.Clamp(GetComponent<Rigidbody>().position.z, zClampCl.x, zClamp.y)
                );

                playerRigid.rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
            }
        }
        #endregion
    }

    /// <summary>
    /// Ateş yönetimini yapar
    /// </summary>
    /// <returns></returns>
    IEnumerator firecontroller()
    {

        while (firing && gm.GamePlaying)
        {
            yield return new WaitForSecondsRealtime(firetime);


            
            Instantiate(firePrefab, firePoint.transform.position, Quaternion.Euler(90,0,0), fireParent.transform) ;
            if(gm.tripleFire == true)
            {
                //Instantiate(firePrefab, firePoint.transform.position, Quaternion.Euler(90, 0, 0), fireParent.transform);
                Instantiate(firePrefab, firePoint.transform.position, Quaternion.Euler(90, 0, 45.0f), fireParent.transform);
                Instantiate(firePrefab, firePoint.transform.position, Quaternion.Euler(90, 0, -45.0f), fireParent.transform);
            }
            if(gm.twinFire == true)
            {
                yield return new WaitForSecondsRealtime(firetime/2);
                FireTwin();
            }
            if(gm.fasterFire == true)
            {
                firetime = 1;
            }
            

        }
    }


    /// <summary>
    /// Klon objeyi aktifleştirir
    /// </summary>
    public void ModeClone()
    {
        xClamp.y = -5;

    }

    /// <summary>
    /// İkiz ateşi sağlar
    /// </summary>
    void FireTwin()
    {
        Instantiate(firePrefab, firePoint.transform.position, Quaternion.Euler(90, 0, 0), fireParent.transform);
        if (gm.tripleFire == true)
        {
            //Instantiate(firePrefab, firePoint.transform.position, Quaternion.Euler(90, 0, 0), fireParent.transform);
            Instantiate(firePrefab, firePoint.transform.position, Quaternion.Euler(90, 0, 45.0f), fireParent.transform);
            Instantiate(firePrefab, firePoint.transform.position, Quaternion.Euler(90, 0, -45.0f), fireParent.transform);
        }
    }

    /// <summary>
    /// Uygulamadan çıkınca bütün işlemler durdurulsun sağlar
    /// </summary>
    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}
