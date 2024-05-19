using UnityEngine;

public class PlayerController : Photon.MonoBehaviour
{
    [SerializeField]
    private PhotonView _photonView;

    [SerializeField]
    private GameObject _playerCamera;

    [SerializeField]
    private TMPro.TMP_Text _userNameText;

    // Различные переменные скоростей
    [SerializeField]
    private float _moveSpeed, _groundDrag, _jumpForce, _jumpCooldown, _airMultiplier;
    
    private bool _readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    // Высота и определение земли
    [SerializeField]
    private float _playerHeight;
    [SerializeField]
    private LayerMask _whatIsGround;
    private bool _grounded;
    // Направление, которое хранит все возможные координаты объекта
    [SerializeField]
    private Transform orientation;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDirection;

    private Rigidbody _rb;

    private void Awake()
    {
        if(photonView.isMine)
        {
            _playerCamera.SetActive(true);
            _userNameText.text = PhotonNetwork.playerName;
        }
        else 
        {
            _userNameText.text = photonView.owner.name;
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        _readyToJump = true;
    }

    private void Update()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);

        MyInput();
        SpeedControl();
        if (_grounded)
            _rb.drag = _groundDrag;
        else
            _rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        // записываем текущее состояние клавиш
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        // в каких случаях прыгаем?
        if (Input.GetKey(jumpKey) && _readyToJump && _grounded)
        {
            _readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), _jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // рассчитаем направление
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        // на земле
        if (_grounded)
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);

        // в воздухе
        else if (!_grounded)
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f * _airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        // ограничиваем скорость, если вдруг разогнались
        if (flatVel.magnitude > _moveSpeed)
        {
            // Нормализуй вектор, прежде чем умножить - Конфуций
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }

    }

    private void Jump()
    {
        // обнуляем по высоте скорость
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        _readyToJump = true;
    }
}
