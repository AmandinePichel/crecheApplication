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

    public partial class ChildFood
    {
        public ChildFood()
        {

        }
        public ChildFood(int? fk_Child, int? fk_Food)
        {
            Fk_Child = fk_Child;
            Fk_Food = fk_Food;
        }

        public int ChildFoodID { get; set; }
        public Nullable<int> Fk_Child { get; set; }
        public Nullable<int> Fk_Food { get; set; }

        public virtual Child Child { get; set; }
        public virtual Food Food { get; set; }
    }
}
