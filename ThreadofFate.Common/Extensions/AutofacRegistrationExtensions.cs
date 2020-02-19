using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ThreadsofFate.Common.Extensions
{
    public static class AutofacRegistrationExtensions
    {
        public static IRegistrationBuilder<TClass, SimpleActivatorData, SingleRegistrationStyle> AddDbContext<TClass,
            TInterface>(
            this ContainerBuilder container,
            Action<DbContextOptionsBuilder> optionsAction,
            Func<DbContextOptions<TClass>, TClass> contextFunc)
            where TClass : DbContext, TInterface
        {
            var optionsBuilder = new DbContextOptionsBuilder<TClass>();
            optionsAction?.Invoke(optionsBuilder);

            return container.Register(c => contextFunc(optionsBuilder.Options))
                .AsSelf()
                .As<TInterface>()
                .InstancePerLifetimeScope();
        }

        public static IRegistrationBuilder<TClass, SimpleActivatorData, SingleRegistrationStyle> AddSingleton<TClass>(
            this ContainerBuilder container,
            Func<IComponentContext, TClass> instanceFunc)
            where TClass : class
        {
            return container.Register(instanceFunc)
                .AsSelf()
                .SingleInstance();
        }

        public static IRegistrationBuilder<TClass, SimpleActivatorData, SingleRegistrationStyle> AddSingleton<TInterface, TClass>(
            this ContainerBuilder container,
            Func<IComponentContext, TClass> instanceFunc)
            where TClass : class, TInterface
            where TInterface : class
        {
            return container.Register(instanceFunc)
                .As<TInterface>()
                .SingleInstance();
        }

        public static IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddSingleton<TClass>(
            this ContainerBuilder container)
            where TClass : class
        {
            return container.RegisterType<TClass>()
                .AsSelf()
                .SingleInstance();
        }

        public static IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddSingleton<TInterface, TClass>(
            this ContainerBuilder container)
            where TClass : class, TInterface
            where TInterface : class
        {
            return container.RegisterType<TClass>()
                .As<TInterface>()
                .SingleInstance();
        }

        public static IRegistrationBuilder<TClass, SimpleActivatorData, SingleRegistrationStyle> AddScoped<TInterface, TClass>(
            this ContainerBuilder container,
            Func<IComponentContext, TClass> instanceFunc)
            where TClass : class, TInterface
            where TInterface : class
        {
            return container.Register(instanceFunc)
                .As<TInterface>()
                .InstancePerLifetimeScope();
        }

        public static IRegistrationBuilder<TClass, SimpleActivatorData, SingleRegistrationStyle> AddScoped<TClass>(
            this ContainerBuilder container,
            Func<IComponentContext, TClass> instanceFunc)
            where TClass : class
        {
            return container.Register(instanceFunc)
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        public static IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddScoped<TClass>(
            this ContainerBuilder container)
            where TClass : class
        {
            return container.RegisterType<TClass>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        public static IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddScoped<TInterface, TClass>(
            this ContainerBuilder container)
            where TClass : class, TInterface
            where TInterface : class
        {
            return container.RegisterType<TClass>()
                .As<TInterface>()
                .InstancePerLifetimeScope();
        }

        public static IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddTransient<TClass>(
            this ContainerBuilder container)
            where TClass : class
        {
            return container.RegisterType<TClass>()
                .AsSelf()
                .InstancePerDependency();
        }

        public static IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddTransient<TInterface, TClass>(
            this ContainerBuilder container)
            where TClass : class, TInterface
        {
            return container.RegisterType<TClass>()
                .As<TInterface>()
                .InstancePerDependency();
        }

        public static IRegistrationBuilder<IOptions<TClass>, SimpleActivatorData, SingleRegistrationStyle> Configure<TClass>(
            this ContainerBuilder container,
            IConfigurationSection configurationSection)
            where TClass : class, new()
        {
            return container.Register(c =>
            {
                var options = new TClass();
                configurationSection.Bind(options);
                return Options.Create(options);
            })
                .AsSelf()
                .SingleInstance();
        }

        //public static IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> InterfaceInterceptedBy<TClass>(
        //    this IRegistrationBuilder<TClass, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder,
        //    params Type[] interceptorType)
        //    where TClass : class
        //{
        //    var registrationBuilder = builder.EnableInterfaceInterceptors();

        //    foreach (var type in interceptorType)
        //    {
        //        registrationBuilder = registrationBuilder.InterceptedBy(type);
        //    }

        //    return registrationBuilder;
        //}
    }
}
