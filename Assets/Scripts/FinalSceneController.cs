using UnityEngine;

/*
- [+]  Включается новый спрайт фона (кадр с дохлой рыбкой).
- [+]  Включается закадровый голос "ну как же так?"
- [+]  Включается черный экран. Через секунду звук смывания рыбки в унитаз.
- [+]  Черный экран держится еще пару секунд и включается закадровый голос "ну и глупая рыбка".
- [+]  Звук опускания рыбки в аквариум.
- [ ]  Черный экран пропадает и начинается новая игра с новым ассетом рыбки.
*/

public class FinalSceneController : MonoBehaviour
{
    [SerializeField] private CameraFollow _camFollow;
    [SerializeField] private WaterUp _wu;
    [SerializeField] private PlayerCutsceneController _player;
    [SerializeField] private LevelChange _level;
    [SerializeField] private GameObject _deadFishImage;
    [SerializeField] private FadePanel _fadePanel;
    [SerializeField] private AudioClip _voiceOne;
    [SerializeField] private AudioClip _voiceTwo;
    [SerializeField] private AudioClip _unitazSound;
    [SerializeField] private AudioClip _aquariumBulk;
    [SerializeField] private AudioSource _backgroundMusic;

    public void EnableScene()
    {
        _fadePanel.TurnOn(ChangeImage);
        _backgroundMusic.mute = true;
    }

    private void ChangeImage()
    {
        _deadFishImage.SetActive(true);
        _fadePanel.TurnOff(VoiceOne);
    }

    private void VoiceOne()
    {
        SoundManager.singleton.PlaySoud(_voiceOne);
        Invoke(nameof(FadeAgain), 4.2f);
    }

    private void FadeAgain()
    {
        _fadePanel.TurnOn(() => Invoke(nameof(UnitazSound), 1f));
    }

    private void UnitazSound()
    {
        SoundManager.singleton.PlaySoud(_unitazSound);
        Invoke(nameof(VoiceTwo), 3f);
    }

    private void VoiceTwo()
    {
        SoundManager.singleton.PlaySoud(_voiceTwo);
        Invoke(nameof(AquariumBulk), 3f);
    }

    private void AquariumBulk()
    {
        SoundManager.singleton.PlaySoud(_aquariumBulk);
        Invoke(nameof(Restart), 1.75f);
    }

    private void Restart()
    {
        _deadFishImage.SetActive(false);

        WaterUp._currentLevelIndex = -1;
        _wu.enabled = true;
        _wu.ResetPosition();
        _level.OnRestart();
        _camFollow.Reset();
        _player.Reset();
        _backgroundMusic.mute = false;
        _fadePanel.TurnOff(null);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.H))
        {
            EnableScene();
        }
    }
}
