using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public RTLTextMeshPro roleText;
    public RTLTextMeshPro buttonText;
    public Button mainButton;

    [Header("Player Selection Buttons")]
    public GameObject playerButtonsPanel;
    public Button player1Btn;
    public Button player2Btn;
    public Button player3Btn;
    public Button player4Btn;

    [Header("Roles")]
    public string roleShah   = "شاه";
    public string roleVazir  = "وزیر";
    public string roleJallad = "جلاد";
    public string roleDozd   = "دزد";

    private List<string> roles = new List<string>();

    private int currentPlayerIndex = 0;
    private bool isShowingRole = false;

    private enum Phase
    {
        ShowRoles,
        Guess_Shah,
        Guess_Vazir
    }

    private Phase phase = Phase.ShowRoles;

    void Start()
    {
        ResetFullGame();
    }

    // ----------------------------------------------------------------
    // RESET GAME COMPLETELY — (FULL SHUFFLE)
    // ----------------------------------------------------------------
    void ResetFullGame()
    {
        roles.Clear();
        roles.Add(roleShah);
        roles.Add(roleVazir);
        roles.Add(roleJallad);
        roles.Add(roleDozd);

        Shuffle(roles);

        StartShowRolesAgain();
    }

    // ----------------------------------------------------------------
    // RESTART SHOWING ROLES (WITHOUT SHUFFLE)
    // ----------------------------------------------------------------
    void StartShowRolesAgain()
    {
        phase = Phase.ShowRoles;

        currentPlayerIndex = 0;
        isShowingRole = false;

        mainButton.gameObject.SetActive(true);
        playerButtonsPanel.SetActive(false);

        roleText.text = "گوشی را بده به بازیکن ۱";
        buttonText.text = "نمایش نقش";

        mainButton.onClick.RemoveAllListeners();
        mainButton.onClick.AddListener(ShowRolesFlow);
    }

    // ----------------------------------------------------------------
    // SHOW ROLES PHASE
    // ----------------------------------------------------------------
    void ShowRolesFlow()
    {
        if (!isShowingRole)
        {
            isShowingRole = true;

            int p = currentPlayerIndex + 1;
            string role = roles[currentPlayerIndex];

            roleText.text =
                $"نوبت بازیکن {p}\n\n" +
                $"نقش تو:\n{role}";

            buttonText.text = "تمام شد";
        }
        else
        {
            isShowingRole = false;
            currentPlayerIndex++;

            if (currentPlayerIndex >= 4)
            {
                EnterGuessByShah();
                return;
            }

            int next = currentPlayerIndex + 1;
            roleText.text = $"گوشی را بده به بازیکن {next}";
            buttonText.text = "نمایش نقش";
        }
    }

    // ----------------------------------------------------------------
    // SHAH GUESS
    // ----------------------------------------------------------------
    void EnterGuessByShah()
    {
        phase = Phase.Guess_Shah;

        mainButton.gameObject.SetActive(false);
        playerButtonsPanel.SetActive(true);

        roleText.text = "شاه، حدس بزن وزیر کدام بازیکن است.";

        AssignPlayerButtonActions(OnShahGuess);
    }

    void OnShahGuess(int guessedPlayer)
    {
        int shahIndex  = roles.IndexOf(roleShah);
        int vazirIndex = roles.IndexOf(roleVazir);

        if (guessedPlayer == vazirIndex)
        {
            roleText.text =
                "شاه درست حدس زد!\n" +
                "حالا وزیر باید جلاد را پیدا کند.";

            EnterGuessByVazir();
        }
        else
        {
            // ❌ شاه اشتباه کرد → شاه با بازیکن انتخابی جابجا می‌شود
            string temp = roles[guessedPlayer];
            roles[guessedPlayer] = roleShah;
            roles[shahIndex] = temp;

            roleText.text =
                "شاه اشتباه حدس زد!\n" +
                "نقش شاه با بازیکن انتخاب‌شده عوض شد.\n" +
                "نقش‌ها دوباره نمایش داده می‌شوند.";

            StartShowRolesAgain();
        }
    }

    // ----------------------------------------------------------------
    // VAZIR GUESS
    // ----------------------------------------------------------------
    void EnterGuessByVazir()
    {
        phase = Phase.Guess_Vazir;

        roleText.text = "وزیر، حدس بزن جلاد کدام بازیکن است.";

        AssignPlayerButtonActions(OnVazirGuess);
    }

    void OnVazirGuess(int guessedPlayer)
    {
        int vazirIndex = roles.IndexOf(roleVazir);
        int jalladIndex = roles.IndexOf(roleJallad);

        if (guessedPlayer == jalladIndex)
        {
            roleText.text =
                "وزیر درست حدس زد!\n" +
                "جلاد پیدا شد 🎉\n" +
                "بازی دوباره آغاز می‌شود.";

            ResetFullGame();
        }
        else
        {
            // ❌ وزیر اشتباه کرد → وزیر با بازیکن انتخابی جابجا می‌شود
            string temp = roles[guessedPlayer];
            roles[guessedPlayer] = roleVazir;
            roles[vazirIndex] = temp;

            roleText.text =
                "وزیر اشتباه حدس زد!\n" +
                "نقش وزیر با بازیکن انتخاب‌شده عوض شد.\n" +
                "نقش‌ها دوباره نمایش داده می‌شوند.";

            StartShowRolesAgain();
        }
    }

    // ----------------------------------------------------------------
    // UTILITY
    // ----------------------------------------------------------------
    void AssignPlayerButtonActions(System.Action<int> callback)
    {
        player1Btn.onClick.RemoveAllListeners();
        player2Btn.onClick.RemoveAllListeners();
        player3Btn.onClick.RemoveAllListeners();
        player4Btn.onClick.RemoveAllListeners();

        player1Btn.onClick.AddListener(() => callback(0));
        player2Btn.onClick.AddListener(() => callback(1));
        player3Btn.onClick.AddListener(() => callback(2));
        player4Btn.onClick.AddListener(() => callback(3));
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            string tmp = list[i];
            list[i] = list[rnd];
            list[rnd] = tmp;
        }
    }
}
