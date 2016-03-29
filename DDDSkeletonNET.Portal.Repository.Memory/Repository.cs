using System;
using DDDSkeletonNET.Infrastructure.Common.Domain;
using DDDSkeletonNET.Infrastructure.Common.UnitOfWork;
using DDDSkeletonNET.Portal.Repository.Memory.Database;

namespace DDDSkeletonNET.Portal.Repository.Memory
{
    public abstract class Repository<DomainType, IdType, DatabaseType> : IUnitOfWorkRepository where DomainType : IAggregateRoot
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectContextFactory _objectContextFactory;

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("Unit of work");
            _unitOfWork = unitOfWork;
        }

        public void Update(DomainType aggregate)
        {
            _unitOfWork.RegisterUpdate(aggregate, this);
        }

        public void Insert(DomainType aggregate)
        {
            _unitOfWork.RegisterInsertion(aggregate, this);
        }

        public void Delete(DomainType aggregate)
        {
            _unitOfWork.RegisterDeletion(aggregate, this);
        }

        public abstract DomainType FindBy(IdType id);

        public abstract DatabaseType ConvertToDatabaseType(DomainType domainType);

        public void PersistInsertion(IAggregateRoot aggregateRoot)
        {
            DatabaseType databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().AddEntity<DatabaseType>(databaseType);
        }

        public void PersistUpdate(IAggregateRoot aggregateRoot)
        {
            DatabaseType databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().UpdateEntity<DatabaseType>(databaseType);
        }

        public void PersistDeletion(IAggregateRoot aggregateRoot)
        {
            DatabaseType databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().DeleteEntity<DatabaseType>(databaseType);
        }

        private DatabaseType RetrieveDatabaseTypeFrom(IAggregateRoot aggregateRoot)
        {
            DomainType domainType = (DomainType)aggregateRoot;
            DatabaseType databaseType = ConvertToDatabaseType(domainType);
            return databaseType;
        }
    }
}
