using System;
using MassTransit;
using SMTRMQ.Core;

namespace SMTRMQ.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.UseRabbitMqRouting();
                sbc.ReceiveFrom("rabbitmq://localhost/program");
                sbc.Subscribe(subs =>
                {
                    subs.Handler<WebMessage>(msg =>
                    {
                        Console.WriteLine(msg.Text);
                        Bus.Instance.Publish(new ProgramMessage { Text = string.Format("echo from app: {0}", msg.Text) });
                    });

                });
            });

            Console.ReadLine();
        }
    }
}
