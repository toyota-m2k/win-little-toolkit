using System;
using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace io.github.toyota32k.toolkit.winui.utils;

public class WinPlacement {
    #region Win32ÉfÅ[É^å^

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom) {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT {
        public int X;
        public int Y;

        public POINT(int x, int y) {
            X = x;
            Y = y;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT : IEquatable<WINDOWPLACEMENT> {
        public int length;
        public int flags;
        public int showCmd;
        public POINT minPosition;
        public POINT maxPosition;
        public RECT normalPosition;

        public bool Equals(WINDOWPLACEMENT other) {
            return length == other.length &&
                   flags == other.flags &&
                   showCmd == other.showCmd &&
                   minPosition.Equals(other.minPosition) &&
                   maxPosition.Equals(other.maxPosition) &&
                   normalPosition.Equals(other.normalPosition);
        }

        public override bool Equals(object obj) {
            return obj is WINDOWPLACEMENT other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(length, flags, showCmd, minPosition, maxPosition, normalPosition);
        }

        public static bool operator ==(WINDOWPLACEMENT left, WINDOWPLACEMENT right) {
            return left.Equals(right);
        }

        public static bool operator !=(WINDOWPLACEMENT left, WINDOWPLACEMENT right) {
            return !(left == right);
        }
    }

    #endregion

    #region Win32 API declarations to set and get window placement

    [DllImport("user32.dll")]
    static extern bool SetWindowPlacement(nint hWnd, [In] ref WINDOWPLACEMENT lpWndPl);

    [DllImport("user32.dll")]
    static extern bool GetWindowPlacement(nint hWnd, out WINDOWPLACEMENT lpWndPl);

    const int SW_SHOWNORMAL = 1;
    const int SW_SHOWMINIMIZED = 2;

    #endregion

    public static WINDOWPLACEMENT GetWindowPlacement(Window w) {
        nint hWnd = WindowNative.GetWindowHandle(w);
        GetWindowPlacement(hWnd, out WINDOWPLACEMENT wp);
        return wp;
    }

    private static void SetWindowPlacement(Window w, WINDOWPLACEMENT wp) {
        wp.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
        wp.flags = 0;
        wp.showCmd = wp.showCmd == SW_SHOWMINIMIZED ? SW_SHOWNORMAL : wp.showCmd;
        nint hWnd = WindowNative.GetWindowHandle(w);
        SetWindowPlacement(hWnd, ref wp);
    }

    public bool HasValue { get; set; }

    public WINDOWPLACEMENT Placement { get; set; }

    public WinPlacement() {
        Placement = new WINDOWPLACEMENT();
        HasValue = false;
    }

    public void GetPlacementFrom(Window w) {
        var wp = GetWindowPlacement(w);
        if (Placement != wp) {
            HasValue = true;
            Placement = wp;
        }
    }

    public void ApplyPlacementTo(Window w) {
        if (HasValue) {
            SetWindowPlacement(w, Placement);
        }
    }
}
