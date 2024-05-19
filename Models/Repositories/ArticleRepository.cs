using System.Collections.Generic;
using System.Linq;
using e_commerce.Models;
namespace e_commerce.Models.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Article> GetAll()
        {
            return _context.Article.ToList();
        }

        public Article GetById(int id)
        {
            return _context.Article.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Article article)
        {
            _context.Article.Add(article);
            _context.SaveChanges();
        }

        public void Update(Article article)
        {
            _context.Article.Update(article);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var articleToDelete = _context.Article.FirstOrDefault(a => a.Id == id);
            if (articleToDelete != null)
            {
                _context.Article.Remove(articleToDelete);
                _context.SaveChanges();
            }
        }
     



    }
}
