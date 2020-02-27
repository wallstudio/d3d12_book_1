#include <windows.h>
#include <stdexcept>
#include "../02_SimpleTriangle/TriangleApp.h"

HWND button;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wp, LPARAM lp) {
	switch (msg) {
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	case WM_COMMAND:
		MessageBox(hwnd, TEXT("Kitty on you lap"), TEXT(""), MB_OK);
		return 0;
	case WM_RBUTTONDOWN:
		SendMessage(button, BM_CLICK, 0, 0);
		return 0;
	case WM_KEYDOWN:
		SendMessage(button, BM_SETSTATE, TRUE, 0);
		return 0;
	case WM_KEYUP:
		SendMessage(button, BM_SETSTATE, FALSE, 0);
		return 0;
	}
	return DefWindowProc(hwnd, msg, wp, lp);
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
	PSTR lpCmdLine, int nCmdShow) {
	HWND hwnd;
	MSG msg;
	WNDCLASS winc;

	winc.style = CS_HREDRAW | CS_VREDRAW;
	winc.lpfnWndProc = WndProc;
	winc.cbClsExtra = winc.cbWndExtra = 0;
	winc.hInstance = hInstance;
	winc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	winc.hCursor = LoadCursor(NULL, IDC_ARROW);
	winc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	winc.lpszMenuName = NULL;
	winc.lpszClassName = TEXT("KITTY");

	if (!RegisterClass(&winc)) return -1;

	hwnd = CreateWindow(
		TEXT("KITTY"), TEXT("Kitty on your lap"),
		WS_OVERLAPPEDWINDOW | WS_VISIBLE,
		CW_USEDEFAULT, CW_USEDEFAULT,
		CW_USEDEFAULT, CW_USEDEFAULT,
		NULL, NULL, hInstance, NULL
	);

	button = CreateWindow(
		TEXT("BUTTON"), TEXT("Kitty"),
		WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
		0, 0, 100, 30,
		hwnd, NULL, hInstance, NULL
	);

	// DirectX12 Init
	TriangleApp theApp{};
	theApp.Initialize(button);
	//SetWindowLongPtr(button, GWLP_USERDATA, reinterpret_cast<LONG_PTR>(&theApp));

	if (hwnd == NULL) return -1;

	while (GetMessage(&msg, NULL, 0, 0))
	{
		DispatchMessage(&msg);
		theApp.Render();
	}

	theApp.Cleanup();
	return msg.wParam;
}