using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTileAnimator : MonoBehaviour
{
    public List<Sprite> sprites;
    public float anim_time = 5f;

    float timer;
    bool current_frame;
    // Start is called before the first frame update
    void Start()
    {
        timer = anim_time;
        current_frame = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                if (current_frame)
                    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                else
                    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                current_frame = !current_frame;
            }
            current_frame = !current_frame;
            timer = anim_time;
        }
    }
}
