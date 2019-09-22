#include "generator.h"

using namespace std;

generator::generator(char** str_array, const int size)
{
	size_ = size;
	words_ = str_array;
}

generator::~generator()
{
	delete words_;
	delete this;
}

char** generator::check_input(const string& input) const
{
	node* return_words = nullptr;
	auto node_length = 0;
	for (auto i = 0; i < size_; i++)
	{
		if (check_word(input, words_[i]))
		{
			return_words = new node(words_[i]);
			return_words = return_words->next;
			node_length++;
		}
	}
	const auto words = new char*[node_length];
	auto current_index = 0;
	while (return_words != nullptr)
	{
		words[current_index] = return_words->data;
		return_words = return_words->previous;
		delete return_words->next;
		current_index++;
	}
	return words;
}

bool generator::check_word(const string& input, const string& word_to_check)
{
	auto *sequence = const_cast<char*>(input.c_str());

	const int sequence_len = input.length();

	const int word_to_check_len = word_to_check.length();
	
	for (auto i = 0; i < word_to_check_len; i++)
	{
		for (auto j = 0; j < sequence_len; j++)
		{
			if (sequence[j] != word_to_check[i])
			{
				return false;
			}
			sequence[j] = 0;
		}
	}
	return true;
}

node::node(char* data)
{
	this->data = data;
}
