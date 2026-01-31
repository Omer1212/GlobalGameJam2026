using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float currentSpeed = 5f;
    [SerializeField] float steerSpeed = 5f;
    GameObject currentMask;
    public TextMeshProUGUI scoreText;
    public AudioClip pickupSound;
    public ObjectPool pool;

    int score;

    void Update()
    {
        MovePlayer();
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

        float moveAmount = move * currentSpeed * Time.deltaTime;
        float steerAmount = steer * steerSpeed * Time.deltaTime;

        transform.Translate(0, moveAmount, 0);
        transform.Translate(steerAmount, 0, 0);
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
            pool.ReturnObject(currentMask);
            currentMask.transform.SetParent(null, true);
        }
        GameObject mask = collision.gameObject;
        currentMask = mask;
        mask.transform.SetParent(transform);
        switch (mask.GetComponent<MaskManager>().GetMaskType())
        {
            case MaskType.RedMask:
                mask.transform.localPosition = new Vector3(0.0651580021f, -0.0199999996f, 0f);
                break;

            case MaskType.GreenMask:
                mask.transform.localPosition = new Vector3(0.0299999993f, -0.310000002f, 0f);
                break;

            case MaskType.YellowMask:
                mask.transform.localPosition = new Vector3(-0.319999993f, 0.419999987f, 0f);
                break;

            default:
                mask.transform.localPosition = new Vector3(0, 0, 0);
                break;
        }
    }
}
