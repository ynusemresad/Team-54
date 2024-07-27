using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{

    /*  Notlar
     *  Etkileþime girilecek her nesnenin içine bu scripti ekleyin
     *  Etkileþime girilecek nesnenin içine ayrýca bir collider ekleyin ve setTrigger'i açýn. Bu kod, o collider'in alanýna girince çalýþacak yani büyüklüðünü ona göre ayarlayýn
     *  Oyuncu prefabýnýn(resources klasöründe kayýtlý) tag'ini Player olarak iþaretleyin 
     */


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interaction()
    {
        Debug.Log("Etkileþime Girildi!");

        //Etkileþime girince yapýlmasý istenilen þeyleri bu fonksiyonun içine yazýn
    }

    public void OutOfInteraction()
    {
        Debug.Log("Etkileþimden Çýkýldý!");
        //Eðer etkileþimden çýkýnca olmasý gereken bir þey varsa bu fonksiyonun içine yazýn
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            Interaction();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            OutOfInteraction();

        }
    }

}
