using AutoMapper;
using LibraryAPI.Dtos;
using LibraryAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private IMapper _mapper;

        public MembersController(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> Get()
        {
            var members = await _memberRepository.GetAllUsersAsync();
            var mappedMembers = _mapper.Map<IEnumerable<MemberDto>>(members);
            return Ok(mappedMembers);
        }

        [HttpGet("{id}", Name = "GetMemberById")]
        public async Task<ActionResult<MemberDto>> GetMemberById(int id)
        {
            var member = await _memberRepository.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            var mappedMember = _mapper.Map<MemberDto>(member);
            return Ok(mappedMember);
        }
    }
}
