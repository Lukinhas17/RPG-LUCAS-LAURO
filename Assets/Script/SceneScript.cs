using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static SceneScript singleton;
    public void LoadScene(string name)//recebe por parametro um nome, esse nome é atribuido no inspector 
    {
        SceneManager.LoadScene(name);//o nome deve ser o nome da cena que o botão vai passar, no caso essa é a função para a troca da cena 
    }

    public void ReloadScene()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()//sai do jogo
    {
        Application.Quit();//função para sair do jogo
    }

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

            Destroy(gameObject);
        }
    }

}
