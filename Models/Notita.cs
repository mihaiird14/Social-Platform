namespace Social_Life.Models
{
    public class Notita
    {
        public virtual Profil Profil { set; get; }
        public int Id_Notita { set; get; }
        public string Text_Notita { set; get; }
        public int Nr_Aprecieri { set; get; }
        public virtual ICollection<Raspuns>Raspunsuri { set; get; }

    }
}
