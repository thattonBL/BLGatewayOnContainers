﻿using Grpc.Core.Interceptors;
using Grpc.Core;

namespace  GatewayGrpcService.Infrastructure
{
    public class GrpcExceptionInterceptor : Interceptor
    {
        private readonly ILogger<GrpcExceptionInterceptor> _logger;

        public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger)
        {
            _logger = logger;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var call = continuation(request, context);

            return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
        }

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> task)
        {
            try
            {
                var response = await task;
                return response;
            }
            catch (RpcException e)
            {
                _logger.LogError(e, "Error calling via gRPC: {Status}", e.Status);
                return default;
            }
        }
    }
}
