using UnityEngine;

public class PregameCamera : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, (float) 0.5, 0);
    }
}
