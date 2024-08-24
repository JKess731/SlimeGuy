using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UITest : MonoBehaviour
{
    private AbilityManager _abilityManager;
    private UIDocument _uiDocument;
    private VisualElement _root;

    private List<VisualElement> _visualElement;
    private Sprite[] _abilityIconArray = new Sprite[3];

    public UIDocument UIDocument { get => _uiDocument; }

    private void Start()
    {
    }

    public void OnEnable()
    {
        _abilityManager = FindObjectOfType<AbilityManager>();
        _uiDocument = GetComponent<UIDocument>();
        _root = _uiDocument.rootVisualElement;

        _visualElement = _root.Query("Icon").ToList();

        _abilityIconArray[0] = _abilityManager.Primary.Icon;
        _abilityIconArray[1] = _abilityManager.Secondary.Icon;
        _abilityIconArray[2] = _abilityManager.Dash.Icon;

        for (int i = 0; i < _abilityIconArray.Length; i++)
        {
            _visualElement[i].style.backgroundImage = new StyleBackground(_abilityIconArray[i]);
        }
    }
}
