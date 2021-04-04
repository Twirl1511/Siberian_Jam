using UnityEngine;

/*
- [+]  Включается новый спрайт фона (кадр с дохлой рыбкой).
- [+]  Включается закадровый голос "ну как же так?"
- [+]  Включается черный экран. Через секунду звук смывания рыбки в унитаз.
- [ ]  Черный экран держится еще пару секунд и включается закадровый голос "ну и глупая рыбка".
- [ ]  Звук опускания рыбки в аквариум.
- [ ]  Черный экран пропадает и начинается новая игра с новым ассетом рыбки.
*/

public class FinalSceneController : MonoBehaviour
{
    [SerializeField] private GameObject _deadFishImage;
    [SerializeField] private FadePanel _fadePanel;
    [SerializeField] private AudioClip _voiceOne;
    [SerializeField] private AudioClip _voiceTwo;
    [SerializeField] private AudioClip _unitazSound;

    public void EnableScene()
    {
        _fadePanel.TurnOn(ChangeImage);
    }

    private void ChangeImage()
    {
        _deadFishImage.SetActive(true);
        _fadePanel.TurnOff(VoiceOne);
    }

    private void VoiceOne()
    {
        SoundManager.singleton.PlaySoud(_voiceOne);
        Invoke(nameof(FadeAgain), 2f);
    }

    private void FadeAgain()
    {
        _fadePanel.TurnOn(Sounds);
    }

    private void Sounds()
    {
        SoundManager.singleton.PlaySoud(_unitazSound);
        
    }



    private void Update() {
        if(Input.GetKeyDown(KeyCode.H))
        {
            EnableScene();
        }
    }
}
