#include "Export.h"

TriangleApp* Init(HWND hwnd, int width, int height)
{
	auto app = new TriangleApp();
	app->Initialize(hwnd, width, height);
	return app;
}

void Render(TriangleApp* app)
{
	app->Render();
}

void Dispose(TriangleApp* app)
{
	app->Cleanup();
	delete app;
}

void Test()
{
	printf("Test");
}