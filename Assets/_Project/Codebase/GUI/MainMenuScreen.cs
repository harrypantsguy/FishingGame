using UnityEngine;
using UnityEngine.UI;

namespace FishingGame.GUI
{
    public sealed class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            
        }
    }
}