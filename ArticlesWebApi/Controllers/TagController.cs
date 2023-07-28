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
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all tags
        /// </summary>
        /// <returns>
        /// Must return all tags
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        public IActionResult GetTags()
        {
            var tags = _mapper.Map<List<TagDto>>(_tagRepository.GetTags());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tags);
        }

        /// <summary>
        /// Get tag by id
        /// </summary>
        [HttpGet("{tagId}")]
        [ProducesResponseType(200, Type = typeof(Tag))]
        [ProducesResponseType(400)]
        public IActionResult GetTag(int tagId)
        {
            if (!_tagRepository.TagExists(tagId))
                return NotFound();

            var tag = _mapper.Map<TagDto>(_tagRepository.GetTag(tagId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tag);
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTag([FromBody] TagDto tagCreate)
        {
            if (tagCreate == null)
                return BadRequest(ModelState);

            var tag = _tagRepository.GetTags().
                Where(c => c.Name.Trim().ToUpper() == tagCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (tag != null)
            {
                ModelState.AddModelError("", "Tag already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tagMap = _mapper.Map<Tag>(tagCreate);

            if (!_tagRepository.CreateTag(tagMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }


            return Ok("Successfully created");

        }


        /// <summary>
        /// Update tag by id
        /// </summary>
        [HttpPut("tagId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTag(int tagId, [FromBody] TagDto updatedTag)
        {
            if (updatedTag == null)
                return BadRequest(ModelState);

            if (tagId != updatedTag.Id)
                return BadRequest(ModelState);

            if (!_tagRepository.TagExists(tagId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tagMap = _mapper.Map<Tag>(updatedTag);

            if (!_tagRepository.UpdateTag(tagMap))
            {
                ModelState.AddModelError("", "Something went wrong updating tag");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete tag by id
        /// </summary>
        [HttpDelete("{tagId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTag(int tagId)
        {
            if (!_tagRepository.TagExists(tagId))
            {
                return NotFound();
            }

            var userToDelete = _tagRepository.GetTag(tagId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tagRepository.DeleteTag(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting tag");
            }
             
            return NoContent();
        }

    }
}
