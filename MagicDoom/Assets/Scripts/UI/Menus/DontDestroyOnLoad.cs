using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSound;
    public static DontDestroyOnLoad instance;


    // Allow to keep object during all the game
    private void Start()
    {
        if (instance == null)
            instance = new DontDestroyOnLoad();

        DontDestroyOnLoad(audioSourceMusic);
        DontDestroyOnLoad(audioSourceSound);
    }
}
