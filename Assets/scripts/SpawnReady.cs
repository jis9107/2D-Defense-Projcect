using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnReady : MonoBehaviour
{
    Image _image;
    GameControll _game;
    // Start is called before the first frame update
    void Start()
    {
        _game = FindObjectOfType<GameControll>();
        _image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_image.fillAmount < 1)
        {
            _image.fillAmount += 0.5f * Time.deltaTime;
            if(_image.fillAmount == 1)
            {
                _game.spawnReady = true;
            }
        }
    }
}
