using System.Text.Json.Serialization;
using VoucherCK.SharedKernel.Interfaces;

namespace VoucherCK.SharedKernel.Responses
{
    public class PagingResponse<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long Total { get; set; }
    }
    
    public class EntityPagingResponse<T> where T : IAggregateRoot
    {
        public IEnumerable<T> Items { get; set; }
        public long Total { get; set; }
    }    
}
