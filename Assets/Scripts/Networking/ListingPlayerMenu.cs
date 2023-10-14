using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class ListingPlayerMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] public Transform _playersSpace;//oyuncu iconlar�n�n g�z�kece�i yer

    [SerializeField] public ListingPlayer _listingPlayer;//listing player scriptini �ekiyor

    public List<ListingPlayer> _listing = new List<ListingPlayer>();//yeni bir list nesnesi olu�turuyor

    private void Start()
    {
        // Instantiate existing players when first joining the room
        foreach (var player in PhotonNetwork.PlayerList)
        {
            AddPlayerToListing(player);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerToListing(newPlayer);
    }

    private void AddPlayerToListing(Player player)
    {
        ListingPlayer listing = Instantiate(_listingPlayer, _playersSpace);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listing.Add(listing);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listing.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listing[index].gameObject);
            _listing.RemoveAt(index);
        }
    }

}
