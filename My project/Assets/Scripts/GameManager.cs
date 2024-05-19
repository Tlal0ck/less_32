using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Поля для префаба игрока и камеры сцены
    [SerializeField]
    private GameObject _playerPrefab, _sceneCamera;

    private void Start()
    {
        // Создаем игрока на сцене
        PhotonNetwork.Instantiate(_playerPrefab.name, transform.position, Quaternion.identity, 0);
        // Отключаем основную камеру
        _sceneCamera.SetActive(false);
    }


}
