using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_DomainModel.IRepositories.AuthModule;
using ToDoList_DomainModel.IRepositories.TaskModule;

namespace ToDoList_DomainModel.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public ILevelRepository LevelRepository { get; }
        public IMissionRepository MissionRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        Task<int> CompleteAsync();
        void Dispose();
    }
}
