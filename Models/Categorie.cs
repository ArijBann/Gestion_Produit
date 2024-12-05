namespace GestionDesProduits.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string NomCategorie { get; set; }
        public virtual ICollection<Produits> Produits { get; set; }

    }
}
