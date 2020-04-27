using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    public InputField nomeInput;//recebe o nome digitado pelo player
    private bool escolher = false;//escolha da class do player
    public ClassManager classeManager;

    public void GetName() //pega o nome digitado 
    {
        //PlayerScript.nomePlayer = nomeInput.text;//o nome digitado pelo nomeinput é convertido em text e depois em string
        //nomeString recebe o nome convertido
    }

    public void BottonClass(int op)
    {
        if (op == 1)
        {
            classeManager.CriarClasse(classeManager.ninja);
        }
        else if (op == 2)
        {
            classeManager.CriarClasse(classeManager.mago);
        }
        else
        {
            classeManager.CriarClasse(classeManager.berserker);
        }

        escolher = true;
    }

    public void BottonContinuar(string scene)
    {
        if (nomeInput.textComponent.text.Length != 0 && escolher == true)
        {
            PlayerScript.nomePlayer = nomeInput.textComponent.text;
            SceneScript.SceneSingleton.LoadScene(scene);
        }
        else
        {
            return;
        }
    }


}