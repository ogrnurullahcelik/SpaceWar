using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Bu script Nurullah Çelik Tarafından 02.08.2020 Tarihinde fire objesinin kontrol işlemleri için kullanılır.
/// </summary>
public class FireController : MonoBehaviour
{

    Rigidbody fireRigid;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {

        //Hızını GameManagerdan alır
        fireRigid = this.GetComponent<Rigidbody>();
        speed = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerS>().fireSpeed;
        Destroy(this.gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {

        //İlerletir
        fireRigid.velocity = this.transform.up * speed;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Rock"))
        {
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerS>().UpdateGameScore(5);
        }
    }
}
