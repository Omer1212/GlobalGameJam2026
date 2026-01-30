using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPool pool;
    public GameObject player;

    const int NUM_OF_MASKS = 10;

    void Start()
    {
        Vector3 playerPosition = player.transform.position;

        for(int i = 0; i < NUM_OF_MASKS; i++)
        {
            GameObject obj = pool.GetObject();

            int xRandom = UnityEngine.Random.Range(0, 2) == 0
                        ? UnityEngine.Random.Range(-10, -2)
                        : UnityEngine.Random.Range(3, 11);

            int yRandom = UnityEngine.Random.Range(0, 2) == 0
                        ? UnityEngine.Random.Range(-10, -2)
                        : UnityEngine.Random.Range(3, 11);


            float moveX = playerPosition.x + xRandom;
            float moveY = playerPosition.y + yRandom;

            obj.transform.Translate(moveX, moveY, 0);
            obj.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }
    }
}
