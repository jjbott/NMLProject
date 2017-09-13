using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public enum Property
    {
        [ApiName("patent_id")]
        [PropertyType(PropertyType.String)]
        Id,
        [ApiName("patent_title")]
        [PropertyType(PropertyType.FullText)]
        Title,
        [ApiName("patent_date")]
        [PropertyType(PropertyType.Date)]
        Date,
        [ApiName("patent_abstract")]
        [PropertyType(PropertyType.FullText)]
        Abstract,
        [ApiName("inventor_first_name")]
        [PropertyType(PropertyType.String)]
        InventorFirstName,
        [ApiName("inventor_last_name")]
        [PropertyType(PropertyType.String)]
        InventorLastName,

    }

    public static class PropertyExtensions
    {
        public static PropertyType Type(this Property property)
        {
            return property.GetAttribute<PropertyTypeAttribute>().Type;          
        }

        public static string ApiName(this Property property)
        {
            return property.GetAttribute<ApiNameAttribute>().Name;
        }
    }        
}
