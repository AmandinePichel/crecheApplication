//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrecheApplication.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Food
    {
        public bool IsChecked { get; set; }
        public int FoodID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ChildSchedule> ChildFood { get; set; }
        public Nullable<int> fk_menu { get; set; }
        public virtual Menu Menu2 { get; set; }

        public Food()
        {

        }

        public Food(string name)
        {
            this.Name = name;
        }
    }
}