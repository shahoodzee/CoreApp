//using MassTransit;
//using Module.Call.Core.Consumer;
//using Module.Email.Core.Consumer;
//using Module.Flow.Core.Handlers;
//using Module.Notification.Core.Consumer;
//using Module.Package.Core.Consumer;
//using Module.Payment.Core.Consumer;
//using Module.Ticket.Core.Consumer;
//using Module.User.Core.Consumer;
//using Shared.Models.Contracts;

//namespace ManagexAPI.Configurations;

//public static class MTConfiguration
//{
//    public static IServiceCollection AddMessageConfiguration(this IServiceCollection services)
//    {
//        services.AddMassTransit(config =>
//        {

//            config.AddConsumer<GetSingleProcessFlowCommandHandler>();
//            config.AddConsumer<TicketCreationConsumer>();
//            config.AddConsumer<CallRequestCreationConsumer>();
//            config.AddConsumer<ForwardCallRequestConsumer>();
//            config.AddConsumer<AcceptCallConsumer>();
//            config.AddConsumer<OnConnectedConsumer>();
//            config.AddConsumer<OnDisconnectedConsumer>();
//            config.AddConsumer<GetPackageByIdConsumer>();
//            config.AddConsumer<GetUserConsumer>();
//            config.AddConsumer<GetUsersByRoleConsumer>();
//            config.AddConsumer<WaitingCallsCountConsumer>();
//            config.AddConsumer<AccountVerificationEmailConsumer>();
//            config.AddConsumer<GetCCAndESUsersConsumer>();
//            config.AddConsumer<GetUsersByIdsConsumer>();
//            config.AddConsumer<GetUsersByEmailConsumer>();
//            config.AddConsumer<SubTaskAssignedEmailConsumer>();
//            config.AddConsumer<DeductHoursConsumer>();
//            config.AddConsumer<SubTaskCreationConsumer>();
//            config.AddConsumer<AddToCallConsumer>();
//            config.AddConsumer<RemoveCallTimerConsumer>();
//            config.AddConsumer<CallRequestAcknowledgedConsumer>();
//            config.AddConsumer<ForgetPasswordEmailConsumer>();
//            config.AddConsumer<SubTaskApprovalConsumer>();
//            config.AddConsumer<SubTaskRejectionConsumer>();
//            config.AddConsumer<SubTaskInProgressConsumer>();
//            config.AddConsumer<SubTaskResolvedConsumer>();
//            config.AddConsumer<SubTaskAssigneeChangedConsumer>();
//            config.AddConsumer<ServiceRequestInReviewConsumer>();
//            config.AddConsumer<ServiceRequestEscalatedConsumer>();
//            config.AddConsumer<ServiceRequestClosedConsumer>();
//            config.AddConsumer<ServiceRequestCreationEmailConsumer>();
//            config.AddConsumer<SubTaskCreationEmailConsumer>();
//            config.AddConsumer<SubTaskResolvedEmailConsumer>();
//            config.AddConsumer<ServiceRequestInReviewEmailConsumer>();
//            config.AddConsumer<ServiceRequestEscalatedEmailConsumer>();
//            config.AddConsumer<ServiceRequestClosedEmailConsumer>();
//            config.AddConsumer<RemoveCallRecordConsumer>();
//            config.AddConsumer<FetchSRAssociatedUsersConsumer>();
//            config.AddConsumer<CommentAddedEmailConsumer>();
//            config.AddConsumer<ReplyAddedEmailConsumer>();
//            config.AddConsumer<CommentAddedConsumer>();
//            config.AddConsumer<ReplyAddedConsumer>();
//            config.AddConsumer<UpdateUserStatusConsumer>();
//            config.AddConsumer<UpdateLiveUserStatusConsumer>();
//            config.AddConsumer<DirectCallCreationConsumer>();
//            config.AddConsumer<NotificationConsumer>();
//            config.UsingRabbitMq((context, cfg) =>
//            {
//                cfg.Host(new Uri("rabbitmq://cloudsupport.vaporvm.com:5672"), h =>
//                {
//                    h.Username("rabbitadmin");
//                    h.Password("4r7K+fre2ekIp");
//                });

//                //cfg.Host(new Uri("rabbitmq://localhost"), h =>
//                //{
//                //    h.Username("guest");
//                //    h.Password("guest");
//                //});

//                // Configure endpoints where the consumer will listen               
//                cfg.ReceiveEndpoint("Ticket-Flow-Queue", e =>
//                {
//                    e.ConfigureConsumer<GetSingleProcessFlowCommandHandler>(context);
//                    e.ConfigureConsumer<TicketCreationConsumer>(context);
//                    e.ConfigureConsumer<CallRequestCreationConsumer>(context);
//                    e.ConfigureConsumer<ForwardCallRequestConsumer>(context);
//                    e.ConfigureConsumer<AcceptCallConsumer>(context);
//                    e.ConfigureConsumer<OnConnectedConsumer>(context);
//                    e.ConfigureConsumer<OnDisconnectedConsumer>(context);
//                    e.ConfigureConsumer<GetPackageByIdConsumer>(context);
//                    e.ConfigureConsumer<GetUserConsumer>(context);
//                    e.ConfigureConsumer<GetUsersByRoleConsumer>(context);
//                    e.ConfigureConsumer<WaitingCallsCountConsumer>(context);
//                    e.ConfigureConsumer<AccountVerificationEmailConsumer>(context);
//                    e.ConfigureConsumer<GetCCAndESUsersConsumer>(context);
//                    e.ConfigureConsumer<GetUsersByIdsConsumer>(context);
//                    e.ConfigureConsumer<GetUsersByEmailConsumer>(context);
//                    e.ConfigureConsumer<SubTaskAssignedEmailConsumer>(context);
//                    e.ConfigureConsumer<DeductHoursConsumer>(context);
//                    e.ConfigureConsumer<SubTaskCreationConsumer>(context);
//                    e.ConfigureConsumer<AddToCallConsumer>(context);
//                    e.ConfigureConsumer<RemoveCallTimerConsumer>(context);
//                    e.ConfigureConsumer<CallRequestAcknowledgedConsumer>(context);
//                    e.ConfigureConsumer<ForgetPasswordEmailConsumer>(context);
//                    e.ConfigureConsumer<SubTaskApprovalConsumer>(context);
//                    e.ConfigureConsumer<SubTaskRejectionConsumer>(context);
//                    e.ConfigureConsumer<SubTaskInProgressConsumer>(context);
//                    e.ConfigureConsumer<SubTaskResolvedConsumer>(context);
//                    e.ConfigureConsumer<SubTaskAssigneeChangedConsumer>(context);
//                    e.ConfigureConsumer<ServiceRequestInReviewConsumer>(context);
//                    e.ConfigureConsumer<ServiceRequestEscalatedConsumer>(context);
//                    e.ConfigureConsumer<ServiceRequestClosedConsumer>(context);
//                    e.ConfigureConsumer<ServiceRequestCreationEmailConsumer>(context);
//                    e.ConfigureConsumer<SubTaskCreationEmailConsumer>(context);
//                    e.ConfigureConsumer<SubTaskResolvedEmailConsumer>(context);
//                    e.ConfigureConsumer<ServiceRequestInReviewEmailConsumer>(context);
//                    e.ConfigureConsumer<ServiceRequestEscalatedEmailConsumer>(context);
//                    e.ConfigureConsumer<ServiceRequestClosedEmailConsumer>(context);
//                    e.ConfigureConsumer<RemoveCallRecordConsumer>(context);
//                    e.ConfigureConsumer<FetchSRAssociatedUsersConsumer>(context);
//                    e.ConfigureConsumer<CommentAddedEmailConsumer>(context);
//                    e.ConfigureConsumer<ReplyAddedEmailConsumer>(context);
//                    e.ConfigureConsumer<CommentAddedConsumer>(context);
//                    e.ConfigureConsumer<ReplyAddedConsumer>(context);
//                    e.ConfigureConsumer<UpdateUserStatusConsumer>(context);
//                    e.ConfigureConsumer<UpdateLiveUserStatusConsumer>(context);
//                    e.ConfigureConsumer<DirectCallCreationConsumer>(context);
//                    e.ConfigureConsumer<NotificationConsumer>(context);
//                });
//            });
//        });

//        return services;
//    }
//}
