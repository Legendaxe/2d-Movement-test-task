// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControl"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""85851f6e-d0f5-47d3-90e5-e29076f43a63"",
            ""actions"": [
                {
                    ""name"": ""InputHorizontal"",
                    ""type"": ""Value"",
                    ""id"": ""bc74ffe7-2d02-4d41-a8d4-0e78bee04176"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DoubleTapLeftDash"",
                    ""type"": ""Button"",
                    ""id"": ""19495e69-3161-4508-a3ad-e8a3b25b07d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DoubleTapRightDash"",
                    ""type"": ""Button"",
                    ""id"": ""e8d2c910-b3f0-43cb-a3a6-eea6cb254808"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""capsLock"",
                    ""type"": ""Button"",
                    ""id"": ""3870e159-4462-48a6-97ca-f175dc0dce76"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OverJumpButton"",
                    ""type"": ""Button"",
                    ""id"": ""397f2f17-6df9-4a62-961f-92a8ff677d9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ObstecleInteractionKey"",
                    ""type"": ""Button"",
                    ""id"": ""8be51d10-a4aa-44b2-ab2c-6b9c72523f0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SitButton"",
                    ""type"": ""Button"",
                    ""id"": ""934e6d77-2ff9-49d7-9cc9-1aef0b02fdfc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LieButton"",
                    ""type"": ""Button"",
                    ""id"": ""60d0b2ba-a624-4aab-bc7d-0c730b605598"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RunButton"",
                    ""type"": ""Button"",
                    ""id"": ""b91f38d6-a8be-4142-8134-b68bc7335208"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AorD"",
                    ""id"": ""38071300-74db-4a19-82a2-0d9b1afb6030"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2a612000-51d2-417a-9ea3-09f4b66e95af"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9f940e61-48b2-4958-a69a-7771f78e5d86"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InputHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4ea0544d-f2bb-4483-8883-e4462831c84a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DoubleTapLeftDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5e2353d-c617-44c7-8489-b9cfa5f14236"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DoubleTapRightDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c14b7162-d2ab-4b8b-82b0-4f4cfd85f357"",
                    ""path"": ""<Keyboard>/capsLock"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""capsLock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d08bc9a4-e2b6-4746-a642-6acb1f8f6cfa"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OverJumpButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d8b6167-ecff-466a-9c6e-5fcc6643f0fa"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ObstecleInteractionKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a55797f-35ca-4069-b41d-499a0694d41b"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SitButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46fa3437-da27-49e2-b533-4f576f35872e"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LieButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a22c8f7-4876-4c50-868d-3407ea90e9ee"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RunButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_InputHorizontal = m_Player.FindAction("InputHorizontal", throwIfNotFound: true);
        m_Player_DoubleTapLeftDash = m_Player.FindAction("DoubleTapLeftDash", throwIfNotFound: true);
        m_Player_DoubleTapRightDash = m_Player.FindAction("DoubleTapRightDash", throwIfNotFound: true);
        m_Player_capsLock = m_Player.FindAction("capsLock", throwIfNotFound: true);
        m_Player_OverJumpButton = m_Player.FindAction("OverJumpButton", throwIfNotFound: true);
        m_Player_ObstecleInteractionKey = m_Player.FindAction("ObstecleInteractionKey", throwIfNotFound: true);
        m_Player_SitButton = m_Player.FindAction("SitButton", throwIfNotFound: true);
        m_Player_LieButton = m_Player.FindAction("LieButton", throwIfNotFound: true);
        m_Player_RunButton = m_Player.FindAction("RunButton", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_InputHorizontal;
    private readonly InputAction m_Player_DoubleTapLeftDash;
    private readonly InputAction m_Player_DoubleTapRightDash;
    private readonly InputAction m_Player_capsLock;
    private readonly InputAction m_Player_OverJumpButton;
    private readonly InputAction m_Player_ObstecleInteractionKey;
    private readonly InputAction m_Player_SitButton;
    private readonly InputAction m_Player_LieButton;
    private readonly InputAction m_Player_RunButton;
    public struct PlayerActions
    {
        private @InputControl m_Wrapper;
        public PlayerActions(@InputControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @InputHorizontal => m_Wrapper.m_Player_InputHorizontal;
        public InputAction @DoubleTapLeftDash => m_Wrapper.m_Player_DoubleTapLeftDash;
        public InputAction @DoubleTapRightDash => m_Wrapper.m_Player_DoubleTapRightDash;
        public InputAction @capsLock => m_Wrapper.m_Player_capsLock;
        public InputAction @OverJumpButton => m_Wrapper.m_Player_OverJumpButton;
        public InputAction @ObstecleInteractionKey => m_Wrapper.m_Player_ObstecleInteractionKey;
        public InputAction @SitButton => m_Wrapper.m_Player_SitButton;
        public InputAction @LieButton => m_Wrapper.m_Player_LieButton;
        public InputAction @RunButton => m_Wrapper.m_Player_RunButton;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @InputHorizontal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputHorizontal;
                @InputHorizontal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputHorizontal;
                @InputHorizontal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInputHorizontal;
                @DoubleTapLeftDash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDoubleTapLeftDash;
                @DoubleTapLeftDash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDoubleTapLeftDash;
                @DoubleTapLeftDash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDoubleTapLeftDash;
                @DoubleTapRightDash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDoubleTapRightDash;
                @DoubleTapRightDash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDoubleTapRightDash;
                @DoubleTapRightDash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDoubleTapRightDash;
                @capsLock.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCapsLock;
                @capsLock.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCapsLock;
                @capsLock.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCapsLock;
                @OverJumpButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOverJumpButton;
                @OverJumpButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOverJumpButton;
                @OverJumpButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOverJumpButton;
                @ObstecleInteractionKey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnObstecleInteractionKey;
                @ObstecleInteractionKey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnObstecleInteractionKey;
                @ObstecleInteractionKey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnObstecleInteractionKey;
                @SitButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSitButton;
                @SitButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSitButton;
                @SitButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSitButton;
                @LieButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLieButton;
                @LieButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLieButton;
                @LieButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLieButton;
                @RunButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunButton;
                @RunButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunButton;
                @RunButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunButton;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InputHorizontal.started += instance.OnInputHorizontal;
                @InputHorizontal.performed += instance.OnInputHorizontal;
                @InputHorizontal.canceled += instance.OnInputHorizontal;
                @DoubleTapLeftDash.started += instance.OnDoubleTapLeftDash;
                @DoubleTapLeftDash.performed += instance.OnDoubleTapLeftDash;
                @DoubleTapLeftDash.canceled += instance.OnDoubleTapLeftDash;
                @DoubleTapRightDash.started += instance.OnDoubleTapRightDash;
                @DoubleTapRightDash.performed += instance.OnDoubleTapRightDash;
                @DoubleTapRightDash.canceled += instance.OnDoubleTapRightDash;
                @capsLock.started += instance.OnCapsLock;
                @capsLock.performed += instance.OnCapsLock;
                @capsLock.canceled += instance.OnCapsLock;
                @OverJumpButton.started += instance.OnOverJumpButton;
                @OverJumpButton.performed += instance.OnOverJumpButton;
                @OverJumpButton.canceled += instance.OnOverJumpButton;
                @ObstecleInteractionKey.started += instance.OnObstecleInteractionKey;
                @ObstecleInteractionKey.performed += instance.OnObstecleInteractionKey;
                @ObstecleInteractionKey.canceled += instance.OnObstecleInteractionKey;
                @SitButton.started += instance.OnSitButton;
                @SitButton.performed += instance.OnSitButton;
                @SitButton.canceled += instance.OnSitButton;
                @LieButton.started += instance.OnLieButton;
                @LieButton.performed += instance.OnLieButton;
                @LieButton.canceled += instance.OnLieButton;
                @RunButton.started += instance.OnRunButton;
                @RunButton.performed += instance.OnRunButton;
                @RunButton.canceled += instance.OnRunButton;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnInputHorizontal(InputAction.CallbackContext context);
        void OnDoubleTapLeftDash(InputAction.CallbackContext context);
        void OnDoubleTapRightDash(InputAction.CallbackContext context);
        void OnCapsLock(InputAction.CallbackContext context);
        void OnOverJumpButton(InputAction.CallbackContext context);
        void OnObstecleInteractionKey(InputAction.CallbackContext context);
        void OnSitButton(InputAction.CallbackContext context);
        void OnLieButton(InputAction.CallbackContext context);
        void OnRunButton(InputAction.CallbackContext context);
    }
}
