using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using DG.Tweening;
using Pooling;
using Asteroids;
using UnityEngine.Events;

public class OpeningCutscene : MonoBehaviour {
    [Header("Opening Scene")]
    [Header("Player Variables")]
    [SerializeField] private PlayerShip player;
    [SerializeField] private float goToDestinationTime;
    [SerializeField] Ease playerEasing;

    [Header("Camera Variables")]
    [SerializeField] private float cameraDestinationTime;

    [Header("AsteroidVariables")]
    [SerializeField] private float asteroidDestinationTime;
    [SerializeField] private Ease asteroidEasing;

    [Header("Events")]
    [SerializeField] private UnityEvent onFinishCutscene;

    private Vector3 _playerStartingDestination;
    private Vector3 _asteroidDestination;
    private Camera _camera;


    public void OpeningScene() {
        player.transform.DOMove(_playerStartingDestination, goToDestinationTime).SetEase(playerEasing).OnComplete(() =>
        DOTween.To(() => _camera.orthographicSize, x => _camera.orthographicSize = x, 15, cameraDestinationTime).OnComplete(() => {
            var a = PoolManager.instance.GetPoolObject(PoolTypes.Asteroids);
            a.gameObject.SetActive(true);
            a.GetComponent<Asteroid>().DontMove();
            a.transform.position = ZeroZPos(_camera.ViewportToWorldPoint(new Vector2(1, 0.5f))) ;
            _asteroidDestination = ZeroZPos(_camera.ViewportToWorldPoint(new Vector2(0.85f, 0.5f)));
            a.transform.DOMove(_asteroidDestination, goToDestinationTime).SetEase(asteroidEasing).OnComplete(() => {
                onFinishCutscene?.Invoke();
            });
        }));
    }

    private void Start()
    {
        _camera = Camera.main;
        player.transform.position = ZeroZPos(_camera.ViewportToWorldPoint(new Vector2(0, 0.5f)));
        _playerStartingDestination = ZeroZPos(_camera.ViewportToWorldPoint(new Vector2((0.3f), 0.5f)));
        OpeningScene();
    }

    private Vector3 ZeroZPos(Vector2 vector2) {
        return  new Vector3(vector2.x, vector2.y, 0);
    }
}
