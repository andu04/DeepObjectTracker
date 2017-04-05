#pragma once
#include "cudnn.h"

class RecurrentNeuralNetwork
{
public:
	RecurrentNeuralNetwork();
	~RecurrentNeuralNetwork();

	bool CheckCUDA();
	bool CheckCUDNN();

private:
	cudnnHandle_t cudnnHandle;
	cudnnStatus_t cudnnStatus;
};

