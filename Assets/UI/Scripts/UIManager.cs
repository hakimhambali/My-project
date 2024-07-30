//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class UIManager : MonoBehaviour
//{
//    public static UIManager Instance;

//    public GameObject mainMenu;
//    public GameObject settings;
//    public GameObject lobby;

//    private Stack<GameObject> _menuStack = new Stack<GameObject>();

//    public enum Page
//    {
//        MainMenu,
//        Settings,
//        Lobby
//    }

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }

//        // Initialize to main menu
//        InitializeMainMenu();
//    }

//    private void InitializeMainMenu()
//    {
//        mainMenu.SetActive(true);
//        settings.SetActive(false);
//        lobby.SetActive(false);
//    }

//    public void NavigateTo(Page page)
//    {
//        // Save the current menu to the stack
//        GameObject currentMenu = GetActiveMenu();
//        if (currentMenu != null)
//        {
//            _menuStack.Push(currentMenu);
//            currentMenu.SetActive(false);
//        }

//        // Load the new menu
//        GameObject newMenu = GetMenu(page);
//        newMenu.SetActive(true);
//    }

//    public void GoBack()
//    {
//        if (_menuStack.Count > 0)
//        {
//            // Get the previous menu from the stack
//            GameObject previousMenu = _menuStack.Pop();

//            // Activate the previous menu
//            previousMenu.SetActive(true);
//        }
//        else
//        {
//            Debug.LogWarning("No previous menu to go back to.");
//        }
//    }

//    private GameObject GetActiveMenu()
//    {
//        if (mainMenu.activeSelf) return mainMenu;
//        if (settings.activeSelf) return settings;
//        if (lobby.activeSelf) return lobby;
//        return null;
//    }

//    private GameObject GetMenu(Page page)
//    {
//        switch (page)
//        {
//            case Page.MainMenu: return mainMenu;
//            case Page.Settings: return settings;
//            case Page.Lobby: return lobby;
//            default: return null;
//        }
//    }
//}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuDocument;
    public GameObject settingsDocument;
    public GameObject lobbyDocument;

    private Stack<GameObject> _pages = new Stack<GameObject>();

    private VisualElement mainMenuRoot;
    private VisualElement settingsRoot;
    private VisualElement lobbyRoot;

    private void Start()
    {
        InitializeUI();

        ShowPage(mainMenuDocument);

        var buttonPlay = mainMenuRoot.Q<Button>("ButtonPlay");
        buttonPlay.clicked += () => ShowPage(lobbyDocument);

        var settingIcon = mainMenuRoot.Q("SettingIcon") as VisualElement;
        settingIcon.RegisterCallback<ClickEvent>(evt => ShowPage(settingsDocument));

        VisualElement backButton = settingsRoot.Q<VisualElement>("BackButton");
        if (backButton != null)
        {
            backButton.RegisterCallback<ClickEvent>(evt => GoBack());
        }
        else
        {
            Debug.LogWarning("BackButton not found in settingsRoot.");
        }
    }

    private void InitializeUI()
    {
        var mainMenuUIDocument = mainMenuDocument.GetComponent<UIDocument>();
        mainMenuRoot = mainMenuUIDocument.rootVisualElement;

        var settingsUIDocument = settingsDocument.GetComponent<UIDocument>();
        settingsRoot = settingsUIDocument.rootVisualElement;

        var lobbyUIDocument = lobbyDocument.GetComponent<UIDocument>();
        lobbyRoot = lobbyUIDocument.rootVisualElement;
    }

    public void ShowPage(GameObject pageToShow)
    {
        mainMenuRoot.style.display = pageToShow == mainMenuDocument ? DisplayStyle.Flex : DisplayStyle.None;
        settingsRoot.style.display = pageToShow == settingsDocument ? DisplayStyle.Flex : DisplayStyle.None;
        lobbyRoot.style.display = pageToShow == lobbyDocument ? DisplayStyle.Flex : DisplayStyle.None;

        _pages.Push(pageToShow);
    }

    public void GoBack()
    {
        Debug.Log("Pressed GoBack");

        if (_pages.Count > 1)
        {
            _pages.Pop();
            var pageToShow = _pages.Peek();
            ShowPage(pageToShow);
        }
        else
        {
            Debug.LogWarning("No previous pages to go back to.");
        }
    }
}
