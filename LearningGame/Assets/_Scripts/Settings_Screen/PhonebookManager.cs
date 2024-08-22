using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhonebookManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField numberInputField;
    [SerializeField] private Button addButton;
    [SerializeField] private RectTransform contactsContainer;
    [SerializeField] private GameObject contactPrefab;

    private PlayerData playerData;
    private List<GameObject> contactObjects = new List<GameObject>();

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        if (playerData == null)
        {
            Debug.LogError("PlayerData not found in the scene!");
            return;
        }

        addButton.onClick.AddListener(AddContact);
        RefreshContactList();
    }

    private void AddContact()
    {
        string name = nameInputField.text;
        string number = numberInputField.text;

        // If there is text in both input fields
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(number))
        {
            playerData.AddContact(name, number);
            // Reset the input fields
            nameInputField.text = "";
            numberInputField.text = "";
            RefreshContactList();
        }
        else
        {
            Debug.LogWarning("Name or number is empty. Cannot add contact.");
        }
    }

    private void RefreshContactList()
    {
        // Clear existing contact objects
        foreach (GameObject contactObj in contactObjects)
        {
            Destroy(contactObj);
        }
        contactObjects.Clear();

        // Create new contact objects
        List<ContactInfo> contacts = playerData.GetContacts();
        foreach (ContactInfo contact in contacts)
        {
            GameObject contactObj = Instantiate(contactPrefab, contactsContainer);
            SetupContactObject(contactObj, contact);
            contactObjects.Add(contactObj);
        }
    }

    private void SetupContactObject(GameObject contactObj, ContactInfo contact)
    {
        TMP_Text nameText = contactObj.transform.Find("NameText").GetComponent<TMP_Text>();
        TMP_Text numberText = contactObj.transform.Find("NumberText").GetComponent<TMP_Text>();
        Button deleteButton = contactObj.transform.Find("DeleteButton").GetComponent<Button>();

        nameText.text = contact.name;
        numberText.text = contact.phoneNumber;

        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() => DeleteContact(contact.phoneNumber));
    }

    private void DeleteContact(string phoneNumber)
    {
        playerData.RemoveContact(phoneNumber);
        RefreshContactList();
    }
}