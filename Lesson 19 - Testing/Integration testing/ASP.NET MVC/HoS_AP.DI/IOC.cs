using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DryIoc;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Services;
using HoS_AP.BLL.Validation;
using HoS_AP.DAL.Dao;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;
using HoS_AP.DAL.Repository;
using HoS_AP.Misc;
using log4net;

namespace HoS_AP.DI
{
    public sealed class InversionOfControlContainer : IInversionOfControlContainer
    {
        private IContainer container;
        private bool disposed;
        private static readonly ILog Logger = LogManager.GetLogger("InversionOfControlContainer");

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Depencency Injection Configuration")]
        public InversionOfControlContainer()
        {
            container = new Container(rules => rules.WithoutThrowOnRegisteringDisposableTransient());
            RegisterServices();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        private void RegisterServices()
        {
            container.RegisterDelegate<IGenericRepository<Character>>(r => new GenericRepository<Character>("Characters.json"), Reuse.Singleton);
            container.RegisterDelegate<IGenericRepository<Account>>(r => new GenericRepository<Account>("Accounts.json"), Reuse.Singleton);
            container.Register<IAccountDao, AccountDao>(Reuse.Singleton);
            container.Register<ICharacterDao, CharacterDao>(Reuse.Singleton);
            container.Register<IValidationMessageProvider, ValidationMessageProvider>(Reuse.Singleton);
            container.Register<IValidationService, ValidationService>(Reuse.Singleton);
            container.Register<IEncryptionService, EncryptionService>(Reuse.Singleton);
            container.Register<IAccountService, AccountService>(Reuse.Singleton);
            container.Register<ICharacterPresentationService, CharacterPresentationService>(Reuse.Singleton);
            container.Register<ICharacterOperationService, CharacterOperationService>(Reuse.Singleton);
            Logger.Debug("RegisterServices: Default registration is completed");
        }

        private InversionOfControlContainer(IContainer container)
        {
            this.container = container;
        }

        public void RegisterControllers(IEnumerable<Type> controllerTypes)
        {
            if (controllerTypes == null)
            {
                return;
            }

            foreach (var t in controllerTypes)
            {
                container.Register(t, Reuse.Transient);
            }
        }

        public void Mock<T>(T mockObject)
        {
            container.Unregister<T>();
            container.RegisterDelegate<T>(r => mockObject, Reuse.Singleton);
            container = container.WithoutSingletonsAndCache();
            Logger.WarnFormat("Mocked instance of type {0} as {1}", typeof(T), mockObject.GetType());
        }

        public object Resolve(Type serviceType)
        {
            return container.IsRegistered(serviceType) 
                ? container.Resolve(serviceType) 
                : null;
        }

        IEnumerable<object> IInversionOfControlContainer.ResolveAll(Type serviceType)
        {
            var result = new Collection<object>();
            if (container.IsRegistered(serviceType))
            {
                result.Add(container.Resolve(serviceType));
            }

            return result;
        }

        public bool IsRegistered(Type serviceType)
        {
            return container.IsRegistered(serviceType);
        }

        IInversionOfControlContainer IInversionOfControlContainer.CreateScope()
        {
            return new InversionOfControlContainer(container.OpenScope());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing && container != null)
            {
                container.Dispose();
            }

            disposed = true;
        }
    }
}
