using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyBulletLogic : MonoBehaviour
{
    [SerializeField]
    private int _speed;
    [SerializeField]
    private AudioSource _enemyBulletSound;

    // Start is called before the first frame update
    void Start()
    {
        PlayBulletSounds();
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
    }

    void ControlMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    void PlayBulletSounds()
    {
        _enemyBulletSound.Play();
        StartCoroutine(CooldownSounds());
    }

    IEnumerator CooldownSounds()
    {
        yield return new WaitForSeconds(0.1f);
        _enemyBulletSound.Play();
    }
}
