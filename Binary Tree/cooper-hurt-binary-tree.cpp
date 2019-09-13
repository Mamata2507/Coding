// Copyright 2019, Bradley Peterson, Weber State University, all rights reserved. (4/2019)

#include <iostream>
#include <string>
#include <stack>
#include <sstream>
#include <cmath>

//To prevent those using g++ from trying to use a library
//they don't have
#ifndef __GNUC__
#include <conio.h>
#else
#include <stdio.h>
#endif

using std::stack;
using std::istringstream;
using std::ostringstream;
using std::string;
using std::cout;
using std::cerr;
using std::endl;
using std::pow;

struct Node {
public:
	string data{};
	Node* llink{ nullptr }; //may not be neccessary
	Node* rlink{ nullptr }; //may not be necessary
};

class TreeParser {
private:
	Node* root{ nullptr };
	stack<string> mathStack;
	double castStrToDouble(string const &s);
	string castDoubleToStr(const double d);
	void initialize();
	bool isDigit(char c);
	bool isOperator(char c);
	void processExpression(Node* p);
	void computeAnswer(Node* p);

	void inOrderTraversal(Node* ptr) const;
	void postOrderTraversal(Node* ptr) const;
protected:
	string expression;
	int position;
public:
	TreeParser();
	void displayParseTree();
	void processExpression(string &expression);
	string computeAnswer();

	void inOrderTraversal() const;
	void postOrderTraversal() const;
};

void TreeParser::inOrderTraversal() const {
	inOrderTraversal(root);
	cout << endl;
}

void TreeParser::inOrderTraversal(Node * ptr) const {
	if (ptr) {
		inOrderTraversal(ptr->llink);
		cout << ptr->data << " ";
		inOrderTraversal(ptr->rlink);
	}
}

void TreeParser::postOrderTraversal() const {
	postOrderTraversal(root);
	cout << endl;
}

void TreeParser::postOrderTraversal(Node * ptr) const {
	if (ptr) {
		postOrderTraversal(ptr->llink);
		postOrderTraversal(ptr->rlink);
		cout << ptr->data << " ";
	}
}

//This is the private method
void TreeParser::processExpression(Node * p) {
	while (this->position < this->expression.length()) {


		if (this->expression[this->position] == '(') {
			Node * tmp = new Node();
			p->llink = new Node();
			this->position++;
			processExpression(p->llink);
		}
		else if (isDigit(this->expression[this->position])) {
			while (isDigit(this->expression[this->position])) {
				p->data += this->expression[this->position];
				this->position++;
			}
			return;
		}
		else if (isOperator(this->expression[this->position])) {
			p->data = this->expression[this->position];
			this->position++;

			p->rlink = new Node();
			processExpression(p->rlink);
		}
		else if (this->expression[this->position] == ')') {
			this->position++;
			return;
		}
		else if (this->expression[this->position] == ' ') {
			this->position++;
		}

	}
}

//This is the public method
void TreeParser::processExpression(string & expression) {
	if (expression.length() > 0) {
		this->expression = expression;
		this->position = 0;
		Node* rootNode = new Node;
		this->root = rootNode;
		processExpression(rootNode);
	}
	return;
}

bool TreeParser::isOperator(char c) {
	if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^') {
		return true;
	}

	return false;
}

bool TreeParser::isDigit(char c) {
	if (isdigit(c)) {
		return true;
	}
	else {
		return false;
	}
}

/*The first line gets the recursion started. 
The second line grabs the last item on mathStack, which is the answer, and returns it. 
The privatecomputeAnswer() handles the recursion.  
*/
string TreeParser::computeAnswer() {
	computeAnswer(root);
	return mathStack.top();
}

void TreeParser::computeAnswer(Node* p) {
	if (p != NULL) {
		computeAnswer(p->llink);
		computeAnswer(p->rlink);
		if (isOperator(p->data[0]))
		{
			double A = castStrToDouble(mathStack.top());
			mathStack.pop();

			double B = castStrToDouble(mathStack.top());
			mathStack.pop();

			//though order does not matter it is in order of PEMDAS
			switch (p->data[0]) {
				case '^':
					mathStack.push(castDoubleToStr((pow(B,A))));
					break;
				case '*':
					mathStack.push(castDoubleToStr((B*A)));
					break;
				case '/':
					mathStack.push(castDoubleToStr((B/A)));
					break;
				case '+':
					mathStack.push(castDoubleToStr((B+A)));
					break;
				case '-':
					mathStack.push(castDoubleToStr((B-A)));
					break;
			}
		}
		else {
			mathStack.push(p->data);
		}
	}


}

void TreeParser::initialize() {
	expression = "";
	position = 0;
	while (!mathStack.empty()) {
		mathStack.pop();
	}
}

double TreeParser::castStrToDouble(const string &s) {
	istringstream iss(s);
	double x;
	iss >> x;
	return x;
}

string TreeParser::castDoubleToStr(const double d) {
	ostringstream oss;
	oss << d;
	return oss.str();

}

TreeParser::TreeParser() {
	initialize();
}


void TreeParser::displayParseTree() {
	cout << "The expression seen using in-order traversal: ";
	inOrderTraversal();
	cout << endl;
	cout << "The expression seen using post-order traversal: ";
	postOrderTraversal();
	cout << endl;

}

void pressAnyKeyToContinue() {
	printf("Press any key to continue\n");

	//Linux and Mac users with g++ don't need this
	//But everyone else will see this message.
#ifndef __GNUC__
	_getch();
#else
	int c;
	fflush(stdout);
	do c = getchar(); while ((c != '\n') && (c != EOF));
#endif

}

// Copyright 2019, Bradley Peterson, Weber State University, all rights reserved. (4/2019)

int main() {

	TreeParser *tp = new TreeParser;

	string expression = "(4+7)";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display 11 as a double output

	expression = "(7-4)";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display 3 as a double output

	expression = "(9*5)";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display 45 as a double output

	expression = "(4^3)";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display 64 as a double output

	expression = "((2-5)-5)";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display -8 as a double output

	expression = "(5*(6/2))";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display 15 as a double output

	expression = "((1 + 2) * (3 + 4))";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display 21 as a double output

	expression = "(543+321)";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display 864 as a double output

	expression = "((5*(3+2))+(7*(4+6)))";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display 95 as a double output

	expression = "(((((3+12)-7)*120)/(2+3))^3)";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display close to 7077888 as a double output 
															//NOTE, it won't be exact, it will display as scientific notation!

	expression = "(((((9+(2*(110-(30/2))))*8)+1000)/2)+(((3^4)+1)/2))";
	tp->processExpression(expression);
	tp->displayParseTree();
	cout << "The result is: " << tp->computeAnswer() << endl; //Should display close to 1337 as a double/decimal output

	pressAnyKeyToContinue();
	return 0;
}
