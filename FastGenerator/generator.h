#pragma once

#include <list>
using namespace std;

class generator
{
	char** words_ = nullptr;
	int size_;
public:
	generator(char** str_array, int size);
	list<char*> check_input(const char* input) const;
private:
	static bool check_word(const char* input, const char* word_to_check);
};

extern "C" {
	__declspec(dllexport) inline generator* CreateGenerator(char** str_array, const int size)
	{
		return new generator(str_array, size);
	}
}

extern "C" {
	__declspec(dllexport) inline char** CallCheckInput(generator* p_object, const char* input, int* count)
	{
		if(p_object != nullptr)
		{
			auto word_list = p_object->check_input(input);

			*count = word_list.size();

			const auto word_arr = new char*[*count];

			copy(word_list.begin(), word_list.end(), word_arr);

			return word_arr;
			
		}
		return nullptr;
	}
}

extern "C" {
	__declspec(dllexport) inline void DisposeGenerator(generator* p_object)
	{
		if(p_object != nullptr)
		{
			delete p_object;
			p_object = nullptr;
		}
	}
}