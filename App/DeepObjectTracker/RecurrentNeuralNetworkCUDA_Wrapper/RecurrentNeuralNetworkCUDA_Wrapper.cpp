// This is the main DLL file.

//#include "stdafx.h"

#include "RecurrentNeuralNetworkCUDA_Wrapper.h"

using namespace RecurrentNeuralNetworkCUDA_Wrapper;

RecurrentNeuralNetworkWrapper::RecurrentNeuralNetworkWrapper()
{
	rnn = new RecurrentNeuralNetwork();
}

bool RecurrentNeuralNetworkWrapper::CheckCUDA()
{
	if (!rnn)
		return false;

	return rnn->CheckCUDA();
}

bool RecurrentNeuralNetworkWrapper::CheckCUDNN()
{
	if (!rnn)
		return false;

	return rnn->CheckCUDNN();
}