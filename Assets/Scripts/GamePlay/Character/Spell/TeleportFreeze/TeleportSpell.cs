using UnityEngine;

namespace GamePlay.Character.Spell.TeleportFreeze
{
    public class TeleportSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _splashParticle;
        [SerializeField] private float _teleportationLength;
        [SerializeField] private float _manaCost;
        public void TeleportSkill()
        {
            if (_reloadSpell._canUseSpellSecond)
            {
                _reloadSpell.ReloadSecondSpell(1);
                if (_manaController.ManaReduction(_manaCost))
                {
                   Instantiate(_splashParticle,transform.position,Quaternion.identity);
                    Ray ray = new Ray(transform.position, transform.forward);
                    if (Physics.Raycast(ray, out var hit))
                        if (Physics.Raycast(ray, out hit, _teleportationLength))
                        {
                            if (hit.transform.gameObject.CompareTag("Wall"))
                            {
                                transform.position = hit.point;
                                transform.position -= transform.forward;
                            }
                        }
                        else
                        {
                            transform.position += transform.forward * _teleportationLength;
                        }
                }
            }
        }
    }
}