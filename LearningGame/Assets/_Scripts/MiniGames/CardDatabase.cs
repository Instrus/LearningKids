using System.Collections.Generic;
using UnityEngine;

// maybe move to resources (does not seem necessary)

[CreateAssetMenu]
public class CardDatabase : ScriptableObject // holds all Cards
{
    [SerializeField] PlayerData playerData;

    [SerializeField] public Card[] FlashCards;

    [SerializeField] public Card[] FIBCards;

    // add a function for when requesting a card
    // if a contact info related card is requested, need to verify there is at least one contact in the phone book
    // if that card cannot be selected, just send back a random card, even if it's a dupe.

    public Card RequestFlashCards(int index)
    {
        // set questions before returning for contact info card

        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();

        List<ContactInfo> contacts = playerData.GetContacts();

        if (FlashCards[index].contactInfoCard == true && contacts.Count > 0)
        {
            System.Random rand = new System.Random();
            int randomContact = rand.Next(0, contacts.Count);

            FlashCards[index].question = "Whose phone numbers is this: " + contacts[randomContact].phoneNumber + "?";
            FlashCards[index].answer = contacts[randomContact].name;
            FlashCards[index].answerSet[2] = contacts[randomContact].name + "'s";
            FlashCards[index].answer = contacts[randomContact].name + "'s";
            FlashCards[index].answerIndex = 3;
        }
        else if (FlashCards[index].contactInfoCard == true && contacts.Count < 1)
        {
            return FlashCards[0]; // contact card cannot be the last one in the FlashCards list.
        }

        return FlashCards[index];

    }

    // right now FlashCards just gets from the list through GM but need to have some check (for contacts) later
    // only return the random card if the check is on
}
