#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport) 
#else
#define DLL_API __declspec(dllimport) 
#endif


#include <string>
using namespace std;

namespace DLL {
	class MyDLL {
	public:
		static DLL_API string GetGreeting();
	};
}