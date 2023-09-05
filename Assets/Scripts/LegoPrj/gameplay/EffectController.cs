using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] GameObject explosionColor;
    [SerializeField] List<GameObject> detailsMaterialOb;
    [SerializeField] GameObject portalEffect;

    public void GenExplosion(Transform parent, Material colorMat)
    {
        for(int i = 0; i < detailsMaterialOb.Count; i++)
        {
            detailsMaterialOb[i].GetComponent<ParticleSystemRenderer>().material = colorMat;
        }

        GameObject fx = Instantiate(explosionColor);
        fx.SetActive(true);
        fx.transform.position = parent.position;

        Destroy(fx.gameObject, 1f);
    }

    public void GenPortalFlyEnemy(float xPosition)
    {
        GameObject fx = Instantiate(portalEffect);
        fx.SetActive(true);
        fx.transform.position = new Vector3(xPosition, 8, -23);

        Destroy(fx.gameObject, 1.5f);
    }
}
