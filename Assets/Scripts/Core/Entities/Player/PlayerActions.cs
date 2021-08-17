// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Core/Entities/Player/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Minecraft.Core.Entities.Player
{
    public class @PlayerActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""58d29ddc-75c7-49fb-9dee-15f7437d7b3c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e5545541-7cb3-4e0f-a6cb-fcc3b9598337"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""dd2b0c7a-1852-481c-8ab0-fd4a5c662caa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""1d5d8f33-fe23-48a2-83c2-139365719bff"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5b4442d9-b02b-4a9e-a71a-5f061eeb1c0b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""25fafbc4-69e7-441e-80e3-22f6572f520d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5624fed7-71d2-49c8-8758-53a97741f749"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""37521df4-4de8-4cab-a579-240a215f7acb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5d9c1e2e-0fc6-4698-8dab-e882a5596bed"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Look"",
            ""id"": ""f7562dd8-2007-45b3-8998-59b91f7305ca"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""31e3a606-384e-4609-ba3b-2530400cb1e5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Value"",
                    ""id"": ""77e80916-1f49-42d6-87a1-c2d474637176"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f96c84bd-58c3-46d7-b391-50f0ac0b39db"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1c491c3-583f-4b8b-b3fa-957b72b10b25"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Movement
            m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
            m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
            m_Movement_Sprint = m_Movement.FindAction("Sprint", throwIfNotFound: true);
            // Look
            m_Look = asset.FindActionMap("Look", throwIfNotFound: true);
            m_Look_Horizontal = m_Look.FindAction("Horizontal", throwIfNotFound: true);
            m_Look_Vertical = m_Look.FindAction("Vertical", throwIfNotFound: true);
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

        // Movement
        private readonly InputActionMap m_Movement;
        private IMovementActions m_MovementActionsCallbackInterface;
        private readonly InputAction m_Movement_Move;
        private readonly InputAction m_Movement_Sprint;
        public struct MovementActions
        {
            private @PlayerActions m_Wrapper;
            public MovementActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Movement_Move;
            public InputAction @Sprint => m_Wrapper.m_Movement_Sprint;
            public InputActionMap Get() { return m_Wrapper.m_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
            public void SetCallbacks(IMovementActions instance)
            {
                if (m_Wrapper.m_MovementActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                    @Sprint.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                    @Sprint.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                    @Sprint.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                }
                m_Wrapper.m_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Sprint.started += instance.OnSprint;
                    @Sprint.performed += instance.OnSprint;
                    @Sprint.canceled += instance.OnSprint;
                }
            }
        }
        public MovementActions @Movement => new MovementActions(this);

        // Look
        private readonly InputActionMap m_Look;
        private ILookActions m_LookActionsCallbackInterface;
        private readonly InputAction m_Look_Horizontal;
        private readonly InputAction m_Look_Vertical;
        public struct LookActions
        {
            private @PlayerActions m_Wrapper;
            public LookActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Horizontal => m_Wrapper.m_Look_Horizontal;
            public InputAction @Vertical => m_Wrapper.m_Look_Vertical;
            public InputActionMap Get() { return m_Wrapper.m_Look; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(LookActions set) { return set.Get(); }
            public void SetCallbacks(ILookActions instance)
            {
                if (m_Wrapper.m_LookActionsCallbackInterface != null)
                {
                    @Horizontal.started -= m_Wrapper.m_LookActionsCallbackInterface.OnHorizontal;
                    @Horizontal.performed -= m_Wrapper.m_LookActionsCallbackInterface.OnHorizontal;
                    @Horizontal.canceled -= m_Wrapper.m_LookActionsCallbackInterface.OnHorizontal;
                    @Vertical.started -= m_Wrapper.m_LookActionsCallbackInterface.OnVertical;
                    @Vertical.performed -= m_Wrapper.m_LookActionsCallbackInterface.OnVertical;
                    @Vertical.canceled -= m_Wrapper.m_LookActionsCallbackInterface.OnVertical;
                }
                m_Wrapper.m_LookActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Horizontal.started += instance.OnHorizontal;
                    @Horizontal.performed += instance.OnHorizontal;
                    @Horizontal.canceled += instance.OnHorizontal;
                    @Vertical.started += instance.OnVertical;
                    @Vertical.performed += instance.OnVertical;
                    @Vertical.canceled += instance.OnVertical;
                }
            }
        }
        public LookActions @Look => new LookActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface IMovementActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnSprint(InputAction.CallbackContext context);
        }
        public interface ILookActions
        {
            void OnHorizontal(InputAction.CallbackContext context);
            void OnVertical(InputAction.CallbackContext context);
        }
    }
}
