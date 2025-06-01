using AutoMapper;
using FAMS.Core.Exceptions;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Printing;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Api.Repositories;
using FAMS.Domain.Constants;
using FAMS.Domain.Dtos.Response;

namespace FAMS.Api.Services
{

    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _baseRepository;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IBaseRepository<ClassUser> _classUserRepo;
        private readonly IBaseRepository<UserPermission> _userPermission;
        private readonly IBaseRepository<Syllabus> _syllabusRepo;
        private readonly IBaseRepository<TrainingProgram> _trainingRepo;
        private readonly IBaseRepository<Class> _classRepo;
        private IBaseRepository<User> object1;
        private IUserRepo object2;
        private IPasswordGenerator object3;
        private IMapper object4;
        private IBaseRepository<ClassUser> object5;
        private IBaseRepository<UserPermission> object6;

        public UserService(IBaseRepository<User> baseRepository, IUserRepo userRepo,
            IPasswordGenerator passwordGenerator, IMapper mapper, IBaseRepository<ClassUser> classUserRepo,
            IBaseRepository<UserPermission> userPermission,IBaseRepository<Syllabus> syllabusRepo,
            IBaseRepository<TrainingProgram> trainingRepo, IBaseRepository<Class> classRepo)
        {
            _baseRepository = baseRepository;
            _passwordGenerator = passwordGenerator;
            _mapper = mapper;
            _userRepo = userRepo;
            _classUserRepo = classUserRepo;
            _userPermission = userPermission;
            _syllabusRepo = syllabusRepo;
            _trainingRepo = trainingRepo;
            _classRepo = classRepo;
        }

        public UserService(IBaseRepository<User> object1, IUserRepo object2, IPasswordGenerator object3, IMapper object4, IBaseRepository<ClassUser> object5, IBaseRepository<UserPermission> object6)
        {
            this.object1 = object1;
            this.object2 = object2;
            this.object3 = object3;
            this.object4 = object4;
            this.object5 = object5;
            this.object6 = object6;
        }

        public async Task<UserResponseDto> GetUser(int id)
        {
            var user = await _baseRepository.Get()
                .Include(x => x.UserPermission)
                .FirstOrDefaultAsync(user => user.Id == id);

            var returned =_mapper.Map<UserResponseDto>(user);
            returned.DateOfBirth = user!=null && user.DateOfBirth.HasValue ? user.DateOfBirth.Value.ToString("dd/MM/yyyy") : "No Information";
            returned.RoleName = user!=null && user.UserPermission !=null?  user.UserPermission.RoleName:"No Information";
            return return;
        }

        public async Task<UserResponseDto> UpdateUser(UpdateUserDTO user)
        {
            var existedUser = await _baseRepository.GetById(user.Id);
            if (existedUser != null)
            {
                existedUser.DateOfBirth = DateTime.Parse(user.DateOfBirth) ;
                existedUser.Gender = user.Gender;
                existedUser.Name = user.Name;
                existedUser.Phone = user.Phone;
                existedUser.Email = user.Email;
                existedUser.ModifiedDate = DateTime.Now;
                existedUser.Status=user.Status;
                var permission = await _userPermission.Get().Where(x => x.RoleName.ToLower().Contains(user.Rolename.ToLower().Trim())).FirstOrDefaultAsync();
                if (permission == null) return null;
                existedUser.PermissionId = permission.PermissionId;
                _baseRepository.Update(existedUser);
            }

            await _baseRepository.SaveChangesAsync();
            return await GetUser(user.Id);
        }

        private async Task<bool> CheckEmailExist(string email)
        {
            var result = await _baseRepository.FindOne(x => x.Email.ToLower().Trim() == email.Trim().ToLower());
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }

        private async Task<bool> CheckPhoneExist(string phone)
        {
            var result = await _baseRepository.FindOne(x => x.Phone.Trim() == phone.Trim());
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }
        public async Task<UserResponseDto> CreateUser(CreateUserDTO user)
        {
            try
            {

                var permissionId = await _userPermission.Get().Where(x => x.RoleName.ToLower().Contains(user.Rolename.ToLower())).Select(x => x.PermissionId).FirstOrDefaultAsync();
                if (permissionId == null) throw new Exception("Not found permission");
                if (await CheckEmailExist(user.Email) || await CheckPhoneExist(user.Phone))
                {
                    throw new BusinessException("Email or Phone existed.");
                }
                var createdUser = _mapper.Map<User>(user);
                createdUser.PermissionId = permissionId;
                createdUser.AvatarUrl = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg";
                createdUser.Password = "123";

                await _baseRepository.AddAsync(createdUser);
                await _baseRepository.SaveChangesAsync();
                var result = await _baseRepository.Get().OrderBy(x => x.Id).Include(x => x.UserPermission).LastAsync();
                var returned = _mapper.Map<UserResponseDto>(result);
                return returned;
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsers(int? pageNumber, int? pageSize)
        {
            IQueryable<User> query = _baseRepository.Get()
                .Include(x => x.UserPermission)
                .AsNoTracking();
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    throw new ArgumentException("Invalid page and page size number!");
                }

                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }
            var users = await query.ToListAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

       public async Task<IActionResult> SearchUsers(int pageNumber = 1, int pageSize = 10, string? searchInput = null,string? gender = null, string? roleName=null, string? dobFro = null, string? dobTo =null,
                                           string? sortBy = null, string? order = null)
        {

            var searchAllUsers = (await _baseRepository.Get().Include(x=>x.UserPermission).ToListAsync()).AsEnumerable();

            if (searchInput != null)
            {
                  searchAllUsers= searchAllUsers.Where(x => string.IsNullOrEmpty(searchInput) || (x.Name.Trim().ToLower().Contains(searchInput.Trim().ToLower())
                                                             || x.Email.Trim().ToLower().Contains(searchInput.Trim().ToLower())
                                                             || x.Gender.Trim().ToLower() == searchInput.Trim().ToLower()
                                                             || x.UserPermission.RoleName.ToLower().Contains(searchInput.ToLower().Trim())));
            }
            if(!string.IsNullOrEmpty(gender))
            {
                var kindsOfGender = DivideString(gender).Select(x => x.ToLower());
                searchAllUsers = searchAllUsers.Where(x => x.Gender!=null &&kindsOfGender.Contains(x.Gender.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleName))
            {   
                var kindsOfRoleName = DivideString((roleName).ToLower()).Select(x=>x.Trim());
                searchAllUsers = searchAllUsers.Where(x => kindsOfRoleName.Contains(x.UserPermission.RoleName.ToLower().Trim())); 
            }
            if (!string.IsNullOrEmpty(dobFro))
            {
                var date = DateTime.Parse(dobFro);
                searchAllUsers = searchAllUsers.Where(x => x.DateOfBirth.HasValue&&x.DateOfBirth.Value>date);

            }
            if (!string.IsNullOrEmpty(dobTo))
            {
                var date = DateTime.Parse(dobTo);
                searchAllUsers = searchAllUsers.Where(x => x.DateOfBirth.HasValue&&x.DateOfBirth.Value < date);

            }
            if (!searchAllUsers.Any())
            {
                return new NotFoundObjectResult(EMS.EM49);
            }

            if (sortBy != null) 
            switch (sortBy.ToLower())
            {
                
                        case OrderByConstant.SortBy_Id:
                            searchAllUsers = order.Trim()!=null && order.ToLower()  == OrderByConstant.OrderByDESC ? searchAllUsers.OrderByDescending(x => x.Id) : searchAllUsers.OrderBy(x => x.Id);
                            break;
                        case OrderByConstant.SortBy_Name:
                            searchAllUsers = order.Trim() != null && order.ToLower() == OrderByConstant.OrderByDESC ? searchAllUsers.OrderByDescending(x => x.Name) : searchAllUsers.OrderBy(x => x.Name);
                            break;
                        case OrderByConstant.SortBy_Email:
                            searchAllUsers = order.Trim() != null && order.ToLower() == OrderByConstant.OrderByDESC ? searchAllUsers.OrderByDescending(x => x.Email) : searchAllUsers.OrderBy(x => x.Email);
                            break;
                        case OrderByConstant.SortBy_DateOfBirth:
             
                            searchAllUsers = order.Trim() != null && order.ToLower() == OrderByConstant.OrderByDESC ? searchAllUsers.OrderByDescending(x => x.DateOfBirth) : searchAllUsers.OrderBy(x => x.DateOfBirth);
                            break;
                        case OrderByConstant.SortBy_Gender:
                            searchAllUsers = order.Trim() != null && order.ToLower() == OrderByConstant.OrderByDESC ? searchAllUsers.OrderByDescending(x => x.Gender) : searchAllUsers.OrderBy(x => x.Gender);
                            break;
                        case OrderByConstant.SortBy_Rolename:
                            searchAllUsers = order.Trim() != null && order.ToLower() == OrderByConstant.OrderByDESC ? searchAllUsers.OrderByDescending(x => x.UserPermission.RoleName) : searchAllUsers.OrderBy(x => x.UserPermission.RoleName);
                            break;
                         case OrderByConstant.SortBy_Phone:
                        searchAllUsers = order.Trim() != null && order.ToLower() == OrderByConstant.OrderByDESC ? searchAllUsers.OrderByDescending(x => x.Phone) : searchAllUsers.OrderBy(x => x.Phone);
                        break;


                }

            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = searchAllUsers.Count();

                var totalItems = searchAllUsers.Count();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                var returnedUsers = searchAllUsers.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new UserResponseDto()
                {
                    Id = x.Id,
                    Email = x.Email,
                    DateOfBirth =x.DateOfBirth.HasValue? x.DateOfBirth.Value.ToString(Value.DateFormat) : Value.NoInformation,
                    Gender = x.Gender,
                    AvatarUrl=x.AvatarUrl,
                    Phone = x.Phone,
                    Name = x.Name,
                    PermissionId = x.PermissionId,
                    RoleName= x.UserPermission.RoleName

                });

                return new OkObjectResult(new ViewListResponse { TotalPage= totalPages, PageNumber = pageNumber, PageSize= pageSize, List = returnedUsers.ToArray() });
            }
            catch (Exception ex)
            {
                throw new Exception("xxx " + ex);
            }
        }
        private IEnumerable<string> DivideString(string name)
        {
            var names = name.Split(',').Select(x => x.Trim().ToLower());
            ;
            return names;
        }

        public async Task<IActionResult> EditAvater(string link, int userId)
        {
            var user = await _baseRepository.Get().FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return new BadRequestObjectResult(EMS.EM51);
            user.AvatarUrl = link.Replace("%2F","/");
            _baseRepository.Update(user);
            await _baseRepository.SaveChangesAsync(); 
            return new OkObjectResult("Edit sucessfully");
        }
        public async Task<DashboardDto> GetDashboard()
        {
            var result = new DashboardDto();
            result.ActiveTrainer = (await _baseRepository.Get().Where(u => u.Status == true && u.PermissionId.ToUpper().Equals("TR")).ToListAsync()).Count;
            result.InactiveTrainer = (await _baseRepository.Get().Where(u => u.Status == false && u.PermissionId.ToUpper().Equals("TR")).ToListAsync()).Count;
            result.OpeningClass = (await _classRepo.Get().Where(u => u.Status.ToUpper().Equals("Opening")).ToListAsync()).Count;
            result.PlanningClass = (await _classRepo.Get().Where(u => u.Status.ToUpper().Equals("Planning")).ToListAsync()).Count;
            result.ActiveSyllabus = (await _syllabusRepo.Get().Where(u=> u.PublishStatus==1).ToListAsync()).Count;
            result.InactiveSyllabus = (await _syllabusRepo.Get().Where(u => u.PublishStatus ==-1).ToListAsync()).Count;
            result.ActiveTraining = (await _trainingRepo.Get().Where(u=> u.Status == 1).ToListAsync()).Count;
            result.InactiveTraining = (await _trainingRepo.Get().Where(u => u.Status ==-1).ToListAsync()).Count;
            return result;
        }
    }
}

