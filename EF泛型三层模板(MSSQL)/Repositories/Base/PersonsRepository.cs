using System;
using System.Linq;
using System.Linq.Expressions;
using Models;
using IRepositories;

namespace Repositories
{
	/// <summary>
    ///   仓储操作层实现——Person
    /// </summary>
    public partial class PersonsRepository : BaseRepository<Persons>, IPersonsRepository
    { }
}
