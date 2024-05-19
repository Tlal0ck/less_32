using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���� ��� ������� ������ � ������ �����
    [SerializeField]
    private GameObject _playerPrefab, _sceneCamera;

    private void Start()
    {
        // ������� ������ �� �����
        PhotonNetwork.Instantiate(_playerPrefab.name, transform.position, Quaternion.identity, 0);
        // ��������� �������� ������
        _sceneCamera.SetActive(false);
    }


}
