using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // ѕеременные под чувствительность по ос€м. Ќе целые
    public float sensX;
    public float sensY;
    // ѕеременна€ под направление, хранит в себе полный набор координат
    public Transform orientation;
    // ѕеременные текущего поворота
    float xRotation;
    float yRotation;

    private void Start()
    {
        // Ѕлокируем  урсор и делаем его невидимым
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // записываем координаты мышки.
        // Ќе забываем про независимость от кадров и умножение на чувствительность   
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        // »змен€ем повороты осей и ограничиваем значение от -90 до 90
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // ”станавливаем повороты при помощи создани€ направлени€ при помощи Quaternion
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
