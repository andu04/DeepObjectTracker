#include "RecurrentNeuralNetwork.h"


RecurrentNeuralNetwork::RecurrentNeuralNetwork()
{
	cudnnStatus = cudnnCreate(&cudnnHandle);
}


RecurrentNeuralNetwork::~RecurrentNeuralNetwork()
{
	cudnnDestroy(cudnnHandle);
}

bool RecurrentNeuralNetwork::CheckCUDA()
{
	return false;
}

bool RecurrentNeuralNetwork::CheckCUDNN()
{
	return cudnnStatus == CUDNN_STATUS_SUCCESS;
}