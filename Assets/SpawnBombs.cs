using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBombs : MonoBehaviour
{
    public GameObject cameraRig;
    public GameObject[] balls;    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBombsOverTime());
    }

    IEnumerator SpawnBombsOverTime()
    {
        while (true)
        {
            GameObject ball = Instantiate(balls[Random.Range(0, balls.Length)]);
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(1.5f, 1.8f);
            ball.transform.position = cameraRig.transform.position + new Vector3( radius * Mathf.Sin(angle), 
                Random.Range(1.25f, 1.75f), 
                radius * Mathf.Cos(angle) );
            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
