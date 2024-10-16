using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesTaxHandler : MonoBehaviour
{
    public List<PurchasedItemData> purchasedItemList = new List<PurchasedItemData>();
    public GameObject productUIPrefab;
    public Transform productContainer;
   
    // Start is called before the first frame update
    void Start()
    {
        CreateInvoice();    
    }

    void CreateInvoice()
    {
        int totalQty = 0;
        float totalBill = 0;
        float totalTax = 0;
        float totalBasePrise = 0;
        for (int i = 0; i < purchasedItemList.Count; i++)
        {
            float taxValue = CalculateSalesTax(purchasedItemList[i].basePrise * purchasedItemList[i].quantity, GetSalesTaxRate(purchasedItemList[i].isImported, purchasedItemList[i].category));
            purchasedItemList[i].purchasePriseIncludingTax = taxValue + (purchasedItemList[i].basePrise * purchasedItemList[i].quantity);
            purchasedItemList[i].appliedTax = taxValue;

            GameObject go = Instantiate(productUIPrefab, productContainer);
            go.GetComponent<PurchaseProductItem>().SetData(purchasedItemList[i]);
            totalQty += purchasedItemList[i].quantity;
            totalBill += purchasedItemList[i].purchasePriseIncludingTax;
            totalTax += taxValue;
            totalBasePrise += (purchasedItemList[i].basePrise * purchasedItemList[i].quantity);
        }
        GameObject result = Instantiate(productUIPrefab, productContainer);
        PurchasedItemData resultData = new PurchasedItemData();
        resultData.productName = "Total";
        resultData.quantity = totalQty;
        resultData.appliedTax = totalTax;
        resultData.purchasePriseIncludingTax = totalBill;
        resultData.basePrise = totalBasePrise;
        result.GetComponent<PurchaseProductItem>().SetData(resultData,true);
    }

    float CalculateSalesTax(float price, float rate)
    {
        float rawTax = (price * rate) / 100;
        float roundedTax = Mathf.Ceil(rawTax / 0.05f) * 0.05f;
        return roundedTax;
    }

    float GetSalesTaxRate(bool isImported,ProductCategory category) 
    {
        float f = 0;
        if (isImported)
            f += 5;
        
        if (category == ProductCategory.Other)
            f += 10;
        
        return f;
    }
}
[System.Serializable]
public class PurchasedItemData
{
    public string productName;
    public float basePrise;
    public int quantity;
    [HideInInspector] public float purchasePriseIncludingTax;
    [HideInInspector] public float appliedTax;
    public ProductCategory category;
    public bool isImported;

}
public enum ProductCategory
{
    Food,
    Medicines,
    Book,
    Other
}
