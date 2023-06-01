using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTouch : MonoBehaviour
{
    public LegoColor color;
    public Collider boxCollider;

    Coroutine delayTimeTouch;
    bool onTouch;
    private void OnMouseDown()
    {
        if (!onTouch)
        {
            onTouch = true;
            EventDispatcher.Instance.PostEvent(EventID.TabAttackLego, color);

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
