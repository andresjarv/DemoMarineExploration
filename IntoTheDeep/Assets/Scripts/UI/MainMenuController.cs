using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {   
        //cargamos directamente el Nivel 1(indice1)
        SceneFader.Instance.FadeToScene(1);
    }
}
