//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/InputPlayer.inputactions
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

public partial class @InputPlayer : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputPlayer()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputPlayer"",
    ""maps"": [
        {
            ""name"": ""PlayerBase"",
            ""id"": ""cf541f2a-bf08-4aaf-94ea-2f748a826ddc"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""a5dcb757-d8c8-4627-a208-08eee903bed7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TopDownMove"",
                    ""type"": ""Button"",
                    ""id"": ""01d874c4-c0e5-452d-9fea-f8773ebdb006"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftRightMove"",
                    ""type"": ""Button"",
                    ""id"": ""80739d97-16d3-46db-83c2-4132c9fea286"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""85727f75-f30c-4442-a3a8-47022d2399c6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f495b09e-fb61-44e7-bd85-06cc30a02c3f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Top Down"",
                    ""id"": ""c8b6c1e0-0991-4c15-a65d-5c7224d1d3f9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TopDownMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d0950168-76a3-4f54-975a-c6184c4825dc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TopDownMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7c577789-a236-4d25-8693-6119368f99df"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TopDownMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Top Down GamePad"",
                    ""id"": ""1df1c96b-e723-47cf-b550-5625baa42352"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TopDownMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""26d581ec-d334-4b03-8c17-ee157e8e01f9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TopDownMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f34655a9-d0b4-4c3c-a0f2-5a2993b597d9"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TopDownMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftRight"",
                    ""id"": ""eec19433-55ba-494a-8805-1421c457d43d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftRightMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8b0dc61b-a44d-4444-a3a6-eded741cea34"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftRightMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2a0a0de8-d4f4-412a-85fa-092181312335"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftRightMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftRight Gamepad"",
                    ""id"": ""40efc3a2-b189-489b-96b5-24138fcb9b8c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftRightMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8c71df42-d604-43fb-b0df-742376f2b856"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftRightMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""120f8cc0-df82-498f-a091-0f22fdc6e324"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftRightMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerBase
        m_PlayerBase = asset.FindActionMap("PlayerBase", throwIfNotFound: true);
        m_PlayerBase_Movement = m_PlayerBase.FindAction("Movement", throwIfNotFound: true);
        m_PlayerBase_TopDownMove = m_PlayerBase.FindAction("TopDownMove", throwIfNotFound: true);
        m_PlayerBase_LeftRightMove = m_PlayerBase.FindAction("LeftRightMove", throwIfNotFound: true);
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

    // PlayerBase
    private readonly InputActionMap m_PlayerBase;
    private IPlayerBaseActions m_PlayerBaseActionsCallbackInterface;
    private readonly InputAction m_PlayerBase_Movement;
    private readonly InputAction m_PlayerBase_TopDownMove;
    private readonly InputAction m_PlayerBase_LeftRightMove;
    public struct PlayerBaseActions
    {
        private @InputPlayer m_Wrapper;
        public PlayerBaseActions(@InputPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerBase_Movement;
        public InputAction @TopDownMove => m_Wrapper.m_PlayerBase_TopDownMove;
        public InputAction @LeftRightMove => m_Wrapper.m_PlayerBase_LeftRightMove;
        public InputActionMap Get() { return m_Wrapper.m_PlayerBase; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerBaseActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerBaseActions instance)
        {
            if (m_Wrapper.m_PlayerBaseActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnMovement;
                @TopDownMove.started -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnTopDownMove;
                @TopDownMove.performed -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnTopDownMove;
                @TopDownMove.canceled -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnTopDownMove;
                @LeftRightMove.started -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnLeftRightMove;
                @LeftRightMove.performed -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnLeftRightMove;
                @LeftRightMove.canceled -= m_Wrapper.m_PlayerBaseActionsCallbackInterface.OnLeftRightMove;
            }
            m_Wrapper.m_PlayerBaseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @TopDownMove.started += instance.OnTopDownMove;
                @TopDownMove.performed += instance.OnTopDownMove;
                @TopDownMove.canceled += instance.OnTopDownMove;
                @LeftRightMove.started += instance.OnLeftRightMove;
                @LeftRightMove.performed += instance.OnLeftRightMove;
                @LeftRightMove.canceled += instance.OnLeftRightMove;
            }
        }
    }
    public PlayerBaseActions @PlayerBase => new PlayerBaseActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerBaseActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnTopDownMove(InputAction.CallbackContext context);
        void OnLeftRightMove(InputAction.CallbackContext context);
    }
}
