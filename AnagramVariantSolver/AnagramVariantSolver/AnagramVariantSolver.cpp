// AnagramVariantSolver.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <algorithm>
#include <string>
#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>

void removeWhiteSpace(std::string& str)
{
	auto pos = std::find(std::begin(str), std::end(str), ' ');
	while (std::end(str) != pos)
	{
		str.erase(pos);
		pos = std::find(std::begin(str), std::end(str), ' ');
	}
}

bool isValid(std::string input, std::string result)
{
	removeWhiteSpace(input);
	removeWhiteSpace(result);
	std::transform(std::begin(input), std::end(input), std::begin(input), ::tolower);
	std::transform(std::begin(result), std::end(result), std::begin(result), ::tolower);
	std::sort(std::begin(input), std::end(input));
	std::sort(std::begin(result), std::end(result));

	return input == result;
}

void check(std::string input, std::string result)
{
	if (isValid(input, result))
	{
		std::cout << "Valid Pattern" << std::endl;
	}
	else
	{
		std::cout << "Invalid Pattern" << std::endl;
	}
}

std::vector<std::string> &split(const std::string &s, char delim, std::vector<std::string> &elems) {
	std::stringstream ss(s);
	std::string item;
	while (std::getline(ss, item, delim)) {
		elems.push_back(item);
	}
	return elems;
}

std::vector<std::string> split(const std::string &s, char delim) 
{
	std::vector<std::string> elems;
	split(s, delim, elems);
	return elems;
}

int _tmain(int argc, _TCHAR* argv[])
{
	using namespace std;

	std::ifstream istream("C:\\JudgeInput.txt", std::ios::in);

	std::string line = "";
	while (std::getline(istream, line))
	{
		auto params = split(line, '/"');
		check(params[1], params[3]);
	}

	return 0;
}

