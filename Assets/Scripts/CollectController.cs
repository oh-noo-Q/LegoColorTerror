using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectController : MonoBehaviour
{
    [SerializeField] Transform rootTrans, stackBallTrans;

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.CompareTag("Wall"))
        //{
        //    if(stackBallTrans.childCount == 0)
        //    {
        //        GameManager.Instance.OnGameLose();
        //    }
        //}

        if(other.gameObject.tag == "Ball")
        {
            AddBall(other.gameObject);
            
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Coin"))
        {
            UIManager.Instance.UpdateCoins();
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("FinishPosition"))
        {
        }
    }

    private void AddBall(GameObject ball)
    {
        int index = stackBallTrans.childCount + 1;
        var newBallPosition = new Vector3(stackBallTrans.position.x, stackBallTrans.position.y - index, stackBallTrans.position.z);
        GameObject collectedBall = Instantiate(ball, Vector3.zero, Quaternion.identity);
        collectedBall.transform.parent = stackBallTrans;
        collectedBall.transform.position = newBallPosition;
        collectedBall.transform.tag = "CollectedBall";

        Vector3 newPosRoot = new Vector3(rootTrans.position.x, rootTrans.position.y + 1, rootTrans.position.z);

        rootTrans.DOMoveY(newPosRoot.y, 0.2f, true);
        //rootTrans.position = new Vector3(rootTrans.position.x, rootTrans.position.y + 1, rootTrans.position.z);
    }
}
