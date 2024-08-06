//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB Guidegrated sulution business Company
//App       : PeopleOfDetermination Application (())
//Date      : July - 2024 
/////////////////////////////////////////////////////


using Core.Entities;

namespace Core.Interfaces
{
    public interface IServices
    {
        public IEnumerable<Service> GetAll();
        public IEnumerable<Service> GetAllUser();
        public Task<Service> GetById(Guid id);
        public Task<BaseResponse> Create(Service service);
        public Task<BaseResponse> Update(Service service);
        public Task<BaseResponse> DeActivate(Guid id);
        public Task<BaseResponse> Activate(Guid id);
        public Task<Service> GetByName(string Name);



        public Task<int> totalServices();
        public Task<int> ActiveServices();
        public Task<int> DeactiveServices();
    }
}
