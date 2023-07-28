using ArticlesWebApi.Dto;
using ArticlesWebApi.Interfaces;
using ArticlesWebApi.Models;
using ArticlesWebApi.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentsController : Controller
    {
        private readonly IComentsRepository _comentsRepository;
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;


        public ComentsController(IComentsRepository comentsRepository, IMapper mapper, IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _comentsRepository = comentsRepository;
        }

        /// <summary>
        /// Gets all Coments
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Coments>))]
        public IActionResult GetComents()
        {
            var coments = _comentsRepository.GetComments();
            return Ok(coments);
        }

        /// <summary>
        /// Get coment by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Coments))]
        [ProducesResponseType(400)]
        public IActionResult GetComent(int id)
        {
            var coment = _comentsRepository.GetComent(id);
            if (coment == null)
                return NotFound();

            return Ok(coment);
        }

        /// <summary>
        /// Create coment by article Id
        /// </summary>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateComent([FromQuery] int artId, [FromBody] ComentsDto comentsCreate)
        {
            if (comentsCreate == null)
                return BadRequest(ModelState);

            var coment = _comentsRepository.GetComments().
                Where(c => c.Content.Trim().ToUpper() == comentsCreate.Content.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (coment != null)
            {
                ModelState.AddModelError("", "Coment already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comentMap = _mapper.Map<Coments>(comentsCreate);

            comentMap.Article = _articleRepository.GetArticle(artId);

            if (!_comentsRepository.CreateComent(comentMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }


            return Ok("Successfully created");

        }

        /// <summary>
        /// Update coment by id
        /// </summary>
        [HttpPut("comentId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateComent(int comentId, [FromBody] ComentsDto updatedComent, [FromQuery] int artId)
        {
            if (updatedComent == null)
                return BadRequest(ModelState);

            if (comentId != updatedComent.Id)
                return BadRequest(ModelState);

            if (!_comentsRepository.ComentExists(comentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comentMap = _mapper.Map<Coments>(updatedComent);

            comentMap.Article = _articleRepository.GetArticle(artId);

            if (!_comentsRepository.UpdateComent(comentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating artical");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete coment by id
        /// </summary>
        [HttpDelete("{comentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteComent(int comentId)
        {
            if (!_comentsRepository.ComentExists(comentId))
            {
                return NotFound();
            }

            var comentToDelete = _comentsRepository.GetComent(comentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_comentsRepository.DeleteComent(comentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting coment");
            }

            return NoContent();
        }

        /// <summary>
        /// Get Artical by UserId
        /// </summary>
        [HttpGet("artical/{artId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Coments>))]
        public IActionResult GetComentsByArtical(int artId)
        {
            var coments = _comentsRepository.GetComentByArtical(artId);
            return Ok(coments);
        }
    }
}
