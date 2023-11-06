using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTouch : MonoBehaviour
{
    public LegoColor color;
    public SoundName soundType;
    public BulletType bulletType;
    public Collider boxCollider;
    public Transform posOnCurve;
    public Animator anim;

    Coroutine delayTimeTouch;
    bool onTouch;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (!onTouch)
        {
            onTouch = true;
            anim.SetTrigger("Attack");
            SoundManager.instance.PlaySingle(SoundType.LayerSound, SoundName.BulletLayer);
            SoundManager.instance.PlaySingle(SoundType.BulletSound, soundType);
            EventDispatcher.Instance.PostEvent(EventID.TabAttackLego, this);

            if (delayTimeTouch != null) StopCoroutine(delayTimeTouch);
            delayTimeTouch = StartCoroutine(DelayTimeTouch(0.1f));
        }
    }

    private void OnMouseUp()
    {
        
    }

    IEnumerator DelayTimeTouch(float time)
    {
        yield return new WaitForSeconds(time);
        onTouch = false;
    }

}
