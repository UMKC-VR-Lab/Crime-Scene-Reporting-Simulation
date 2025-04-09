using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    bool trig, open;
    public float smooth = 2.0f;//скорость вращения
    public float DoorOpenAngle = 90.0f;//угол вращения 
    private Vector3 defaulRot;
    private Vector3 openRot;
    public Text txt;
    
    void Start()
    {
        defaulRot = transform.eulerAngles;
        openRot = new Vector3(defaulRot.x, defaulRot.y + DoorOpenAngle, defaulRot.z);
    }

    void Update()
    {
        if (open)//открыть
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
        }
        else//закрыть
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaulRot, Time.deltaTime * smooth);
        }
        // Replace this with new input system to make it work again.
        // if (Input.GetKeyDown(KeyCode.E) && trig) open = !open;
        if (trig)
        {
            if (open)
            {
                txt.text = "Close E";
            }
            else
            {
                txt.text = "Open E";
            }
        }
    }
    private void OnTriggerEnter(Collider coll)//вход и выход в\из  триггера 
    {
        if (coll.tag == "Player")
        {
            if (!open)
            {
                txt.text = "Close E ";
            }
            else
            {
                txt.text = "Open E";
            }
            trig = true;
        }
    }
    private void OnTriggerExit(Collider coll)//вход и выход в\из  триггера 
    {
        if (coll.tag == "Player")
        {
            txt.text = " ";
            trig = false;
        }
    }
}
