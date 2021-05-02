using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BE.Bases
{
    public class Property
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public IEnumerable<PropertyImage> PropertyImages { get; set; }
        public IEnumerable<PropertyTrace> PropertyTraces { get; set; }

        public string PropertyImagesJson() => PropertyImages != null ? JsonConvert.SerializeObject(this.PropertyImages) : string.Empty;
    }

    public class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }

    public class PropertyTrace
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }
    }
}
