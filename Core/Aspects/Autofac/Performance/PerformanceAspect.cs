using System;
using System.Diagnostics;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4net;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private readonly Stopwatch _stopwatch;
        private readonly int _interval;
        private readonly LoggerServiceBase _loggerServiceBase;

        public PerformanceAspect(int interval,Type loggerService)
        {
            _interval = interval;
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase) Activator.CreateInstance(loggerService);
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                _loggerServiceBase.Warn($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }

            _stopwatch.Reset();
        }
    }
}
