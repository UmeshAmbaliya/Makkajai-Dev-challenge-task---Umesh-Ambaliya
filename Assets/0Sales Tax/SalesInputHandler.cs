using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalesInputHandler : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField qtyInput;
    public TMP_InputField rateInput;
    public TMP_Dropdown categorySelection;
    public Toggle importToggle;

    public void GenerateBill()
    {
        if(SalesTaxHandler.instance.purchasedItemList.Count == 0)
        {
            ShowNotification("No purchase product found");
            return;
        }
        SalesTaxHandler.instance.CreateInvoice();
    }
    public void AddProduct()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            ShowNotification("Please input product name!");
            return;
        }
       string pName = nameInput.text;

        if (string.IsNullOrEmpty(qtyInput.text))
        {
            ShowNotification("Please input quantity!");
            return;
        }
        int qty = int.Parse(qtyInput.text);

        if (string.IsNullOrEmpty(rateInput.text))
        {
            ShowNotification("Please input Rate of product!");
            return;
        }
        float rate = float.Parse(rateInput.text);

        if (string.IsNullOrEmpty(rateInput.text))
        {
            ShowNotification("Please input Rate of product!");
            return;
        }
        
        bool isImported = importToggle.isOn;

        if (currentCategory1 == 0)
        {
            ShowNotification("Please Select category!");
            return;
        }

        PurchasedItemData data = new PurchasedItemData();
        data.productName = pName;
        data.quantity = qty;
        data.basePrise = rate;
        data.isImported = isImported;
        data.category = (ProductCategory)(currentCategory1);
        SalesTaxHandler.instance.purchasedItemList.Add(data);

        nameInput.text = "";
        qtyInput.text = "";
        rateInput.text = "";
        categorySelection.value = 0;
        categorySelection.RefreshShownValue();
        importToggle.isOn = false;
    }
    int currentCategory1 = 0;
    public void OnChangeCategorySelection(int currentCategory)
    {
        this.currentCategory1 = currentCategory;
        Debug.LogError(currentCategory);
    }

    public Animator animNotification;
    public TextMeshProUGUI notificationText;
    public void ShowNotification(string str)
    {
        notificationText.text = str;
        animNotification.SetTrigger("Show");
    }
}
