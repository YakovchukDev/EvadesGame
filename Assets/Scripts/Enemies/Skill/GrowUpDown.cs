using UnityEngine;

namespace Enemy.Skill
{
    public class GrowUpDown : MonoBehaviour
    {
        private bool UpDown = true;
        private float incrise = 1;
        void Start()
        {
            transform.localScale = new Vector3(incrise, incrise, incrise);
        }
        void Update()
        {
            IncreaseDecrease();
        }
        private void IncreaseDecrease()
        {
            if (UpDown == true)
            {
                if (incrise <= 4)
                {
                    incrise += 2 * Time.deltaTime;
                    transform.localScale = new Vector3(incrise, incrise, incrise);
                }
                else
                    UpDown = false;
            }
            else
            {
                if (incrise >= 1)
                {
                    incrise -= 2 * Time.deltaTime;
                    transform.localScale = new Vector3(incrise, incrise, incrise);
                }
                else
                    UpDown = true;
            }
        }
    }
}