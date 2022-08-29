using System.ComponentModel.DataAnnotations;

namespace MiniProjetBibliotheque.Models
{
    public class Lecteur
    {
        [Key]
        public int LecteurId { get; set; }
        [Required]
        [StringLength(50)]
        public string NomLecteur { get; set; }
        [StringLength(50)]
        public string PrenomLecteur { get; set; }
        public string EmailLecteur { get; set; }
        public int TelephoneLecteur { get; set; }
        public string Adresse { get; set; }
        [Required]
        public string Mdp_Lecteur { get; set; }

        public virtual ICollection<Emprunt> EmpruntsLecteur { get; set; }

        public void emprunter_livre()
        {

        }
    }
}
