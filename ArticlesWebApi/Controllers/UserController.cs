using ArticlesWebApi.Dto;
using ArticlesWebApi.Interfaces;
using ArticlesWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        

       


       
    }
}
