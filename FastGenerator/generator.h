#pragma once

#include <string>
using namespace std;

class generator
{
	string input_;
	char** words_ = nullptr;
	int size_;
public:
	generator(char** str_array, int size);
	virtual ~generator();
	char** check_input(const string& input) const;
private:
	static bool check_word(const string& input, const string& word_to_check);
};

struct node
{
	char* data;
	node * previous = nullptr;
    node * next = nullptr;
	explicit node(char* data);
};

extern "C" {
	__declspec(dllexport) inline generator* CreateGenerator(char** str_array, const int size)
	{
		return new generator(str_array, size);
	}
}

extern "C" {
	__declspec(dllexport) inline char** CallCheckInput(generator* p_object, const string& input)
	{
		if(p_object != nullptr)
		{
			return p_object->check_input(input);
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