//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/InputSysteme/Inputs.inputactions
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

public partial class @Inputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""cd3de4c2-e5d5-4720-adf4-107cb1d43ec4"",
            ""actions"": [
                {
                    ""name"": ""Movements"",
                    ""type"": ""Value"",
                    ""id"": ""0b0508f9-d8b4-452e-88d9-f7aa5c6f8bf3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""InteractWithObjects"",
                    ""type"": ""Button"",
                    ""id"": ""c1e00509-ffe2-4100-816c-d1e47c675c5f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PushOtherPlayers"",
                    ""type"": ""Button"",
                    ""id"": ""8618626e-eb57-4352-8785-859f1d280daf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""5e7dbda8-b49f-4287-aa54-c19a833ab174"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ReadyToPlay"",
                    ""type"": ""Button"",
                    ""id"": ""b3ff9ffd-bda2-49f6-85c3-be46b1d5e05e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c76633a-cb58-4b2b-a07e-a437c1da038f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movements"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bda4677f-dca7-4884-bac5-8997e92ab3a1"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movements"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e57d282e-c713-4ba2-9290-ce7942e13d0c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractWithObjects"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1eb10e09-661b-4f41-bacc-ee27bf4cb27f"",
                    ""path"": ""<HID::Microntek              USB Joystick          >/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractWithObjects"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce69c7a8-81a9-4690-8369-0f7f8c0ebbfd"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PushOtherPlayers"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7ccea3e-0d15-4779-ad0a-ee97f5fb6879"",
                    ""path"": ""<HID::Microntek              USB Joystick          >/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PushOtherPlayers"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c04a8055-3b62-41e6-9230-fd1e3cd826f8"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12b564fc-5585-4431-bbff-6aaa717ca18e"",
                    ""path"": ""<HID::Microntek              USB Joystick          >/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7c9cc44-0572-44a3-b7a2-2f088acc2a2b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReadyToPlay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GUI"",
            ""id"": ""45ef5027-8015-4b26-ad50-fa115ca81363"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""0ad75193-1e57-4de2-b34f-ad64e165b9db"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Validate"",
                    ""type"": ""Button"",
                    ""id"": ""b66a2482-7c54-47fe-a9c4-67cc68f939c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""543c3412-183c-4e65-a595-b70242fde18e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acff0ead-35f6-4392-a273-e56710320ed0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53b2efae-ae38-4c52-8461-4fe0a7e75c34"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Validate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0aea59c6-c459-41bb-ad7d-ebee8007dd1b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Validate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_Movements = m_InGame.FindAction("Movements", throwIfNotFound: true);
        m_InGame_InteractWithObjects = m_InGame.FindAction("InteractWithObjects", throwIfNotFound: true);
        m_InGame_PushOtherPlayers = m_InGame.FindAction("PushOtherPlayers", throwIfNotFound: true);
        m_InGame_Pause = m_InGame.FindAction("Pause", throwIfNotFound: true);
        m_InGame_ReadyToPlay = m_InGame.FindAction("ReadyToPlay", throwIfNotFound: true);
        // GUI
        m_GUI = asset.FindActionMap("GUI", throwIfNotFound: true);
        m_GUI_Navigate = m_GUI.FindAction("Navigate", throwIfNotFound: true);
        m_GUI_Validate = m_GUI.FindAction("Validate", throwIfNotFound: true);
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

    // InGame
    private readonly InputActionMap m_InGame;
    private List<IInGameActions> m_InGameActionsCallbackInterfaces = new List<IInGameActions>();
    private readonly InputAction m_InGame_Movements;
    private readonly InputAction m_InGame_InteractWithObjects;
    private readonly InputAction m_InGame_PushOtherPlayers;
    private readonly InputAction m_InGame_Pause;
    private readonly InputAction m_InGame_ReadyToPlay;
    public struct InGameActions
    {
        private @Inputs m_Wrapper;
        public InGameActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movements => m_Wrapper.m_InGame_Movements;
        public InputAction @InteractWithObjects => m_Wrapper.m_InGame_InteractWithObjects;
        public InputAction @PushOtherPlayers => m_Wrapper.m_InGame_PushOtherPlayers;
        public InputAction @Pause => m_Wrapper.m_InGame_Pause;
        public InputAction @ReadyToPlay => m_Wrapper.m_InGame_ReadyToPlay;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void AddCallbacks(IInGameActions instance)
        {
            if (instance == null || m_Wrapper.m_InGameActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InGameActionsCallbackInterfaces.Add(instance);
            @Movements.started += instance.OnMovements;
            @Movements.performed += instance.OnMovements;
            @Movements.canceled += instance.OnMovements;
            @InteractWithObjects.started += instance.OnInteractWithObjects;
            @InteractWithObjects.performed += instance.OnInteractWithObjects;
            @InteractWithObjects.canceled += instance.OnInteractWithObjects;
            @PushOtherPlayers.started += instance.OnPushOtherPlayers;
            @PushOtherPlayers.performed += instance.OnPushOtherPlayers;
            @PushOtherPlayers.canceled += instance.OnPushOtherPlayers;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @ReadyToPlay.started += instance.OnReadyToPlay;
            @ReadyToPlay.performed += instance.OnReadyToPlay;
            @ReadyToPlay.canceled += instance.OnReadyToPlay;
        }

        private void UnregisterCallbacks(IInGameActions instance)
        {
            @Movements.started -= instance.OnMovements;
            @Movements.performed -= instance.OnMovements;
            @Movements.canceled -= instance.OnMovements;
            @InteractWithObjects.started -= instance.OnInteractWithObjects;
            @InteractWithObjects.performed -= instance.OnInteractWithObjects;
            @InteractWithObjects.canceled -= instance.OnInteractWithObjects;
            @PushOtherPlayers.started -= instance.OnPushOtherPlayers;
            @PushOtherPlayers.performed -= instance.OnPushOtherPlayers;
            @PushOtherPlayers.canceled -= instance.OnPushOtherPlayers;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @ReadyToPlay.started -= instance.OnReadyToPlay;
            @ReadyToPlay.performed -= instance.OnReadyToPlay;
            @ReadyToPlay.canceled -= instance.OnReadyToPlay;
        }

        public void RemoveCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInGameActions instance)
        {
            foreach (var item in m_Wrapper.m_InGameActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InGameActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InGameActions @InGame => new InGameActions(this);

    // GUI
    private readonly InputActionMap m_GUI;
    private List<IGUIActions> m_GUIActionsCallbackInterfaces = new List<IGUIActions>();
    private readonly InputAction m_GUI_Navigate;
    private readonly InputAction m_GUI_Validate;
    public struct GUIActions
    {
        private @Inputs m_Wrapper;
        public GUIActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_GUI_Navigate;
        public InputAction @Validate => m_Wrapper.m_GUI_Validate;
        public InputActionMap Get() { return m_Wrapper.m_GUI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GUIActions set) { return set.Get(); }
        public void AddCallbacks(IGUIActions instance)
        {
            if (instance == null || m_Wrapper.m_GUIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GUIActionsCallbackInterfaces.Add(instance);
            @Navigate.started += instance.OnNavigate;
            @Navigate.performed += instance.OnNavigate;
            @Navigate.canceled += instance.OnNavigate;
            @Validate.started += instance.OnValidate;
            @Validate.performed += instance.OnValidate;
            @Validate.canceled += instance.OnValidate;
        }

        private void UnregisterCallbacks(IGUIActions instance)
        {
            @Navigate.started -= instance.OnNavigate;
            @Navigate.performed -= instance.OnNavigate;
            @Navigate.canceled -= instance.OnNavigate;
            @Validate.started -= instance.OnValidate;
            @Validate.performed -= instance.OnValidate;
            @Validate.canceled -= instance.OnValidate;
        }

        public void RemoveCallbacks(IGUIActions instance)
        {
            if (m_Wrapper.m_GUIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGUIActions instance)
        {
            foreach (var item in m_Wrapper.m_GUIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GUIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GUIActions @GUI => new GUIActions(this);
    public interface IInGameActions
    {
        void OnMovements(InputAction.CallbackContext context);
        void OnInteractWithObjects(InputAction.CallbackContext context);
        void OnPushOtherPlayers(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnReadyToPlay(InputAction.CallbackContext context);
    }
    public interface IGUIActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnValidate(InputAction.CallbackContext context);
    }
}
