using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    SpriteRenderer _Sr;        
    public bool _is_disappear= false;
    Collider2D _platform_collider;
    Collider2D _player_collider;

    void Start()
    {
        _Sr = GetComponent<SpriteRenderer>();        
        _Sr.color = new Color(0.7f, 0.3f, 0.5f, 1f);
        _platform_collider = GetComponent<Collider2D>();
        _player_collider = PlayerController.instance.GetComponent<Collider2D>();
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");        
        if (!_is_disappear)
        {            
            StartCoroutine(nameof(DisappearAfterSeconds));            
        }
    }

    IEnumerator DisappearAfterSeconds()
    {        
        yield return new WaitForSeconds(1f);
        _Sr.color = new Color(_Sr.color.r, _Sr.color.g, _Sr.color.b, 0.3f);
        _is_disappear = true;        
        Physics2D.IgnoreCollision(_platform_collider, _player_collider, true);
        gameObject.layer = LayerMask.NameToLayer("Default");
        
        yield return new WaitForSeconds(2.5f);
        _Sr.color = new Color(_Sr.color.r, _Sr.color.g, _Sr.color.b, 1f);
        _is_disappear = false;
        Physics2D.IgnoreCollision(_platform_collider, _player_collider, false);
        gameObject.layer = LayerMask.NameToLayer("Ground");

    }
}
