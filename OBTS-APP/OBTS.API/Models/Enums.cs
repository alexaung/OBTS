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
            Amenities,
            [Description("Currency")]
            Currency
        }

        public enum BookState
        {
            Confirmed=0,
            UnConfirm = 1,
            Cancelled=3
        }

        public enum SeatState
        {
            Available = 0,
            Confirmed = 1,
            UnConfirm =2,
            NotAvailable = 3,
            Space = 4
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