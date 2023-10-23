using UnityEngine;
using Photon.Pun;

public class MaterialBaseMapChanger : MonoBehaviourPun
{
    public Texture newBaseMap; // Drag your new texture here in the inspector

    private void Start()
    {
        // Check if the object doesn't belong to the local player.
        if (!photonView.IsMine)
        {
            ChangeBaseMap();
        }
    }

    private void ChangeBaseMap()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend && rend.material)
        {
            rend.material.SetTexture("_BaseMap", newBaseMap);
        }
    }
}
