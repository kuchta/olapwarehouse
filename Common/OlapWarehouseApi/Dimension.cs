//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OlapWarehouseApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dimension
    {
        public Dimension()
        {
            this.Dimensions = new HashSet<Dimension>();
            this.Elements = new HashSet<Element>();
        }
    
        public int Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> ServerId { get; protected set; }
        public Nullable<int> CubeId { get; protected set; }
        public Nullable<int> DimensionId { get; set; }
    
        public virtual Server Server { get; set; }
        public virtual Cube Cube { get; set; }
        public virtual Dimension Template { get; set; }
        public virtual ICollection<Dimension> Dimensions { get; set; }
        public virtual ICollection<Element> Elements { get; set; }
    }
}