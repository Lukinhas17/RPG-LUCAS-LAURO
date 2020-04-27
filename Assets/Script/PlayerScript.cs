
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static string nomePlayer = null;
    public int acurice;
    public static PlayerScript singleton {get; private set;}
    public ClasseBase classe {get; set;}

    private void Awake()
    {
        NoDestroy();
    }

    void NoDestroy()
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
            Debug.LogWarning("Já existe uma instância dessa classe.");

            Destroy(gameObject);
        }
    }
}
