using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoScroll : _MonoBehaviour
{
    [SerializeField] protected WormHole wormHole;
    

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.wormHole = GameObject.Find("WormHole").GetComponent<WormHole>();
    }

    public virtual void StartCorou()
    {
        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        yield return new WaitForSeconds(0.5f);
        float t0 = 0f;
        while (t0 < 1f)
        {
            t0 += Time.fixedDeltaTime/5f;
            this.Scroll(t0);
            this.HandleHorizontalScroll();
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        
        UIReward.Instance.Toggle();

        UIItemInventory uIItemInventory;
        if (transform.GetChild(1).localPosition.x >= 290)
        {
            uIItemInventory = transform.GetChild(1).GetComponent<UIItemInventory>();
        }
        else uIItemInventory = transform.GetChild(2).GetComponent<UIItemInventory>();

        UIReward.Instance.AddReward(uIItemInventory);

        this.wormHole.transform.position = PlayerCtrl.Instance.transform.position + new Vector3(0, 0, 5);
        yield return new WaitForSeconds(3f);
        ItemDropSpawner.Instance.GetAllItem();
    }

    //protected virtual void AddReward(UIItemInventory uIItemInventory)
    //{
    //    Transform uiItem = RewardSpawner.Instance.Spawn("UIInvItemReward", transform.position, transform.rotation);

    //    uiItem.transform.parent = this.holder;
    //    uiItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(100, 0, 0);
    //    UIItemInventory uIItemInventory1;
    //    uIItemInventory1 = uiItem.GetComponent<UIItemInventory>();
    //    Debug.Log(uIItemInventory.ItemName);
    //    uIItemInventory1.ShowItem(uIItemInventory);

    //    StartCoroutine(GetReward(uiItem));

    //    itemInventory.itemProfileSO = ItemProfileSO.FindByItemName(uIItemInventory1.ItemName.text.ToString());
    //    //if(itemInventory.itemProfileSO.itemType == ItemType.Clothing)
    //    //{

    //    //}

    //    itemInventory.itemCount = Int32.Parse(uIItemInventory1.ItemCount.text);
    //    PlayerCtrl.Instance.Inventory.AddItem(itemInventory);
    //}

    //IEnumerator GetReward(Transform uiItem)
    //{
    //    yield return new WaitForSeconds(2f);
    //    uiItem.transform.DOMove(this.inventory.transform.position, 1).SetUpdate(true);
    //    uiItem.transform.DOScale(Vector3.zero, 1).SetUpdate(true);
    //    RewardSpawner.Instance.ClearItems(this.holder);
    //}

    protected virtual void Scroll(float t0)
    {
        float  rand = UnityEngine.Random.Range(40, 60);
        foreach (RectTransform transform in transform)
        {
            Vector2 pos = transform.anchoredPosition;
            transform.anchoredPosition = Vector2.Lerp(pos, pos - new Vector2(rand, 0), t0);
        }
    }

    private void HandleHorizontalScroll()
    {
        if (transform.childCount < 1) return;
        int currItemIndex = 0;
        var currItem = transform.GetChild(currItemIndex);
        if (!ReachedThreshold(currItem)) return;

        int endItemIndex = transform.childCount - 1;
        Transform endItem = transform.GetChild(endItemIndex);
        Vector2 newPos = endItem.localPosition;
        newPos.x += 200 + 10;

        currItem.localPosition = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    private bool ReachedThreshold(Transform item)
    {
        return item.localPosition.x < -50;
    }
}
