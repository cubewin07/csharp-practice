GUI: Interaction between Windows and Winforms.

4 models I need to remember and understand:
    1. Windows - the one that will render UI (in pixels) under WinForms instruction and tracks user activities (mouse and keyboard) by sending messages and which HWND get events
    2.Win32 Windowing layer - raw UI engine
    3. WinForms - the framework between our business code and Windows which handles message translation, State management, rendering cooridation for Windows and Object model 
    4.Business layer - C# code

What is WinForms?
# It is a stateful UI engine that sits between raw Windows messages and my object-oriented application code. It helps Windows render UI correctly and tell it when to re-paint (when data is updated) while being an adapter for our CODE which converts HWND(window) to Control object and WM_* to Events. 'it does more than that but those are the main function of this abstract framework'

Dive deep into Sender (HWND) and Events (WM_*):
        2 types of input route
    1. Mouse routing:
        - Events are sent window (HWND or Control) on cursor position
        * If there is a captured window, all mouse events' messages go to this window
    2. Keyboard routing:
        - Always goes to focused window/control

        Special case: Mouse capture
    # When a window has mouse capture, all mouse input messages are routed to this window, regardless of cursor position - when cursor is not in that window but the cursor position is still tracked by OS and mousemove event message still include cursor position (hit-testing by cursor position is bypassed)


How GUI works?

1. Winforms sends controls, forms, panels, ... to Windows. 
2. Windows sees those object as windows and starts painting base on the layouts given by Winforms.
3. Windows now tracks UI with window stored as (parent-child relationship)
4. Windows update focus windown and Active form by its own.
5. WinForms then starts a loop listening to Windows messages (events and HWND)

mouse input route:
    - Windowns try to find the deepest HWND on that cursor position
        - if many form are opened, Windows sort them by z-index 
        - Then, it checks whether the current cursor position at that form
            - if yes: It continues checking its children position 
                - If no children matches, HWND is the form
                - If yes, it continues this loop, until the end or no more children matches, the lastest is the HWND
            - if no: It continues on the next form with the exact same loop.

        User click L/R mouse down
        - After getting HWND, it sets focus window to this HWND
            - Send a SETFOCUS event message
            - WinForms receives, converts and then triggers Enter hanlder of that control
        - After that, it sends L/R ButtonDOWN
            - WinForms receives, converts and then triggers L/R ButtonDown handler
        - Windowns save a state for this event with:
            a. CaptureWindow
            b. DownPosition
            c. DownWindow
            d. Left/Right button down
        
        User releases L/R mouse (mouse up)
        - Windown fires mouse up event message to the window the cursor position at
        - If the postion and window matches with buttonDown
            - Click event message is fired to taht window
        
        User move mouse
        - Send movemouse event message to the window the cursor is at

        When a window is in mouse capture
            - Every mouse button up/down, left or right, or when mouse move around, everything mouse event message is fired with this captured window as HWND - the control that WinsForm triggers event handler.

        User type something (keyboard)
        - Windows sends related events' messages to focused window
            - Mouse down to a textField
            - Focused window = textField
            - A key is typed ---> that textField received events



On start, everything used objects are created and saved on memeory, then WinForms tell Windows to paint based on the layout
Windows jobs is only about tracking activies, telling which window get interacted with
WinForms is the one who tell Windows what to show and in what layout.
Therefore, when our business code add/remove/update anything, WinForms requests Windowns to re-paint (Invalidate());


Summary:
Windows renders UI + tracks activies, sending messages to WinForms based on events.
WinForms is a framework that allows Windows and Business logic to interact with each other. It also tells Windows what to show and in what way.
Business code is where user interaction is executed based on controls and events. 

Application which contains business code is where data is stored, using WinForms to interact with Windows 
        