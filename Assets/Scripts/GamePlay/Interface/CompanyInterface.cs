using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Interface
{
    public class CompanyInterface : InterfaceController
    {
        public override void ExitAndSave()
        {
            base.ExitAndSave();
            MapManager.MainDataCollector.SaveCoins();
        }

        public void FinishedExit()
        {
            base.ExitAndSave();
        }
    }
}