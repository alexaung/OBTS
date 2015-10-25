using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OBTS.API.Models
{
    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    };

    

    public sealed class OBTSEnum
    {
        public enum Types
        {
            //[Description(null)]
            //None = 0,
            [Description("UserType")]
            UserType,
            [Description("Operator")]
            Operator,
            [Description("Brand")]
            Brand,
            [Description("BusType")]
            BusType,
            [Description("BusFeatures")]
            BusFeatures,
            [Description("Amenities")]
            Amenities
        }

        public enum BookState
        {
            NotAvailable=0,
            Selected=1,
            Booked=2,
            Available=3
        }

        public static string ToString(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

    }
}