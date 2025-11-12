using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float jumpPower;
    public Camera gameCamera;
    public Camera preGameCamera;
    public GameObject playButton;
    public TextMeshProUGUI curTimeText;
    public TextMeshProUGUI CollectablesLeft;
    private Vector3 startLocation;
    private Rigidbody rig;
    private RaycastHit hit;
    private float startTime;
    private float timeTaken;
    private int collectablesPicked;
    public int maxCollectables = 10;
    private bool isPlaying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        startLocation= gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        if (!isPlaying)
            return;
        if (Input.GetKey(KeyCode.Q))
            gameObject.transform.Rotate(0, rotateSpeed, 0);
        if (Input.GetKey(KeyCode.E))
            gameObject.transform.Rotate(0, -rotateSpeed, 0);
        if (Input.GetKey(KeyCode.Space))
            Jump();
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 0.7f))
            rig.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
            return;
        if (gameObject.transform.position.y < -50)
            gameObject.transform.position = startLocation;
        curTimeText.text = (Time.time - startTime).ToString("F2");
        CollectablesLeft.text = (collectablesPicked).ToString() + " / " + (maxCollectables).ToString();
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        Vector3 dir = (transform.forward * z + transform.right * x);
        dir.y = rig.linearVelocity.y;
        rig.linearVelocity = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            collectablesPicked++;
            Destroy(other.gameObject);
            if (collectablesPicked == maxCollectables)
                End();
        }
    }

    public void Begin()
    {
        playButton.SetActive(false);
        CollectablesLeft.gameObject.SetActive(true);
        preGameCamera.gameObject.SetActive(false);
        gameCamera.gameObject.SetActive(true);
        startTime = Time.time;
        Debug.Log("Start Time" + startTime);
        isPlaying = true;
    }

    void End()
    {
        CollectablesLeft.gameObject.SetActive(false);
        playButton.SetActive(true);
        Debug.Log("End Time" + Time.time + " + " + startTime);
        timeTaken = Time.time - startTime;
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
        isPlaying = false;
    }
}
