using IRepositories;
using IServices;
using Models;

namespace Services
{
	/// <summary>
    ///   业务操作层实现——Person
    /// </summary>
    public partial class PersonsService : BaseService<Persons>, IPersonsService
    {
		IPersonsRepository dal;

		public PersonsService(IPersonsRepository dal)
		{
			this.dal = dal;
			base.baseDal = dal;
		}
	}
}
