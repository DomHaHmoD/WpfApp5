//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace core.persistence
{
    using System;
    using System.Collections.Generic;
    
    public partial class datameteo
    {
        public int id { get; set; }
        public System.DateTime date { get; set; }
        public decimal temperature { get; set; }
        public decimal humudite { get; set; }
        public Nullable<int> id_capteur { get; set; }
    
        public virtual capteur capteur { get; set; }
    }
}
