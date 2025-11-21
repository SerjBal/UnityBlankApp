using UnityEngine;

namespace Serjbal
{
    public class Canvases : MonoBehaviour
    {
        public Canvas StaticCanvas;
        public Canvas DynamicCanvas;

        public void Clear()
        {
            foreach (Transform item in StaticCanvas.transform)
                Destroy(item.gameObject);

            foreach (Transform item in DynamicCanvas.transform)
                Destroy(item.gameObject);
        }
    }
}
