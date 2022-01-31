using UnityEngine;
using UnityEngine.UI;

namespace ATT
{
    public class ATTSampleController : MonoBehaviour
    {
        [SerializeField] private Button _getStatusButton = null;
        [SerializeField] private Button _requestStatusButton = null;
        [SerializeField] private Button _launchButton = null;
        [SerializeField] private Text _resultText = null;


        // Start is called before the first frame update
        void Start()
        {
            _getStatusButton.onClick.AddListener(GetStatus);
            _requestStatusButton.onClick.AddListener(Request);
            _launchButton.onClick.AddListener(Launch);
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void GetStatus()
        {
            var ret = ATTService.GetTrackingAuthorizationStatus();
            _resultText.text = $"{(ret == null ? "null" : ret.ToString())}";
        }

        private void Request()
        {
            ATTService.RequestTrackingAuthorization(ret => _resultText.text = $"{ret}");
        }

        private void Launch()
        {
            void OnComplete(bool result)
            {
                if (result)
                {
                    Debug.Log("ATT permitted");
                }
                else
                {
                    Debug.Log("ATT not permitted");
                }
                _resultText.text = $"{result}";
            }
            var ret = ATTService.GetTrackingAuthorizationStatus();
            if (!ret.HasValue)
            {
                Debug.Log("ATT not selected");
                ATTService.RequestTrackingAuthorization(OnComplete);
            }
            else
            {
                Debug.Log("ATT selected");
                OnComplete((bool)ret);
            }
        }
    }
}