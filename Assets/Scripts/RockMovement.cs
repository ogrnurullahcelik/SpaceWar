using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement : MonoBehaviour
{
    Rigidbody rockRigid;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {

        rockRigid = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        rockRigid.velocity = this.transform.forward * speed *(-1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerS>().UpdateGameScore(-3);
            Destroy(this.gameObject);
        }
    }

}
