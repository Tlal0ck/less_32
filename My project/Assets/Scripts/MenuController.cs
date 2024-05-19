using UnityEngine;
using TMPro; // Для работы с текстом

public class MenuController : MonoBehaviour
{
    // Строковое поле для хранения версии
    [SerializeField]
    private string _versionName = "1"; // Можно написать любую

    // Ишровые объекты -- панели и кнопка
    [SerializeField]
    private GameObject _usernamePanel, _menuPanel, _startButton;

    // Поля ввода
    [SerializeField]
    private TMP_InputField _usernameInput , _createGameInput, _joinGameInput;
    

    // Конфигурация Photon Engine
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(_versionName);
    }

    // При старте, активируем панель пользователя
    private void Start()
    {
        _usernamePanel.SetActive(true);
    }

    // Подключение к серверу
    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Подключение");
    }


    // Смена имени пользователя
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

    // Сохраение имени пользователя
    public void SetUserNameToGame()
    {
        _usernamePanel.SetActive(false);
        PhotonNetwork.playerName = _usernameInput.text;
    }

    // Создание комнаты
    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(_createGameInput.text, new RoomOptions() { maxPlayers = 10 }, null);
    }

    // Подключение к комнате
    public void JoinGame()
    {
        PhotonNetwork.JoinOrCreateRoom(_joinGameInput.text, new RoomOptions() { maxPlayers = 10 }, TypedLobby.Default);
    }

    // Подключение к лобби
    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }
}
