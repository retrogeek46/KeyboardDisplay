using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using UnityEngine;

public class Keylogger3D : MonoBehaviour {

	private static int WH_KEYBOARD_LL = 13;
	private static int WM_KEYDOWN = 0x0100;
	private static int WM_KEYUP = 0x0101;
	private static int WM_SYSKEYDOWN = 0x0104;
	private static int WM_SYSKEYUP = 0x0105;
	private static IntPtr hook = IntPtr.Zero;
	private static LowLevelKeyboardProc llkProcedure = HookCallback;

	// Use this for initialization
	void Start () {
		Hook();
	}

	private delegate IntPtr LowLevelKeyboardProc (int nCode, IntPtr wParam, IntPtr lParam);

	private static IntPtr HookCallback (int nCode, IntPtr wParam, IntPtr lParam) {
		try {
			//regular keys
			if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN) {
				int vkCode = Marshal.ReadInt32(lParam);
				//UnityEngine.Debug.Log("pressed : " + (Keys)vkCode);
				KeyHandler3D.PressKey(((Keys)vkCode).ToString(), true);
			}
			if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP) {
				int vkCode = Marshal.ReadInt32(lParam);
				//UnityEngine.Debug.Log("pressed : " + (Keys)vkCode);
				KeyHandler3D.PressKey(((Keys)vkCode).ToString(), false);
			}
			//alt key
			if (nCode >= 0 && wParam == (IntPtr)WM_SYSKEYDOWN) {
				int vkCode = Marshal.ReadInt32(lParam);
				//UnityEngine.Debug.Log("pressed : " + (Keys)vkCode);
				KeyHandler3D.PressKey(((Keys)vkCode).ToString(), true);
			}
			if (nCode >= 0 && wParam == (IntPtr)WM_SYSKEYUP) {
				int vkCode = Marshal.ReadInt32(lParam);
				//UnityEngine.Debug.Log("pressed : " + (Keys)vkCode);
				KeyHandler3D.PressKey(((Keys)vkCode).ToString(), false);
			}
			return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
		} catch (Exception ex) {
            UnityEngine.Debug.Log(ex);
            Unhook();
			Hook();
			return CallNextHookEx(IntPtr.Zero, 0, wParam, lParam);
		}
	}

	private static IntPtr SetHook (LowLevelKeyboardProc proc) {
		Process currentProcess = Process.GetCurrentProcess();
		ProcessModule currentModule = currentProcess.MainModule;
		string moduleName = currentModule.ModuleName;
		IntPtr moduleHandle = GetModuleHandle(moduleName);
		return SetWindowsHookEx(WH_KEYBOARD_LL, llkProcedure, moduleHandle, 0);
	}

	[DllImport("user32.dll")]
	private static extern IntPtr CallNextHookEx (IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
	[DllImport("user32.dll")]
	private static extern IntPtr SetWindowsHookEx (int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
	[DllImport("user32.dll")]
	private static extern bool UnhookWindowsHookEx (IntPtr hhk);
	[DllImport("kernel32.dll")]
	private static extern IntPtr GetModuleHandle (String lpModuleName);

	private void OnDestroy () {
		Unhook();
	}

	public static void Unhook () {
		UnhookWindowsHookEx(hook);
	}

	public static void Hook () {
		hook = SetHook(llkProcedure);
	}
}


