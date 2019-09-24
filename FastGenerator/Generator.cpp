#include "generator.h"

using namespace std;

generator::generator(char** str_array, const int size)
{
	size_ = size;
	words_ = str_array;
}

list<char*> generator::check_input(const char* input) const
{
	list<char*> word_list;
	for (auto i = 0; i < size_; i++)
	{
		auto word = words_[i];
		
		if (check_word(input, word))
		{
			word_list.push_back(word);
		}
	}
	return word_list;
}

bool generator::check_word(const char* input, const char* word_to_check)
{
	list<char> sequence;
	for (auto i = 0; input[i] != 0; i++)
		sequence.push_back(input[i]);
	
	for (auto i = 0; word_to_check[i] != 0; i++)
	{
		auto element = find(sequence.begin(), sequence.end(), word_to_check[i]);
		
		if (element == sequence.end())
			return false;

		sequence.erase(element);
	}
	return true;
}
