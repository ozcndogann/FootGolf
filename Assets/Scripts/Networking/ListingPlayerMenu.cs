using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class ListingPlayerMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] public Transform _playersSpace, _playersSpace1, _playersSpace2, _playersSpace3, _playersSpace4, _playersSpace5;//oyuncu iconlarýnýn gözükeceði yer

    [SerializeField] public ListingPlayer _listingPlayer;//listing player scriptini çekiyor

    public List<ListingPlayer> _listing = new List<ListingPlayer>();//yeni bir list nesnesi oluþturuyor

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
        if (CreateAndJoinRandomRooms.practice || CreateAndJoinRooms.practice)
        {
            if (player.ActorNumber == 1)
            {
                ListingPlayer listing1 = Instantiate(_listingPlayer, _playersSpace4);
                if (listing1 != null)
                {
                    listing1.SetPlayerInfo(player);
                    _listing.Add(listing1);
                }


            }
        }

        if (CreateAndJoinRandomRooms.versus|| CreateAndJoinRooms.versus) 
        {

            if (player.ActorNumber == 1)
            {
                ListingPlayer listing1 = Instantiate(_listingPlayer, _playersSpace4);
                if (listing1 != null)
                {
                    listing1.SetPlayerInfo(player);
                    _listing.Add(listing1);
                }
            }
            else if (player.ActorNumber == 2)
            {
                ListingPlayer listing2 = Instantiate(_listingPlayer, _playersSpace5);
                if (listing2 != null)
                {
                    listing2.SetPlayerInfo(player);
                    _listing.Add(listing2);
                }
            }

        }
        if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
        {
            if (player.ActorNumber == 1)
            {
                ListingPlayer listing1 = Instantiate(_listingPlayer, _playersSpace);
                if (listing1 != null)
                {
                    listing1.SetPlayerInfo(player);
                    _listing.Add(listing1);
                }
            }
            else if (player.ActorNumber == 2)
            {
                ListingPlayer listing2 = Instantiate(_listingPlayer, _playersSpace1);
                if (listing2 != null)
                {
                    listing2.SetPlayerInfo(player);
                    _listing.Add(listing2);
                }
            }
            else if (player.ActorNumber == 3)
            {
                ListingPlayer listing3 = Instantiate(_listingPlayer, _playersSpace2);
                if (listing3 != null)
                {
                    listing3.SetPlayerInfo(player);
                    _listing.Add(listing3);
                }
            }
            else if (player.ActorNumber == 4)
            {
                ListingPlayer listing4 = Instantiate(_listingPlayer, _playersSpace3);
                if (listing4 != null)
                {
                    listing4.SetPlayerInfo(player);
                    _listing.Add(listing4);
                }
            }

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
