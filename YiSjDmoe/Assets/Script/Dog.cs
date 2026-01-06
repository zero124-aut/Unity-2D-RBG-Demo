using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Animator at;
    public GameObject StarEffect;
    public AudioClip petSound;
    // Start is called before the first frame update
    void Start()
    {
        at = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BeHapp()
    {
        Debug.Log(111);
        at.CrossFade("DogComforted", 0);
        GameManager.Instanace.hasPetTheDog = true;
        GameManager.Instanace.SetContentIndex();
        Destroy(StarEffect);
        GameManager.Instanace.PlaySound(petSound);

        Invoke("CanControlLuna",1.7f);
    }
    public void CanControlLuna()
    {
        GameManager.Instanace.canControlLuna = true;
    }
}
