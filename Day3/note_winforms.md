# GUI: How Windows and WinForms interact

---

## The four layers

Hardware

↓

Windows OS kernel      — tracks physical input, manages native windows, triggers painting

↓

Win32 API              — the public surface of Windows; MSG structs, HWNDs, CreateWindowEx

↓

WinForms               — adapter framework; maps HWNDs to Controls, WM_* to Events

↓

Business code          — your C# event handlers and application logic

**Windows OS kernel**
Tracks hardware input (mouse position, keystrokes), maintains the table of all
native windows and their screen regions, decides which HWND owns a given pixel,
and triggers repainting by posting WM_PAINT when a region is dirty.

**Win32 API**
The public interface of Windows itself — not a separate layer, but the surface
Windows exposes to programs. Packages raw input into MSG structs
{ hwnd, message, wParam, lParam, time, pt } and provides functions like
CreateWindowEx(), PostMessage(), GetMessage(), DispatchMessage().

**WinForms**
A stateful UI engine sitting between raw Windows messages and your
object-oriented code. Its main responsibilities:
- Calls CreateWindowEx() to create native windows for each control, receives
  HWNDs, and stores them in an internal HWND → Control dictionary
- Runs the message loop (GetMessage → TranslateMessage → DispatchMessage)
- Owns a WndProc that receives raw WM_* messages and translates them into
  .NET events (WM_LBUTTONDOWN → Click, WM_KEYDOWN → KeyDown, etc.)
- Tells Windows when to repaint via Invalidate(), which marks a region dirty
  and causes Windows to post WM_PAINT
- Manages focus state, z-order, and the parent-child window tree

**Business code**
Your C# application — event handlers, AppState, form logic. Never touches
Win32 directly. Sees only Control objects and .NET events.

---

## How the application starts

1. WinForms calls CreateWindowEx() for each control. Windows creates a native
   HWND for each one and returns it. WinForms stores the mapping:
   HWND 0x00A4F2C0 → Button btnSave, and so on.

2. Windows now owns a tree of native windows in parent-child relationships
   (Form → Panel → Button, etc.) and knows the screen region each one occupies.

3. Windows paints the initial UI. WinForms responds to the first WM_PAINT
   messages by telling each control to draw itself into its region.

4. Application.Run() starts the message loop. The app now sits waiting for
   input.

> Note: Not every WinForms control gets its own HWND. Lightweight controls
> like Label are windowless — they are drawn by their parent's WndProc onto
> the parent's surface. This is why Labels cannot receive focus or keyboard
> messages — there is no HWND to route them to.

---

## Two input routes

### Mouse — routed by cursor position

Windows finds the target HWND by hit-testing:

1. All open forms are sorted by z-order (frontmost first)
2. For the top form, Windows checks whether the cursor is inside its bounds
3. If yes, it recursively checks each child control
4. It keeps descending until no child matches — the deepest match is the HWND
5. If the top form does not contain the cursor, Windows moves to the next form
   and repeats

### Keyboard — routed by focus

No hit-testing. Keystrokes always go to the focused HWND. There is exactly
one focused control at any time. The OS delivers the keystroke there directly.

---

## Mouse event sequence in detail

### User presses left/right mouse button (down)

1. Windows hit-tests to find the deepest HWND at the cursor position
2. Windows sends WM_KILLFOCUS to the previously focused window
   → WinForms fires Leave on the old control
3. Windows sends WM_SETFOCUS to the new HWND
   → WinForms fires Enter on the new control
4. Windows saves state:
   - CaptureWindow (none yet)
   - DownPosition (cursor coordinates)
   - DownWindow (the HWND that received the click)
   - Button state (left or right down)
5. Windows sends WM_LBUTTONDOWN (or WM_RBUTTONDOWN) to that HWND
   → WinForms fires MouseDown on the control

### User releases the button (up)

1. Windows sends WM_LBUTTONUP to the window at the current cursor position
   → WinForms fires MouseUp
2. If the current cursor position is still within the same window that received
   ButtonDown, Windows sends WM_LBUTTONCLK
   → WinForms fires Click

### User moves the mouse

- Windows sends WM_MOUSEMOVE to the window at the current cursor position
  → WinForms fires MouseMove

---

## Special case: Mouse capture

When a control captures the mouse (e.g. during a drag), all mouse messages —
ButtonDown, ButtonUp, MouseMove — are routed to that captured HWND regardless
of where the cursor is. Hit-testing is bypassed entirely.

The cursor position reported in the message is still accurate — Windows always
tracks real cursor coordinates. Only the routing target changes.

WinForms sets mouse capture automatically on MouseDown for certain operations,
or you can do it manually:

```csharp
control.Capture = true;   // capture starts
control.Capture = false;  // capture released
```

---

## Repainting

When your business code changes data that affects the UI, WinForms does not
immediately redraw. Instead:

1. You (or WinForms internally) call control.Invalidate()
2. WinForms marks that control's screen region as dirty
3. Windows posts a WM_PAINT message for that region
4. WinForms receives WM_PAINT and fires the control's OnPaint method
5. Only the dirty region is redrawn — not the entire screen

This deferred, region-based approach keeps rendering efficient. Multiple
Invalidate() calls in the same frame are batched into one WM_PAINT.

---

## Summary

| Layer         | Owns                                              | Does not own                        |
|---------------|---------------------------------------------------|-------------------------------------|
| Windows kernel| Hardware input, native window table, painting     | .NET objects, events, focus logic   |
| Win32 API     | MSG structs, HWNDs, system functions              | Control concepts, event translation |
| WinForms      | HWND↔Control map, event translation, repaint flow | Raw input, pixel rendering          |
| Business code | Application logic, data, event handlers           | Anything UI or OS related           |

Windows renders UI and tracks activity, sending messages to WinForms.
WinForms translates those messages into .NET objects and events, and tells
Windows what to show and where.
Business code responds to events and updates data — it never speaks to Windows
directly.