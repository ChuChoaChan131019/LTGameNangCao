using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;
    public List<string> InventoryItemList= new List<string>();

    //Category btn
    Button toolsBTN;
    //Craft btn
    Button craftAxeBTN;
    //Requirememt Text
    Text AxeReq1, AxeReq2;
    public bool isOpen;
    //All Blueprint;
    public Blueprint AxeBLP= new Blueprint("Axe",2, "Stone",3, "Stick", 3);
    public static CraftingSystem Instance { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy( gameObject);
        }
        else
        {
            Instance = this;
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpen = false;
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });
        //Axe
        AxeReq1= toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();
        craftAxeBTN= toolsScreenUI.transform.Find("Axe").transform.Find("CraftButton").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });



    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }
    void CraftAnyItem(Blueprint blueprintTocraft)
    {
        InventorySystem.Instance.AddToInventory(blueprintTocraft.itemName);

        if (blueprintTocraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintTocraft.Req1, blueprintTocraft.Req1Amount);
        }
        else if (blueprintTocraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintTocraft.Req1, blueprintTocraft.Req1Amount);

            InventorySystem.Instance.RemoveItem(blueprintTocraft.Req2, blueprintTocraft.Req2Amount);
        }
        StartCoroutine(caclculate());

        RefreshNeededItem();

    }

    public IEnumerator caclculate()
    {
        yield return new WaitForSeconds(0.05f);
        InventorySystem.Instance.ReCalculateList();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshNeededItem();
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {

            Debug.Log("i is pressed");
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
                Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }
    private void RefreshNeededItem()
    {
        int stone_count = 0;
        int stick_count = 0;

        InventoryItemList = InventorySystem.Instance.itemList;
        foreach (string itemName in InventoryItemList)
        {
           switch(itemName)
           {
               case "Stone":
                   stone_count++;
                   break;
               case "Stick":
                   stick_count++;
                   break;
           }
        }

        //axe
        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";
        
        if(stone_count >= 3 && stick_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }


    }

}
