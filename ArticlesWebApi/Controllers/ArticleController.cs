using ArticlesWebApi.Dto;
using ArticlesWebApi.Interfaces;
using ArticlesWebApi.Models;
using ArticlesWebApi.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArticlesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;


        public ArticleController(IArticleRepository articleRepository, IMapper mapper, IUserRepository userRepository)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // GET: api/Article
        /// <summary>
        /// Gets all Articles
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticles()
        {
            var articles = _articleRepository.GetArticles();
            return Ok(articles);
        }

        // GET: api/Article/{id}
        /// <summary>
        /// Get Article by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Article))]
        [ProducesResponseType(400)]
        public IActionResult GetArticle(int id)
        {
            var article = _articleRepository.GetArticle(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        /// <summary>
        /// Create artical by user Id
        /// </summary>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateArticle([FromQuery] int userId, [FromBody] ArticleDto articleCreate)
        {
            if (articleCreate == null)
                return BadRequest(ModelState);

            var article = _articleRepository.GetArticles().
                Where(c => c.Title.Trim().ToUpper() == articleCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (article != null)
            {
                ModelState.AddModelError("", "Article already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var articleMap = _mapper.Map<Article>(articleCreate);

            articleMap.User = _userRepository.GetUser(userId);

            if (!_articleRepository.CreateArticle(articleMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }


            return Ok("Successfully created");

        }

        /// <summary>
        /// Update artical by id
        /// </summary>
        [HttpPut("artId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateArtical(int artId, [FromBody] ArticleDto updatedArtical, [FromQuery] int userId)
        {
            if (updatedArtical == null)
                return BadRequest(ModelState);

            if (artId != updatedArtical.Id)
                return BadRequest(ModelState);

            if (!_articleRepository.ArticleExists(artId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var articalMap = _mapper.Map<Article>(updatedArtical);

            articalMap.User = _userRepository.GetUser(userId);

            if (!_articleRepository.UpdateArticle(articalMap))
            {
                ModelState.AddModelError("", "Something went wrong updating artical");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete artical by id
        /// </summary>
        [HttpDelete("{artId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteArticle(int artId)
        {
            if (!_articleRepository.ArticleExists(artId))
            {
                return NotFound();
            }

            var articalToDelete = _articleRepository.GetArticle(artId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_articleRepository.DeleteArticle(articalToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting artical");
            }

            return NoContent();
        }


        /// <summary>
        /// Get Artical by UserId
        /// </summary>
        // GET: api/Article/user/{userId}
        [HttpGet("user/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Article>))]
        public IActionResult GetArticlesByUser(int userId)
        {
            var articles = _articleRepository.GetArticleByUser(userId);
            return Ok(articles);
        }
    }
}
