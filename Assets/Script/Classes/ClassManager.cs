using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public ClasseBase ninja = new Ninja(90, 40, 20);
    public ClasseBase berserker = new Berserker(200, 25, 20);
    public ClasseBase mago = new Mago(100, 35, 20);
  

    public GameObject ninjaPrefab;
    public GameObject berserkerPrefab;
    public GameObject magoPrefab;

    private void Start()
    {
        ninja.prefab = ninjaPrefab;
        mago.prefab = magoPrefab;
        berserker.prefab = berserkerPrefab;
    }
    public void CriarClasse(ClasseBase classe)
    {
        PlayerScript.singleton.classe = classe;
        
    }
}
