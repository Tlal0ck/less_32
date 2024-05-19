using UnityEngine;
using TMPro; // ��� ������ � �������

public class MenuController : MonoBehaviour
{
    // ��������� ���� ��� �������� ������
    [SerializeField]
    private string _versionName = "1"; // ����� �������� �����

    // ������� ������� -- ������ � ������
    [SerializeField]
    private GameObject _usernamePanel, _menuPanel, _startButton;

    // ���� �����
    [SerializeField]
    private TMP_InputField _usernameInput , _createGameInput, _joinGameInput;
    

    // ������������ Photon Engine
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(_versionName);
    }

    // ��� ������, ���������� ������ ������������
    private void Start()
    {
        _usernamePanel.SetActive(true);
    }

    // ����������� � �������
    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("�����������");
    }


    // ����� ����� ������������
    public void ChangeUserName()
    {
        if (_usernameInput.text.Length >= 5)
        {
            _startButton.SetActive(true);
        }
        else
        {
            _startButton.SetActive(false);
        }
    }

    // ��������� ����� ������������
    public void SetUserNameToGame()
    {
        _usernamePanel.SetActive(false);
        PhotonNetwork.playerName = _usernameInput.text;
    }

    // �������� �������
    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(_createGameInput.text, new RoomOptions() { maxPlayers = 10 }, null);
    }

    // ����������� � �������
    public void JoinGame()
    {
        PhotonNetwork.JoinOrCreateRoom(_joinGameInput.text, new RoomOptions() { maxPlayers = 10 }, TypedLobby.Default);
    }

    // ����������� � �����
    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }
}
