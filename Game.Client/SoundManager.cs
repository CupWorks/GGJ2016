using System.IO;
using System.Media;
using Game.Core;

namespace Game.Client
{
	public class SoundManager : ISoundManager
	{
		private SoundPlayer BackgroundPlayer { get; } = new SoundPlayer();
	    private bool IsPlaying { get; set; } = false;

		public void PlayLoop(string soundFile)
		{
		    soundFile = Path.Combine("Files", soundFile);
            if(IsPlaying) StopLoop();
			BackgroundPlayer.SoundLocation = soundFile;
			BackgroundPlayer.PlayLooping();
		    IsPlaying = true;
		}

		public void StopLoop()
		{
			BackgroundPlayer.Stop();
		}
	}
}