using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemPickupable : _MonoBehaviour
{
    public ItemCtrl itemCtrl;

    public virtual ItemCode GetItemCode()
    {
        return ItemCodeParse.FromString(transform.parent.name);
    }

    public virtual void Picked()
    {
        this.itemCtrl.itemSpawner.Despawn(transform.parent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Inventory inventory = other.GetComponentInChildren<Inventory>();
            inventory.AddItem(this.itemCtrl.ItemInventory);
            this.Picked();
        }
    }

    //private void FixedUpdate()
    //{
    //    transform.parent.position = Vector3.MoveTowards(transform.parent.position, PlayerCtrl.Instance.transform.position, Time.fixedDeltaTime*10f);
    //}
}
