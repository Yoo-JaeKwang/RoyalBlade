using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Util
{
    public class CMCamera : MonoBehaviour
    {
        private static CMCamera _instance;

        public CinemachineVirtualCamera MyCamera;

        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

        private void Awake()
        {
            _instance = this;
            _cinemachineBasicMultiChannelPerlin = MyCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _shakeCoroutine = ShakeCoroutine();
            _zoomInCo = ZoomInCo();
        }
        public static void SetCameraFollow(Transform transform) => _instance.MyCamera.Follow = transform;

        private static float _duration;
        private static float _intensity;
        public static void Shake(float duration = 0.5f, float intensity = 40)
        {
            _duration = duration;
            _intensity = intensity;
            _instance.StartCoroutine(_instance._shakeCoroutine);
        }
        private IEnumerator _shakeCoroutine;
        private IEnumerator ShakeCoroutine()
        {
            while (true)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensity;

                yield return WFSecs.GetWaitForSeconds(_duration);

                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

                StopCoroutine(_shakeCoroutine);

                yield return null;
            }
        }
        public static void ZoomIn()
        {
            _instance._isZoomOut = false;
            _instance.StartCoroutine(_instance._zoomInCo);
        }
        private IEnumerator _zoomInCo;
        private readonly Vector3 _camPos = new(0, 300, -10);
        private readonly Vector3 _zoomPos = new(0, 200, -10);
        private IEnumerator ZoomInCo()
        {
            while (true)
            {
                MyCamera.m_Lens.OrthographicSize -= 1;
                MyCamera.transform.position += Vector3.down;
                if (MyCamera.m_Lens.OrthographicSize <= 540)
                {
                    MyCamera.m_Lens.OrthographicSize = 540;
                    MyCamera.transform.position = _zoomPos;
                }

                if (_isZoomOut)
                {
                    StopCoroutine(_zoomInCo);
                }

                yield return null;
            }
        }
        private bool _isZoomOut = false;
        public static void ZoomOut()
        {
            _instance._isZoomOut = true;
            _instance.MyCamera.m_Lens.OrthographicSize = 640;
            _instance.MyCamera.transform.position = _instance._camPos;
        }
    }
}