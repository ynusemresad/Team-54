using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{

    /*  Notlar
     *  Etkile�ime girilecek her nesnenin i�ine bu scripti ekleyin
     *  Etkile�ime girilecek nesnenin i�ine ayr�ca bir collider ekleyin ve setTrigger'i a��n. Bu kod, o collider'in alan�na girince �al��acak yani b�y�kl���n� ona g�re ayarlay�n
     *  Oyuncu prefab�n�n(resources klas�r�nde kay�tl�) tag'ini Player olarak i�aretleyin 
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
        Debug.Log("Etkile�ime Girildi!");

        //Etkile�ime girince yap�lmas� istenilen �eyleri bu fonksiyonun i�ine yaz�n
    }

    public void OutOfInteraction()
    {
        Debug.Log("Etkile�imden ��k�ld�!");
        //E�er etkile�imden ��k�nca olmas� gereken bir �ey varsa bu fonksiyonun i�ine yaz�n
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
