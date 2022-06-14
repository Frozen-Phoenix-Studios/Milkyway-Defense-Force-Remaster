using UnityEngine;

namespace FrozenPhoenixStudios.Components
{
    public class SafeAreaVerticalResizer : MonoBehaviour
    {
        private RectTransform _rectTransform;
        
        private Rect _safeArea;
        public Rect safeArea => _safeArea;

        private Vector2 _minAnchor;
        private Vector2 _maxAnchor;

        public Vector2 minAnchor => _minAnchor;
        public Vector2 maxAnchor => _maxAnchor;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _safeArea = Screen.safeArea;
            GetAnchors();
            SetAnchors();
        }

        private void GetAnchors()
        {
            _minAnchor = _safeArea.position;
            _maxAnchor = _minAnchor + _safeArea.size;

            _minAnchor.x /= Screen.width;
            _maxAnchor.x /= Screen.width;

            _minAnchor.y /= Screen.height;
            _maxAnchor.y /= Screen.height;
        }

        private void SetAnchors()
        {
            Debug.Log($"Min anchor is {minAnchor} \n" +
                      $"Max anchor is {maxAnchor}");
            
            _rectTransform.anchorMin = minAnchor;
            _rectTransform.anchorMax = maxAnchor;
        }

        public Vector3 GetSafeAreaAsWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(_safeArea.width,
                _safeArea.height, Camera.main.transform.position.z));
        }
        
    }

}

