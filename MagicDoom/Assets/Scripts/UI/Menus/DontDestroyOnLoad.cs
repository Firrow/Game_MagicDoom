using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSound;
    public static DontDestroyOnLoad instance;


    private void Start()
    {
        if (instance == null)
            instance = new DontDestroyOnLoad();

        DontDestroyOnLoad(audioSourceMusic);
        DontDestroyOnLoad(audioSourceSound);
    }
}
