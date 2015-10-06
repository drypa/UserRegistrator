using System;
using System.Runtime.Serialization;

namespace WcfCommon.Domain
{
    [DataContract]
    public class City
    {
        [DataMember]
        public virtual Guid Id { get; set; }

        [DataMember]
        public virtual string Name { get; set; }
    }
}