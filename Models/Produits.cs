using System.ComponentModel.DataAnnotations;

namespace GestionDesProduits.Models
{
    public class Produits
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string NomProduit { get; set; }
        public int MagasinId { get; set; }
        public virtual Magasin Magasins { get; set; }
        public int CategorieId { get; set; }
        public virtual Categorie Categories { get; set; }
        public string? DescriptionStock { get; set; }


        public float prixProduit { get; set; }
        public float prixProduitEnPromo { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateDebutPromo { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFinPromo { get; set; }
    }
}
