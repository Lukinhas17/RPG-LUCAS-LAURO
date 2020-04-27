using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public int opcao;//escolha da class do player


    private void Awake()
    {
        NoDestroy();
    }

    private void NoDestroy()
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
