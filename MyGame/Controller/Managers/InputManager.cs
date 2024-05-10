using System.Runtime.CompilerServices;

namespace MyGame.Controller.Managers;

class InputManager {
    private static InputManager _instance;
    private KeyboardState currentKeyState, prevKeyState;

    public static InputManager Instance {
        get {
            if(_instance == null)
                _instance = new InputManager();
            return _instance;
        }
    }

    public void Update() {
        prevKeyState = currentKeyState;
        if(!ScreenManager.Instance.IsTransitioning)
            currentKeyState = Keyboard.GetState();
    }

    public bool KeyPressed(params Keys[] keys) {
        foreach(var key in keys) {
            if(currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key)) {
                return true;
            }
        }
        return false;
    }

    public bool KeyDown(params Keys[] keys) {
        foreach(var key in keys) {
            if(currentKeyState.IsKeyDown(key)) {
                return true;
            }
        }
        return false;
    }

    public bool KeyReleased(params Keys[] keys) {
        foreach(var key in keys) {
            if(currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key)) {
                return true;
            }
        }
        return false;
    }
}
