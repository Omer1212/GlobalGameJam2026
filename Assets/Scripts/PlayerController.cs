using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float regularSpeed = 5f;

    const float slowSpeed = 1f;

    const float fastSpeed = 10f;

    float currentSpeed;

    private float maskStartTime;
    private float maskElapsedTime;

    GameObject currentMask;
    public TextMeshProUGUI scoreText;
    public AudioClip pickupSound;
    public ObjectPool pool;

    int score;

    void Start()
    {
        currentSpeed = regularSpeed;
    }

    void Update()
    {
        MovePlayer();
        CheckMask();
    }

    void CheckMask()
    {
        if(currentMask != null && (Time.time - maskStartTime) > 5)
        {
            RemoveMask();
            if(pool.GetCountOfObjects() == GameManager.NUM_OF_MASKS) //All masks are collected
            {
                for(int i = 0; i < GameManager.NUM_OF_MASKS; i++)
                {
                    FindFirstObjectByType<GameManager>().MaskBuild(transform.position);
                }
            }
        }
    }

    private void MovePlayer()
    {
        float move = 0f;
        float steer = 0f;

        if (Keyboard.current.wKey.isPressed)
        {
            move = 1f;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            move = -1f;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            steer = -1f;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            steer = 1f;
        }

        float moveX = steer * currentSpeed * Time.deltaTime;
        float moveY = move * currentSpeed * Time.deltaTime;

        transform.Translate(moveX, 0, 0);
        transform.Translate(0, moveY, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mask"))
        {
            PickMask(collision);
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
            score++;
            scoreText.text = $"SCORE: {score}";
        }
    }

    private void PickMask(Collider2D collision)
    {
        if (currentMask != null)
        {
            RemoveMask();
        }
        GameObject mask = collision.gameObject;
        maskStartTime = Time.time;
        currentMask = mask;
        mask.transform.SetParent(transform);
        switch (mask.GetComponent<MaskManager>().GetMaskType())
        {
            case MaskType.RedMask:
                mask.transform.localPosition = new Vector3(0.0900000036f, -0.130919993f, 0f);
                currentSpeed = slowSpeed;
                break;

            case MaskType.GreenMask:
                mask.transform.localPosition = new Vector3(0.0299999993f, -0.310000002f, 0f);
                currentSpeed = fastSpeed;
                break;

            case MaskType.YellowMask:
                mask.transform.localPosition = new Vector3(-0.319999993f, 0.419999987f, 0f);
                break;

            case MaskType.BlueMask:
                mask.transform.localPosition = new Vector3(0f, 0.0599999987f, 0f);
                break;
            default:
                mask.transform.localPosition = new Vector3(0, 0, 0);
                break;
        }
    }

    private void RemoveMask()
    {
        pool.ReturnObject(currentMask);
        currentMask.transform.SetParent(null, true);
        currentMask = null;
        currentSpeed = regularSpeed;
    }
}
