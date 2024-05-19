namespace e_commerce.Models
{
    public class LignePanier
    {
        public int ID {  get; set; }
        public int ArticleID { get; set; }  
        public int Qte {  get; set; }
        public string PanierId { get; set; }
        public string UserID { get; set; }
        public Article Article { get; set; }
        public float montonTot()
        {
            return Qte * Article.Prix;
        }
    }
}
