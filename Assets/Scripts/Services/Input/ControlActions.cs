//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/Services/Input/ControlActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ControlActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlActions"",
    ""maps"": [
        {
            ""name"": ""player"",
            ""id"": ""463cda95-c4c5-4479-b1a5-67ff6da09343"",
            ""actions"": [
                {
                    ""name"": ""handbrake"",
                    ""type"": ""Button"",
                    ""id"": ""ce2d0c18-7db6-437a-8ac8-47344f180bca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""direction"",
                    ""type"": ""Value"",
                    ""id"": ""29eb1d2b-f9a9-4386-855f-2aeb6edfaf7a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""turn"",
                    ""type"": ""Value"",
                    ""id"": ""d4b82f2c-b231-47bf-b935-291b7800b645"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""respawn"",
                    ""type"": ""Button"",
                    ""id"": ""c36f9e0f-7a10-43a9-8580-0692d7d56554"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""79136e9e-f09d-44eb-8ca4-6e7f5e8f8553"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""handbrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""47782f30-24d6-4658-8e1d-801c53caf9e4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cc8fa479-841a-4ac9-b80d-c1072eb87f8e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""50ec8e4d-a69e-4021-8291-64bbeefade80"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""kerboard"",
                    ""id"": ""91e9bfe7-f224-49fe-bbf7-7b9f7ce41f19"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""6c6b9777-4c7d-43bb-bad6-b4a758d781c6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""7609cbc1-d0c0-4e11-b19c-ce12e7d34714"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1c29674d-fc5e-48bb-9ddb-b81feb01608f"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""respawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ui"",
            ""id"": ""3ce84f81-03bc-4ed2-96c6-9de762b95469"",
            ""actions"": [
                {
                    ""name"": ""escape"",
                    ""type"": ""Button"",
                    ""id"": ""1748c7d6-d3d3-47ab-8ad1-13d7905b9301"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""60b735d6-3501-467e-9a05-e9e0c590fb79"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // player
        m_player = asset.FindActionMap("player", throwIfNotFound: true);
        m_player_handbrake = m_player.FindAction("handbrake", throwIfNotFound: true);
        m_player_direction = m_player.FindAction("direction", throwIfNotFound: true);
        m_player_turn = m_player.FindAction("turn", throwIfNotFound: true);
        m_player_respawn = m_player.FindAction("respawn", throwIfNotFound: true);
        // ui
        m_ui = asset.FindActionMap("ui", throwIfNotFound: true);
        m_ui_escape = m_ui.FindAction("escape", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // player
    private readonly InputActionMap m_player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_player_handbrake;
    private readonly InputAction m_player_direction;
    private readonly InputAction m_player_turn;
    private readonly InputAction m_player_respawn;
    public struct PlayerActions
    {
        private @ControlActions m_Wrapper;
        public PlayerActions(@ControlActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @handbrake => m_Wrapper.m_player_handbrake;
        public InputAction @direction => m_Wrapper.m_player_direction;
        public InputAction @turn => m_Wrapper.m_player_turn;
        public InputAction @respawn => m_Wrapper.m_player_respawn;
        public InputActionMap Get() { return m_Wrapper.m_player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @handbrake.started += instance.OnHandbrake;
            @handbrake.performed += instance.OnHandbrake;
            @handbrake.canceled += instance.OnHandbrake;
            @direction.started += instance.OnDirection;
            @direction.performed += instance.OnDirection;
            @direction.canceled += instance.OnDirection;
            @turn.started += instance.OnTurn;
            @turn.performed += instance.OnTurn;
            @turn.canceled += instance.OnTurn;
            @respawn.started += instance.OnRespawn;
            @respawn.performed += instance.OnRespawn;
            @respawn.canceled += instance.OnRespawn;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @handbrake.started -= instance.OnHandbrake;
            @handbrake.performed -= instance.OnHandbrake;
            @handbrake.canceled -= instance.OnHandbrake;
            @direction.started -= instance.OnDirection;
            @direction.performed -= instance.OnDirection;
            @direction.canceled -= instance.OnDirection;
            @turn.started -= instance.OnTurn;
            @turn.performed -= instance.OnTurn;
            @turn.canceled -= instance.OnTurn;
            @respawn.started -= instance.OnRespawn;
            @respawn.performed -= instance.OnRespawn;
            @respawn.canceled -= instance.OnRespawn;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @player => new PlayerActions(this);

    // ui
    private readonly InputActionMap m_ui;
    private List<IUiActions> m_UiActionsCallbackInterfaces = new List<IUiActions>();
    private readonly InputAction m_ui_escape;
    public struct UiActions
    {
        private @ControlActions m_Wrapper;
        public UiActions(@ControlActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @escape => m_Wrapper.m_ui_escape;
        public InputActionMap Get() { return m_Wrapper.m_ui; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UiActions set) { return set.Get(); }
        public void AddCallbacks(IUiActions instance)
        {
            if (instance == null || m_Wrapper.m_UiActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UiActionsCallbackInterfaces.Add(instance);
            @escape.started += instance.OnEscape;
            @escape.performed += instance.OnEscape;
            @escape.canceled += instance.OnEscape;
        }

        private void UnregisterCallbacks(IUiActions instance)
        {
            @escape.started -= instance.OnEscape;
            @escape.performed -= instance.OnEscape;
            @escape.canceled -= instance.OnEscape;
        }

        public void RemoveCallbacks(IUiActions instance)
        {
            if (m_Wrapper.m_UiActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUiActions instance)
        {
            foreach (var item in m_Wrapper.m_UiActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UiActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UiActions @ui => new UiActions(this);
    public interface IPlayerActions
    {
        void OnHandbrake(InputAction.CallbackContext context);
        void OnDirection(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnRespawn(InputAction.CallbackContext context);
    }
    public interface IUiActions
    {
        void OnEscape(InputAction.CallbackContext context);
    }
}
