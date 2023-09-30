using UnityEngine;

public class GameInit : MonoBehaviour
{
     [SerializeField] private TransitionScreen _transitionScreen;
     [SerializeField] private MusicPlayer _musicPlayer;
     
     private void Awake()
     {
         _transitionScreen.FadeOut(1f);
         _musicPlayer.PlayGame();
     }
}