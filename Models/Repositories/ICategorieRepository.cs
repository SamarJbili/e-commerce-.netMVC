namespace e_commerce.Models.Repositories
{
    public interface ICategorieRepository
    {
        IEnumerable<Categorie> GetAll();
        Categorie GetById(int id);
        void Add(Categorie categorie);
        void Update(Categorie categorie);
        void Delete(int id);
    }
}
