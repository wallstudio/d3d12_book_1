#pragma once

#include "../02_SimpleTriangle/TriangleApp.h"

using namespace System;

namespace Bridge
{
	public ref class ManagedTriangleApp
	{
	private:
		void *m_native;
	public:
		ManagedTriangleApp(HWND hwnd);
	};
}
