using UnityEngine;

namespace Aezakmi
{
    public class MoneyEjector : MonoBehaviour
    {
        [SerializeField] private MoneyThrower moneyThrower;
        [SerializeField] private new ParticleSystem particleSystem;

        private void Update()
        {
            if (!moneyThrower.HasMoneyToThrow)
            {
                particleSystem.Stop();
            }
            else
            {
                particleSystem.Play();
            }
        }
    }
}
