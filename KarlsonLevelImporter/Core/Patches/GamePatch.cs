using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KarlsonLevelImporter.Core.Patches
{
    [HarmonyPatch(typeof(Game), "Win")]
    class GamePatch
    {
        static void Prefix(ref bool ___playing, ref bool ___done)
        {
			___playing = false;
			Timer.Instance.Stop();
			Time.timeScale = 0.05f;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			UIManger.Instance.WinUI(true);

			float timer = Timer.Instance.GetTimer();

			if (!LevelLoader.Playing)
			{
				int sceneIndex = int.Parse(SceneManager.GetActiveScene().name[0].ToString() ?? "");
				int subIndex;
				if (int.TryParse(SceneManager.GetActiveScene().name.Substring(0, 2) ?? "", out subIndex))
				{
					sceneIndex = subIndex;
				}
				float lastTime = SaveManager.Instance.state.times[sceneIndex];
				if (timer < lastTime || lastTime == 0f)
				{
					SaveManager.Instance.state.times[sceneIndex] = timer;
					SaveManager.Instance.Save();
				}
			} else
            {
				LevelTimes levelTimes = new LevelTimes();

				float lastTime = levelTimes.Get(LevelLoader.currentBundlePath);
				if (timer < lastTime || lastTime == 0f)
				{
					levelTimes.Set(LevelLoader.currentBundlePath, timer);
					levelTimes.Save();
				}
			}
			UnityEngine.Debug.Log("time has been saved as: " + Timer.Instance.GetFormattedTime(timer));
			___done = true;
		}
    }
}
