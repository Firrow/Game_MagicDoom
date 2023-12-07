using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public AudioSource audioSource;
    public static DontDestroyOnLoad instance;


    private void Start()
    {
        if (instance == null)
            instance = new DontDestroyOnLoad();

        DontDestroyOnLoad(audioSource);
    }
}
