using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    float timer = 0;
    float explosionTime = 1;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameObject.transform.localScale = new Vector3(timer, timer, timer);
        if (timer >= explosionTime)
		{
            gameObject.SetActive(false);
		}
    }

    public void Spawn(Vector3 position)
	{
        gameObject.transform.position = position;
        timer = 0;
	}
}
