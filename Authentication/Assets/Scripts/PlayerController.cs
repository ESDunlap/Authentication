using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
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
            Destroy(other.gameObject);
            if (collectablesPicked == maxCollectables)
                End();
        }
    }

    public void Begin()
    {
        playButton.SetActive(false);
        startTime = Time.time;
        isPlaying = true;
    }

    void End()
    {
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
        playButton.SetActive(true);
        timeTaken = Time.time - startTime;
        isPlaying = false;
    }
}
