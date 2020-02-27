#pragma once
#include"TriangleApp.h"

extern "C" {
    __declspec(dllexport) TriangleApp* Init(HWND hwnd, int width, int height);
    __declspec(dllexport) void Render(TriangleApp* app);
    __declspec(dllexport) void Dispose(TriangleApp* app);
    __declspec(dllexport) void Test();
}