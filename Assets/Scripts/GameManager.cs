using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPool pool;

    const int NUM_OF_MASKS = 10;

    void Start()
    {
        for(int i = 0; i < NUM_OF_MASKS; i++)
        {
            pool.GetObject().transform.Translate(Random.Range(-5,6), Random.Range(-5,6), 0);
        }
    }
}
