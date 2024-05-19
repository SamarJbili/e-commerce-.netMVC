namespace e_commerce.Models.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly AppDbContext _context;

        public CategorieRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categorie> GetAll()
        {
            return _context.Categorie.ToList();
        }

        public Categorie GetById(int id)
        {
            return _context.Categorie.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Categorie categorie)
        {
            _context.Categorie.Add(categorie);
            _context.SaveChanges();
        }

        public void Update(Categorie categorie)
        {
            _context.Categorie.Update(categorie);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var categorieToDelete = _context.Categorie.FirstOrDefault(c => c.Id == id);
            if (categorieToDelete != null)
            {
                _context.Categorie.Remove(categorieToDelete);
                _context.SaveChanges();
            }
        }
    }
}
