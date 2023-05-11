using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class JengaTowerFocusControlls : MonoBehaviour
    {
        [SerializeField] private GameObject _jengaTowerButton;
        [SerializeField] private float _jengaButtonYOffset = 50;

        private float _currentJengaButtonYOffset;

        public int FocussedJengaTowerIndex { get; private set; }

        public void CreateJengaTowerButton(GameObject jengaTower, string buttonName,
            CinemachineVirtualCamera cinemachineVirtualCamera)
        {
            GameObject jengaTowerButton = Instantiate(_jengaTowerButton, transform);

            int buttonIndex = 99;
            if (buttonName.Contains("6"))
            {
                buttonIndex = 6;
            }
            else if (buttonName.Contains("7"))
            {
                buttonIndex = 7;
            }
            else if (buttonName.Contains("8"))
            {
                buttonIndex = 8;
            }

            jengaTowerButton.GetComponent<Button>().onClick
                .AddListener(() => FocusJengaTower(jengaTower, cinemachineVirtualCamera, buttonIndex));

            RectTransform rectTransform = jengaTowerButton.GetComponent<RectTransform>();
            var anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition = new Vector2(anchoredPosition.x,
                anchoredPosition.y - _currentJengaButtonYOffset);
            rectTransform.anchoredPosition = anchoredPosition;

            _currentJengaButtonYOffset += _jengaButtonYOffset;

            //replace the space in buttonName with a new line
            buttonName = buttonName.Replace(" ", "\n");

            jengaTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
        }

        private void FocusJengaTower(GameObject jengaTower, ICinemachineCamera cinemachineVirtualCamera,
            int buttonIndex)
        {
            FocussedJengaTowerIndex = buttonIndex;
            cinemachineVirtualCamera.LookAt = jengaTower.transform;
            cinemachineVirtualCamera.Follow = jengaTower.transform;
        }
    }
}