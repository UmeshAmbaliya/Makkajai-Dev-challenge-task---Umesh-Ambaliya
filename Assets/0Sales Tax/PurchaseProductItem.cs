using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchaseProductItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI qtyText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI taxText;
    [SerializeField] TextMeshProUGUI finalPriseText;
    public void SetData(PurchasedItemData data, bool isResultData = false)
    {
        nameText.text = data.productName;
        qtyText.text = data.quantity.ToString();
        if (!isResultData )
        {
            priceText.text = (data.basePrise * data.quantity).ToString();
        }
        else
        {
            priceText.text = data.basePrise.ToString();
        }
        taxText.text = data.appliedTax.ToString();
        finalPriseText.text = data.purchasePriseIncludingTax.ToString();
    }
}
