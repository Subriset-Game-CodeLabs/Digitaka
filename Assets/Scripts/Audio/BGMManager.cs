namespace Audio
{
    public class BGMManager: PersistentSingleton<BGMManager>
    {
        private SoundType _currentBGM;
        
        public void PlayBGM(SoundType bgm)
        {
            if (_currentBGM == bgm) return;
            _currentBGM = bgm;
            if (_currentBGM.ToString() != null)
            {
                AudioManager.Instance.StopSound(_currentBGM);
            }
            AudioManager.Instance.PlaySound(bgm);
        }
    }
}