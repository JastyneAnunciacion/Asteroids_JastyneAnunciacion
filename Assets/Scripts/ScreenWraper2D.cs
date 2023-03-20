using UnityEngine;

public class ScreenWraper2D : MonoBehaviour {
    [SerializeField] private bool wrapWidth = true;
    [SerializeField] private bool wrapHeight = true;
    [SerializeField] private Renderer graphic;
    private Camera _camera;

    private void Start() {
        _camera = Camera.main;
    }

    private void LateUpdate() {
        Wrap();
    }

    private void Wrap() {
        var newPosition = transform.position;
        var viewportPosition = _camera.WorldToViewportPoint(newPosition);
        if (wrapWidth && IsVisbleByCamera()) {
            if (viewportPosition.x > 1) {
                newPosition.x = _camera.ViewportToWorldPoint(Vector2.zero).x;
            }
            else if (viewportPosition.x < 0) {
                newPosition.x = _camera.ViewportToWorldPoint(Vector2.one).x;
            }
        }
        if (wrapHeight && IsVisbleByCamera()) {
            if (viewportPosition.y > 1) {
                newPosition.y = _camera.ViewportToWorldPoint(Vector2.zero).y;
            }
            else if (viewportPosition.y < 0) {
                newPosition.y = _camera.ViewportToWorldPoint(Vector2.one).y;
            }
        }
        transform.position = newPosition;
    }

    private bool IsVisbleByCamera() {
        if (graphic.isVisible)
            return true;
        return false;
    }
}