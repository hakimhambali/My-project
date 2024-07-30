using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private UIDocument _document;
    //private Button _button;
    //private VisualElement _setting;
    private List<VisualElement> _menuElements = new List<VisualElement>();
    private AudioSource _audioSource;
    //public VisualTreeAsset lobby;
    //public VisualTreeAsset settings;

    //private Stack<VisualElement> _menuStack = new Stack<VisualElement>();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();
        //_button = _document.rootVisualElement.Q("ButtonPlay") as Button;
        //_button.RegisterCallback<ClickEvent>(OnButtonPlayClicked);

        //_setting = _document.rootVisualElement.Q("SettingIcon") as VisualElement;
        //_setting.RegisterCallback<ClickEvent>(OnSettingIconClicked);

        _menuElements = _document.rootVisualElement.Query<VisualElement>().ToList();
        for (int i = 0; i < _menuElements.Count; i++)
        {
            _menuElements[i].RegisterCallback<ClickEvent>(OnAllElementsClick);
        }
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        //if (_button != null)
        //{
        //    _button.UnregisterCallback<ClickEvent>(OnButtonPlayClicked);
        //}

        //if (_setting != null)
        //{
        //    _setting.UnregisterCallback<ClickEvent>(OnSettingIconClicked);
        //}

        for (int i = 0; i < _menuElements.Count; i++)
        {
            _menuElements[i].UnregisterCallback<ClickEvent>(OnAllElementsClick);
        }
    }

    //private void OnButtonPlayClicked(ClickEvent evt)
    //{
    //    Debug.Log("Pressed OnPlayButtonClicked");

    //    VisualElement lobbyRoot = lobby.CloneTree();
    //    _document.rootVisualElement.Clear();
    //    _document.rootVisualElement.Add(lobbyRoot);
    //    //NavigateTo(lobby);
    //}

    //private void OnSettingIconClicked(ClickEvent evt)
    //{
    //    Debug.Log("Pressed OnSettingIconClicked");

    //    VisualElement settingsRoot = settings.CloneTree();
    //    _document.rootVisualElement.Clear();
    //    _document.rootVisualElement.Add(settingsRoot);
    //    //NavigateTo(settings);
    //}

    private void OnAllElementsClick(ClickEvent evt)
    {
        Debug.Log("Playing audio clip: " + _audioSource.clip.name);
        _audioSource.Play();
    }

    //private void NavigateTo(VisualTreeAsset newMenu)
    //{
    //    // Save the current menu to the stack
    //    VisualElement currentMenu = _document.rootVisualElement.ElementAt(0);
    //    _menuStack.Push(currentMenu);

    //    // Clear the current menu and load the new one
    //    VisualElement newMenuRoot = newMenu.CloneTree();
    //    _document.rootVisualElement.Clear();
    //    _document.rootVisualElement.Add(newMenuRoot);

    //    // Register the back button callback if it exists
    //    VisualElement backButton = newMenuRoot.Q<VisualElement>("BackButton");
    //    if (backButton != null)
    //    {
    //        backButton.RegisterCallback<ClickEvent>(evt => GoBack());
    //    }

    //    // Register callbacks for all elements in the new menu
    //    _menuElements = _document.rootVisualElement.Query<VisualElement>().ToList();
    //    for (int i = 0; i < _menuElements.Count; i++)
    //    {
    //        _menuElements[i].RegisterCallback<ClickEvent>(OnAllElementsClick);
    //    }
    //}

    //private void GoBack()
    //{
    //    if (_menuStack.Count > 0)
    //    {
    //        // Get the previous menu from the stack
    //        VisualElement previousMenu = _menuStack.Pop();

    //        // Clear the current menu and load the previous one
    //        _document.rootVisualElement.Clear();
    //        _document.rootVisualElement.Add(previousMenu);

    //        // Register callbacks for all elements in the previous menu
    //        _menuElements = _document.rootVisualElement.Query<VisualElement>().ToList();
    //        for (int i = 0; i < _menuElements.Count; i++)
    //        {
    //            _menuElements[i].RegisterCallback<ClickEvent>(OnAllElementsClick);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No previous menu to go back to.");
    //    }
    //}
}
