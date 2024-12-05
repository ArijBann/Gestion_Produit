namespace GestionDesProduits.Models
{
    public class Magasin
    {
        
            public int Id { get; set; }
            public string NomMagasin { get; set; }
            public string ville { get; set; }
        public string NomParVille { get { return NomMagasin + " " + ville; } }
        public virtual ICollection<Produits> Produits { get; set; }






    }
}
