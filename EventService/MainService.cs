using Common.MessageQueue;
using EventService.DBContexts;
using EventService.MessageQueue;
using EventService.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Common.DependencyInjection.Utilities;

namespace EventService
{
    public class MainService : IHostedService
    {
        QueueListener listener;
        IHostApplicationLifetime appLifetime;
        private readonly string queueName;
        private IMessageQueue messageQueue;
        private IPersonRepository _personRepository;
        private IEventRepository _eventRepository;
        private readonly EventServiceContext _eventServiceContext;

        public MainService(IHostApplicationLifetime appLifetime, IServiceProvider serviceProvider)
        {
            queueName = "PersonQueue";
            this.appLifetime = appLifetime;
            messageQueue = DependencyInjection.GetService<IMessageQueue>();
            _eventServiceContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<EventServiceContext>();
            _personRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IPersonRepository>();
            _eventRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IEventRepository>();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.appLifetime.ApplicationStarted.Register(OnStarted);
            this.appLifetime.ApplicationStopping.Register(OnStopping);
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private void OnStarted()
        {
            StartService();
        }

        private void OnStopping()
        {
            StopService();
        }

        private void StartService()
        {
            listener = new QueueListener(queueName, messageQueue, _personRepository, _eventRepository);
            messageQueue.ServiceName = "PersonQueue";
            listener.StartListener();
        }
        private void StopService()
        {
            if (listener != null)
                listener.StopListener();
        }

    }
}

