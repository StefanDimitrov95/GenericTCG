using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class CardScaling
    {
        static readonly Vector3 cardPopUpScale = new Vector3(0.5F, 0.5F);

        public static void UpscaleCard(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.transform.localScale += cardPopUpScale;
            //Debug.Log("LOCAL SCALE:" + monoBehaviour.transform.localScale);
            //Debug.Log("LOSSY SCALE:" + monoBehaviour.transform.lossyScale);
        }

        public static void DownscaleCard(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.transform.localScale -= cardPopUpScale;
            //Debug.Log("LOCAL SCALE:" + monoBehaviour.transform.localScale);
            //Debug.Log("LOSSY SCALE:" + monoBehaviour.transform.lossyScale);
        }
    }
}
