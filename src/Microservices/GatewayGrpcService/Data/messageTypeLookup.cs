using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GatewayGrpcService.Data
{
    [Table("messageTypeLookup")]
    public partial class messageTypeLookup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public messageTypeLookup()
        {
            Commons = new HashSet<Common>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Common> Commons { get; set; }
    }
}
