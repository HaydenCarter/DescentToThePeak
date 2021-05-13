using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RegenStamina()
    {
        Universe.Instance.Stamina = Universe.Instance.MaxStamina;
    }

}
