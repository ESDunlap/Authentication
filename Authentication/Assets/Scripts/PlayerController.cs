using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float jumpPower;
    public GameObject playButton;
    public TextMeshProUGUI curTimeText;
    private Rigidbody rig;
    private float startTime;
    private float timeTaken;
    private int collectablesPicked;
    public int maxCollectables = 10;
    private bool isPlaying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
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
        if (gameObject.transform.position.y <= 0.2)
        {
            rig.AddForce(Vector3.up * jumpPower);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
            return;
        curTimeText.text = (Time.time - startTime).ToString("F2");
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        rig.linearVelocity = new Vector3(x, rig.linearVelocity.y, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            collectablesPicked++;
            speed *= (float) 1.2;
            Destroy(other.gameObject);
            if (collectablesPicked == maxCollectables)
                End();
        }
    }

    public void Begin()
    {
        playButton.SetActive(false);
        startTime = Time.time;
        Debug.Log("Start Time" + startTime);
        isPlaying = true;
    }

    void End()
    {
        playButton.SetActive(true);
        Debug.Log("End Time" + Time.time + " + " + startTime);
        timeTaken = Time.time - startTime;
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
        isPlaying = false;
    }
}
