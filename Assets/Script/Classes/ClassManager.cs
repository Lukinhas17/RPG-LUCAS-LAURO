using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public ClasseBase ninja = new Ninja(93, 33, 25);
    public ClasseBase berserker = new Berserker(200, 23, 40);
    public ClasseBase mago = new Mago(120, 26, 30);
  

    public GameObject ninjaPrefab;
    public GameObject berserkerPrefab;
    public GameObject magoPrefab;

    private void Start()
    {
        ninja.prefab = ninjaPrefab;
        mago.prefab = magoPrefab;
        berserker.prefab = berserkerPrefab;
    }
	//VAMOS COMENTAR ESSE CÓDIGO!!!
    public void CriarClasse(ClasseBase classe)
    {
        PlayerScript.singleton.classe = classe;        
    }
}
