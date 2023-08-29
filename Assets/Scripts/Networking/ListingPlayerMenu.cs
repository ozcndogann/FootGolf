using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class ListingPlayerMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _playersSpace;//oyuncu iconlar�n�n g�z�kece�i yer

    [SerializeField] private ListingPlayer _listingPlayer;//listing player scriptini �ekiyor

    private List<ListingPlayer> _listing = new List<ListingPlayer>();//yeni bir list nesnesi olu�turuyor


    public override void OnPlayerEnteredRoom(Player newPlayer)//oyunuyu lobiye ekleyen fonksyon
    {
        ListingPlayer listing = Instantiate(_listingPlayer, _playersSpace);//burada oyuncular� iconlar�n oluca�� yerede instantiate ediyor
        if (listing != null)
        {
            listing.SetPlayerInfo(newPlayer);
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
