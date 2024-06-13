using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BtnThrowBomb : BaseButton {
    protected override void OnClick()
    {
        
    }
    protected float throwForce;
    protected bool isThrow;
    public Image fillImage;
    public string typebomb;
    private void FixedUpdate() {
      if (isThrow) {
        throwForce += Time.fixedDeltaTime;
        fillImage.fillAmount = throwForce;
      }
    }

    public void OnClickDownThrow() {
      PlayerCtrl.Instance.Animator.SetTrigger("throw");
      throwForce = 0;
      isThrow = true;
      // Collider[] enemy = Physics.OverlapBox();
    }
    
    public async void OnClickUpThrow() {
      PlayerCtrl.Instance.Animator.SetTrigger("throw1");
      isThrow = false;
      await Task.Delay(TimeSpan.FromSeconds(0.7f));
      PlayerShooting.Instance.Throw(throwForce, typebomb);
    }
}
