// RecurrentNeuralNetworkCUDA_Wrapper.h

#pragma once
#include "RecurrentNeuralNetwork.h"

using namespace System;

namespace RecurrentNeuralNetworkCUDA_Wrapper {

	public ref class RecurrentNeuralNetworkWrapper
	{
	public:
		RecurrentNeuralNetworkWrapper();

		bool CheckCUDA();
		bool CheckCUDNN();

	private:
		RecurrentNeuralNetwork* rnn;
	};
}
