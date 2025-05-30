using UnityEngine;

[CreateAssetMenu(fileName = "GameAssets", menuName = "Scriptable Objects/GameAssets")]
public class GameAssetsSO : ScriptableObject
{
    [SerializeField] private Sprite ground;
    [SerializeField] private Sprite pipeBody;
    [SerializeField] private Sprite pipeHead;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip flapSound;
    [SerializeField] private AudioClip hitSound;
    

    
    public Sprite PipeHead => pipeHead;
    public Sprite PipeBody => pipeBody;
    public Sprite Ground => ground;

    
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
