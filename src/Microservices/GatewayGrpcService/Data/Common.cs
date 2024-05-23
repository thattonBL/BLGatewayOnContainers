using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GatewayGrpcService.Data
{
    [Table("Common")]
    public partial class Common
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public int id { get; set; }

        [StringLength(50)]
        public string msg_status { get; set; }

        [StringLength(50)]
        public string msg_source { get; set; }

        public int msg_target { get; set; }

        [StringLength(50)]
        public string prty { get; set; }

        public int type { get; set; }

        [StringLength(50)]
        public string ref_source { get; set; }

        [StringLength(50)]
        public string ref_request_id { get; set; }

        public int? ref_seq_no { get; set; }

        public DateTime dt_created { get; set; }

        public bool is_acknowledged { get; set; }

        public virtual messageTypeLookup messageTypeLookup { get; set; }
    }
}
