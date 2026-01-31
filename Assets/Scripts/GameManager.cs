using UnityEngine;
using UnityEngine.InputSystem;

public enum MaskType {GreenMask = 0, RedMask = 1, BlueMask = 2, YellowMask = 3}

public class GameManager : MonoBehaviour
{
    [SerializeField] Sprite[] maskSprites;
    public ObjectPool pool;
    public GameObject player;

    const int NUM_OF_MASKS = 10;

    void Start()
    {
        Vector3 playerPosition = player.transform.position;

        for(int i = 0; i < NUM_OF_MASKS; i++)
        {
            MaskBuild(playerPosition);
        }
    }

    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed)
        {
            Debug.Log("Quitting the game.");
            Application.Quit();
        }
    }

    private void MaskBuild(Vector3 playerPosition)
    {
        GameObject mask = pool.GetObject();

        int xRandom = Random.Range(0, 2) == 0
                    ? Random.Range(-10, -2)
                    : Random.Range(3, 11);

        int yRandom = Random.Range(0, 2) == 0
                    ? Random.Range(-10, -2)
                    : Random.Range(3, 11);


        float moveX = playerPosition.x + xRandom;
        float moveY = playerPosition.y + yRandom;

        mask.transform.Translate(moveX, moveY, 0);

        MaskType maskType = (MaskType)Random.Range(0, 4);
        mask.GetComponent<MaskManager>().SetMaskType(maskType);
        SpriteRenderer maskSpriteRenderer = mask.GetComponent<SpriteRenderer>();
        maskSpriteRenderer.sprite = maskSprites[(int)maskType];
        if (maskType == MaskType.RedMask)
        {
            mask.transform.localScale = new Vector3(0.696743786f, 0.696743786f, 0.696743786f);
        }
        if (maskType == MaskType.GreenMask)
        {
            mask.transform.localScale = new Vector3(0.610097587f, 0.610097587f, 0.610097587f);
        }
    }
}
