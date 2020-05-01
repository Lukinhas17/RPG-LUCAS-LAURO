
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public string nomePlayer; 
    public int tentativas = 3; //tentativas do jogador


    public static PlayerScript singleton {get; private set;} //criando singleton
    public ClasseBase classe {get; set; } //classe base do player, que vai poder ser um Mago,Ninja ou Berseker.

    private void Awake()
    {
        NoDestroy();
    }

    void NoDestroy()//criando singleton
    {
        //Faz com que o game object que possui esta classe não seja destruído ao trocar de cena
        DontDestroyOnLoad(gameObject); 

        if (singleton == null && singleton != this)
        {
            singleton = this;

            //Faz com que o game object que possui esta classe não seja destruído ao trocar de cena
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }
}
